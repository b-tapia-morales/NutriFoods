using System.Collections.Immutable;
using Domain.Enum;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using RecipeInsertion.Mapping;
using Utils.Csv;
using Utils.Enumerable;
using Utils.String;
using static Domain.Enum.DishTypes;
using static Domain.Enum.MealTypes;

namespace RecipeInsertion;

public static class Recipes
{
    private const string ConnectionString =
        "Host=localhost;Database=nutrifoods_db;Username=nutrifoods_dev;Password=MVmYneLqe91$";

    private static readonly DbContextOptions<NutrifoodsDbContext> Options =
        new DbContextOptionsBuilder<NutrifoodsDbContext>()
            .UseNpgsql(ConnectionString,
                builder => builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
            .Options;

    private static readonly string BaseDirectory =
        Directory.GetParent(Directory.GetCurrentDirectory())!.FullName;

    private static readonly string ProjectDirectory = Path.Combine(BaseDirectory, "RecipeInsertion");
    private static readonly string RecipesPath = Path.Combine(ProjectDirectory, "Recipe", "recipe.csv");
    private static readonly string IngredientMeasuresPath = Path.Combine(ProjectDirectory, "Measures");
    private static readonly string RecipeMeasuresPath = Path.Combine(ProjectDirectory, "DataRecipes", "Ingredient");
    private static readonly string StepsPath = Path.Combine(ProjectDirectory, "DataRecipes", "Steps");

    private static readonly Dictionary<string, MealTypes> MealTypesDict = new()
    {
        ["Desayunos"] = Breakfast,
        ["Ensaladas"] = Lunch,
        ["Entradas"] = Lunch,
        ["PlatosFondo"] = Lunch,
        ["Cenas"] = Dinner,
    };

    private static readonly Dictionary<string, DishTypes> DishTypesDict = new()
    {
        ["Ensaladas"] = Salad,
        ["Entradas"] = Entree,
        ["PlatosFondo"] = MainDish,
        ["Postres"] = Dessert,
        ["Vegano"] = Vegan
    };

    public static void BatchInsert()
    {
        var mappings = RowRetrieval
            .RetrieveRows<Recipe, RecipeMapping>(RecipesPath, DelimiterToken.Semicolon, true)
            .DistinctBy(e => e.Url);
        using var context = new NutrifoodsDbContext(Options);
        InsertRecipes(context, mappings);
        var ingredients = IncludeSubfields(context.Ingredients).ToList();
        var recipes = IncludeSubfields(context.Recipes).ToList();
        var measures = IncludeSubfields(context.IngredientMeasures).ToList();
        var ingredientsDict = IngredientDictionary(ingredients).AsReadOnly();
        var recipesDict = RecipeDictionary(recipes).AsReadOnly();
        var measuresDict = MeasureDictionary(measures).AsReadOnly();
        InsertEnumValues(context, recipesDict);
        InsertSteps(context, recipesDict);
        InsertRecipeMeasures(context, recipesDict, ingredientsDict, measuresDict);
    }

    private static void InsertRecipeMeasures(DbContext context,
        IReadOnlyDictionary<string, Recipe> recipesDict, IDictionary<string, Ingredient> ingredientsDict,
        IDictionary<(string Measure, string IngredientName), IngredientMeasure> measuresDict)
    {
        var paths = Directory.GetFiles(RecipeMeasuresPath, "*.csv", SearchOption.AllDirectories);
        var usedRecipes = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
        foreach (var path in paths)
        {
            var name = path.Split(@"\")[^1].Replace("_", " ").Replace(".csv", "").Standardize();

            if (!recipesDict.TryGetValue(name, out var recipe) || usedRecipes.Contains(name))
                continue;

            var recipeId = recipe.Id;

            var recipeIngredients = RowRetrieval
                .RetrieveRows<RecipeIngredient, RecipeIngredientMapping>(path, DelimiterToken.Comma)
                .Where(x => !x.Quantity.Equals("x") && !x.IngredientName.Equals("agua"));
            foreach (var recipeIngredient in recipeIngredients)
                InsertRecipeMeasure(context, recipeIngredient, ingredientsDict, measuresDict, recipeId);
            
            usedRecipes.Add(name);
        }
        
        context.SaveChanges();
    }

    private static void InsertRecipeMeasure(DbContext context, RecipeIngredient data,
        IDictionary<string, Ingredient> ingredientsDict,
        IDictionary<(string Measure, string IngredientName), IngredientMeasure> measuresDict, int recipeId)
    {
        if (!ingredientsDict.TryGetValue(data.IngredientName, out var ingredient))
            return;

        var ingredientId = ingredient.Id;
        if (data.MeasureName.Equals("g") || data.MeasureName.Equals("ml") || data.MeasureName.Equals("cc"))
        {
            context.Add(new RecipeQuantity
            {
                RecipeId = recipeId,
                IngredientId = ingredientId,
                Grams = double.Parse(data.Quantity)
            });
            return;
        }

        if (!measuresDict.TryGetValue((data.MeasureName.Format().Standardize(), data.IngredientName.Standardize()),
                out var measure))
            return;

        var measureId = measure.Id;
        switch (data.Quantity.Length)
        {
            case 1 or 2:
                InsertMeasure(context, recipeId, measureId, data.Quantity, "0", "0");
                break;
            case 3:
            {
                var numerator = data.Quantity[0].ToString();
                var denominator = data.Quantity[2].ToString();
                InsertMeasure(context, recipeId, measureId, "0", numerator, denominator);
                break;
            }
            default:
            {
                var integerPart = data.Quantity[0].ToString();
                var numerator = data.Quantity[2].ToString();
                var denominator = data.Quantity[4].ToString();
                InsertMeasure(context, recipeId, measureId, integerPart, numerator, denominator);
                break;
            }
        }
    }

    private static void InsertEnumValues(DbContext context, IReadOnlyDictionary<string, Recipe> recipesDict)
    {
        var paths = Directory.GetFiles(RecipeMeasuresPath, "*.csv", SearchOption.AllDirectories);
        foreach (var file in paths)
        {
            var name = file.Split(@"\")[^1].Replace("_", " ").Replace(".csv", "").Standardize();
            if (!recipesDict.TryGetValue(name, out var recipe))
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

    private static void InsertSteps(DbContext context, IReadOnlyDictionary<string, Recipe> recipesDict)
    {
        var stepPaths = Directory.GetFiles(StepsPath, "*.csv", SearchOption.AllDirectories);
        var usedRecipes = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
        foreach (var stepPath in stepPaths)
        {
            var recipeName = stepPath.Split(@"\")[^1].Replace("_", " ").Replace(".csv", "").Standardize();
            if (!recipesDict.TryGetValue(recipeName, out var recipe) || usedRecipes.Contains(recipeName))
                continue;

            var id = recipe.Id;
            var step = File.ReadAllLines(stepPath);

            for (var i = 0; i < step.Length; i++)
                context.Add(new RecipeStep
                {
                    RecipeId = id,
                    Number = i + 1,
                    Description = step[i]
                });

            usedRecipes.Add(recipeName);
        }

        context.SaveChanges();
    }

    private static void InsertRecipes(DbContext context, IEnumerable<Recipe> mappings)
    {
        foreach (var recipe in mappings)
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

    private static void InsertMeasure(DbContext context, int recipeId, int measureId, string integerPart,
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

    private static IDictionary<string, Ingredient> IngredientDictionary(IList<Ingredient> ingredients)
    {
        var ingredientsDict = ingredients
            .GroupBy(e => e.Name.Standardize(), StringComparer.InvariantCultureIgnoreCase)
            .ToDictionary(e => e.Key, e => e.First(), StringComparer.InvariantCultureIgnoreCase);
        var synonymsDict = ingredients
            .SelectMany(e => e.Synonyms.Select(x => (Synonym: x, Ingredient: e)))
            .GroupBy(e => e.Synonym.Standardize(), StringComparer.InvariantCultureIgnoreCase)
            .ToDictionary(e => e.Key, e => e.First().Ingredient, StringComparer.InvariantCultureIgnoreCase);
        return ingredientsDict.Merge(synonymsDict);
    }

    private static IDictionary<(string Measure, string IngredientName), IngredientMeasure> MeasureDictionary(
        IList<IngredientMeasure> measures) =>
        measures
            .GroupBy(e => (e.Name.Format().Standardize(), e.Ingredient.Name.Standardize()))
            .ToDictionary(e => e.Key, e => e.First());


    private static IDictionary<string, Recipe> RecipeDictionary(IList<Recipe> recipes) =>
        recipes
            .GroupBy(e => e.Name.Standardize(), StringComparer.InvariantCultureIgnoreCase)
            .ToDictionary(e => e.Key, e => e.First(), StringComparer.InvariantCultureIgnoreCase);

    private static IQueryable<Recipe> IncludeSubfields(this DbSet<Recipe> recipes) =>
        recipes
            .AsQueryable()
            .Include(e => e.NutritionalValues)
            .Include(e => e.RecipeMeasures)
            .ThenInclude(e => e.IngredientMeasure)
            .ThenInclude(e => e.Ingredient)
            .ThenInclude(e => e.NutritionalValues)
            .Include(e => e.RecipeQuantities)
            .ThenInclude(e => e.Ingredient)
            .ThenInclude(e => e.NutritionalValues)
            .Include(e => e.RecipeSteps);

    private static IQueryable<Ingredient> IncludeSubfields(this DbSet<Ingredient> dbSet) =>
        dbSet
            .AsQueryable()
            .Include(e => e.IngredientMeasures)
            .Include(e => e.NutritionalValues);

    private static IQueryable<IngredientMeasure> IncludeSubfields(this DbSet<IngredientMeasure> dbSet) =>
        dbSet
            .AsQueryable()
            .Include(e => e.Ingredient)
            .ThenInclude(e => e.NutritionalValues);
}