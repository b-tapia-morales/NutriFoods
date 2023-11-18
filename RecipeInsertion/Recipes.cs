using Domain.Enum;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using RecipeInsertion.Mapping;
using Utils.Csv;
using Utils.String;
using static System.StringComparison;
using static Domain.Enum.DishTypes;
using static Domain.Enum.MealTypes;
using static Utils.Csv.DelimiterToken;

namespace RecipeInsertion;

public static class Recipes
{
    private const string ConnectionString =
        "Host=localhost;Database=nutrifoods_db;Username=nutrifoods_dev;Password=MVmYneLqe91$";

    private static readonly string BaseDirectory =
        Directory.GetParent(Directory.GetCurrentDirectory())!.FullName;

    private static readonly string ProjectDirectory = Path.Combine(BaseDirectory, "RecipeInsertion");

    private static readonly string RecipesPath = Path.Combine(ProjectDirectory, "Recipe", "recipe.csv");

    private static readonly string IngredientsPath = Path.Combine(ProjectDirectory, "Ingredient", "ingredient.csv");

    private static readonly string RepeatedRecipesPath = Path.Combine(ProjectDirectory, "Recipe", "repeated.csv");

    private static readonly string SynonymsPath = Path.Combine(ProjectDirectory, "Recipe", "synonyms.csv");

    private static readonly string MeasuresPath = Path.Combine(ProjectDirectory, "DataRecipes", "Ingredient");

    private static readonly string StepsPath = Path.Combine(ProjectDirectory, "DataRecipes", "Steps");

    private static readonly Dictionary<string, MealTypes> MealTypesDict = new()
    {
        ["Cenas"] = Dinner,
        ["Desayunos"] = Breakfast
    };

    private static readonly Dictionary<string, DishTypes> DishTypesDict = new()
    {
        ["Ensaladas"] = Salad,
        ["Entradas"] = Entree,
        ["PlatosFondo"] = MainDish,
        ["Postres"] = Dessert,
        ["Vegano"] = Vegan
    };

    private static readonly Dictionary<string, List<string>> DictionarySynonyms = GetDictionarySynonyms();

    public static void RecipeInsert()
    {
        using var context = Context();

        var recipes = RowRetrieval
            .RetrieveRows<Recipe, RecipeMapping>(RecipesPath, Semicolon, true)
            .DistinctBy(e => e.Url);

        foreach (var recipe in recipes)
        {
            context.Add(new Recipe
            {
                Name = recipe.Name,
                Author = recipe.Author,
                Url = recipe.Url,
                Portions = recipe.Portions,
                Time = recipe.Time
            });
        }

        context.SaveChanges();
    }

    public static void IngredientSynonyms()
    {
        var dictionary = GetDictionarySynonyms();
        using var context = Context();
        var ingredients = context.Ingredients.ToList();
        foreach (var (name, synonyms) in dictionary)
        {
            var ingredient =
                ingredients.Find(e =>
                    string.Equals(e.Name.Standardize(), name.Standardize()));

            if (ingredient == null)
                continue;

            foreach (var synonym in synonyms)
                ingredient.Synonyms.Add(synonym);
        }

        context.SaveChanges();
    }

    public static void RecipeMeasures()
    {
        using var context = Context();
        var ingredients = context.Ingredients.ToList();
        var nameIngredient = File.ReadAllLines(IngredientsPath);
        foreach (var name in nameIngredient)
        {
            var ingredient = ingredients.Find(e => string.Equals(e.Name.Standardize(), name.Standardize()));
            if (ingredient == null)
                continue;

            var id = ingredient.Id;
            var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.FullName,
                "RecipeInsertion", "Measures", $"{name}.csv");
            var measuresIngredient = RowRetrieval.RetrieveRows<IngredientMeasure, MeasuresMapping>(path);
            foreach (var measure in measuresIngredient)
            {
                context.Add(new IngredientMeasure
                {
                    IngredientId = id,
                    Name = measure.Name,
                    Grams = measure.Grams
                });
            }
        }

        context.SaveChanges();
    }

    public static void InsertionOfRecipeData()
    {
        using var context = Context();
        var ingredients = context.Ingredients.ToList();
        var recipes = context.Recipes.ToList();
        var units = context.IngredientMeasures.ToList();
        InsertRecipeIngredientMeasures(context, recipes, ingredients, units);
        InsertRecipeSteps(context, recipes);
        InsertTypes(context, recipes);
        context.SaveChanges();
    }

    private static NutrifoodsDbContext Context()
    {
        var options = new DbContextOptionsBuilder<NutrifoodsDbContext>()
            .UseNpgsql(ConnectionString,
                builder => builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
            .Options;
        var context = new NutrifoodsDbContext(options);
        return context;
    }

    private static void InsertDataRecipe(DbContext context, DataRecipe dataRecipe, List<Ingredient> ingredients,
        List<IngredientMeasure> units, int recipeId)
    {
        var name = SynonymousIngredientAux(dataRecipe.NameIngredients).Standardize();
        var ingredient = ingredients.Find(e => string.Equals(e.Name.Standardize(), name.Standardize()));
        if (ingredient == null)
            return;

        var ingredientId = ingredient.Id;
        if (dataRecipe.Units.Equals("g") || dataRecipe.Units.Equals("ml") || dataRecipe.Units.Equals("cc"))
        {
            context.Add(new RecipeQuantity
            {
                RecipeId = recipeId,
                IngredientId = ingredientId,
                Grams = double.Parse(dataRecipe.Quantity)
            });
            return;
        }

        var measure = units.Find(e =>
            string.Equals(e.Name.Standardize(), dataRecipe.Units.Standardize()) && e.IngredientId == ingredientId);
        if (measure == null)
            return;

        var measureId = measure.Id;
        switch (dataRecipe.Quantity.Length)
        {
            case 1 or 2:
                InsertMeasuresWhitIngredient(context, recipeId, measureId, dataRecipe.Quantity, "0", "0");
                break;
            case 3:
            {
                var numerator = dataRecipe.Quantity[0].ToString();
                var denominator = dataRecipe.Quantity[2].ToString();
                InsertMeasuresWhitIngredient(context, recipeId, measureId, "0", numerator, denominator);
                break;
            }
            default:
            {
                var integerPart = dataRecipe.Quantity[0].ToString();
                var numerator = dataRecipe.Quantity[2].ToString();
                var denominator = dataRecipe.Quantity[4].ToString();
                InsertMeasuresWhitIngredient(context, recipeId, measureId, integerPart, numerator, denominator);
                break;
            }
        }
    }


    private static void InsertRecipeIngredientMeasures(DbContext context, List<Recipe> recipes,
        List<Ingredient> ingredients, List<IngredientMeasure> units)
    {
        var dataRecipes = Directory.GetFiles(MeasuresPath, "*.csv", SearchOption.AllDirectories);
        var repeated = RowRetrieval.RetrieveRows<Repeated, RecipeMapping>(RepeatedRecipesPath).ToList();
        foreach (var pathDataRecipe in dataRecipes)
        {
            var name = pathDataRecipe.Split(@"\")[^1].Replace("_", " ").Replace(".csv", "");

            var recipe = recipes.Find(e => string.Equals(e.Name.Standardize(), name.Standardize()));
            if (recipe == null)
                continue;

            var recipeId = recipe.Id;
            if (repeated.Exists(
                    e => string.Equals(e.Name.Standardize(), name.Standardize())))
            {
                var repeatedRecipe = repeated.Find(e => string.Equals(e.Name.Standardize(), name.Standardize()));
                if (repeatedRecipe!.AddedAmount > 0)
                    continue;
                repeatedRecipe.AddedAmount++;
            }

            var ingredientData = RowRetrieval.RetrieveRows<DataRecipe, RecipeDataMapping>(pathDataRecipe, Comma)
                .Where(x => !x.Quantity.Equals("x") && !x.NameIngredients.Equals("agua"));
            foreach (var data in ingredientData)
                InsertDataRecipe(context, data, ingredients, units, recipeId);
        }
    }

    private static void InsertRecipeSteps(DbContext context, List<Recipe> recipes)
    {
        var dataRecipesSteps = Directory.GetFiles(StepsPath, "*.csv", SearchOption.AllDirectories);
        var repeated = RowRetrieval.RetrieveRows<Repeated, RecipeMapping>(RepeatedRecipesPath).ToList();
        foreach (var pathDataRecipesStep in dataRecipesSteps)
        {
            var name = pathDataRecipesStep.Split(@"\")[^1].Replace("_", " ").Replace(".csv", "");
            var recipe = recipes.Find(e => string.Equals(e.Name.Standardize(), name.Standardize()));
            if (recipe == null || recipe.RecipeSteps.Count > 0)
                continue;
            
            var id = recipe.Id;
            var step = File.ReadAllLines(pathDataRecipesStep);
            if (repeated.Exists(e => string.Equals(e.Name.Standardize(), name.Standardize())))
            {
                var recipeRepeated = repeated.Find(e => string.Equals(e.Name.Standardize(), name.Standardize()));
                if (recipeRepeated!.AddedAmount > 0)
                    continue;
                recipeRepeated.AddedAmount++;
            }

            for (var i = 0; i < step.Length; i++)
                context.Add(new RecipeStep { RecipeId = id, Number = i + 1, Description = step[i] });
        }
    }

    private static void InsertTypes(DbContext context, List<Recipe> recipes)
    {
        var files = Directory.GetFiles(MeasuresPath, "*.csv", SearchOption.AllDirectories);
        foreach (var file in files)
        {
            var name = file.Split(@"\")[^1].Replace("_", " ").Replace(".csv", "");
            var recipe = recipes.Find(e => string.Equals(e.Name.Standardize(), name.Standardize()));
            if (recipe == null)
                continue;

            var mealTypes = MealTypesDict.Where(e => file.Contains(e.Key)).Select(e => e.Value);
            foreach (var mealType in mealTypes)
                recipe.MealTypes.Add(mealType);

            var dishTypes = DishTypesDict.Where(e => file.Contains(e.Key)).Select(e => e.Value);
            foreach (var dishType in dishTypes)
                recipe.DishTypes.Add(dishType);
        }

        context.SaveChanges();
    }

    private static void InsertMeasuresWhitIngredient(DbContext context, int recipeId, int measureId, string integerPart,
        string numerator, string denominator)
    {
        context.Add(new RecipeMeasure
        {
            RecipeId = recipeId,
            IngredientMeasureId = measureId,
            IntegerPart = int.Parse(integerPart),
            Numerator = int.Parse(numerator),
            Denominator = int.Parse(denominator)
        });
    }

    private static Dictionary<string, List<string>> GetDictionarySynonyms()
    {
        var rows = File.ReadAllLines(SynonymsPath).Select(e => e.Split(","));
        var dictionary = new Dictionary<string, List<string>>();
        foreach (var row in rows)
        {
            var ingredient = row[0];
            var synonyms = row[1].Split(";").Select(e => e.Capitalize()).ToList();
            if (string.Equals(synonyms[0], "None"))
                continue;

            dictionary.Add(ingredient, synonyms);
        }

        return dictionary;
    }

    private static string SynonymousIngredientAux(string name)
    {
        foreach (var (key, _) in DictionarySynonyms.Where(keyValuePair => keyValuePair.Value.Contains(name)))
        {
            return key;
        }

        return name;
    }
}