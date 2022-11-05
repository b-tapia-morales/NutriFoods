using System.Diagnostics;
using System.Globalization;
using System.Text;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using RecipeInsertion.Mapping;
using Utils.Csv;

namespace RecipeInsertion;

public static class Recipes
{
    private const string ConnectionString =
        "Host=localhost;Database=nutrifoods_db;Username=nutrifoods_dev;Password=MVmYneLqe91$";

    private static readonly string FilePathRecipes = Path.Combine(Directory.GetParent(
            Directory.GetCurrentDirectory())!.FullName, "RecipeInsertion", "Recipe", "recipe.csv");

    private static readonly string FilePathIngredient = Path.Combine(Directory.GetParent(
        Directory.GetCurrentDirectory())!.FullName, "RecipeInsertion","Ingredient","ingredient.csv");

    public static void RecipeInsert()
    {
        using var context = Context();
        
        var recipes = RowRetrieval
            .RetrieveRows<Recipe, RecipeMapping>(FilePathRecipes, DelimiterToken.Semicolon, true)
            .DistinctBy(e => e.Name);

        foreach (var recipe in recipes)
        {
            context.Add(new Recipe
            {
                Name = recipe.Name,
                Author = recipe.Author,
                Url = recipe.Url,
                Portions = recipe.Portions,
                PreparationTime = recipe.PreparationTime
            });
        }

        context.SaveChanges();
    }

    public static void RecipeMeasures()
    {
        using var context = Context();
        var ingredients = context.Ingredients.ToList();
        var nameIngredient = File.ReadAllLines(FilePathIngredient);
        foreach (var name in nameIngredient)
        {
            var id = ingredients.Find(x => x.Name.Equals(name))!.Id;
            var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.FullName,
                "RecipeInsertion", "Measures" , $"{name}.csv");
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
        var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.FullName,
            "RecipeInsertion", "DataRecipes", "Ingredient");
        var dataRecipes = Directory.GetFiles(path, "*.csv", SearchOption.AllDirectories);
        foreach (var pathDataRecipe in dataRecipes)
        {
            var nameRecipe = pathDataRecipe.Split(@"\")[^1].Replace("_", " ").Replace(".csv", "");
            var idRecipe = recipes.Find(x => x.Name.ToLower().Equals(nameRecipe))!.Id;
            var recipe = RowRetrieval.RetrieveRows<DataRecipe, RecipeDataMapping>(pathDataRecipe, DelimiterToken.Comma)
                .Where(x => !x.Quantity.Equals("x") && !x.NameIngredients.Equals("agua"));
            foreach (var dataRecipe in recipe)
            {
                InsertDataRecipe(context,dataRecipe,ingredients,units,idRecipe,pathDataRecipe);
            }
            context.SaveChanges();
        }
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

    private static string RemoveAccents(this string text) =>
        new string(text.Normalize(NormalizationForm.FormD).Where(
                c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray()
        ).Normalize(NormalizationForm.FormC);
    

    private static void InsertDataRecipe(DbContext context,DataRecipe dataRecipe, List<Ingredient> ingredients, List<IngredientMeasure> units,int idRecipe, string path)
    {
        try
        {
            var idIngredient =
                ingredients.Find(i => RemoveAccents(i.Name).ToLower().Equals(dataRecipe.NameIngredients))!.Id;
            if (dataRecipe.Units.Equals("g") || dataRecipe.Units.Equals("ml") || dataRecipe.Units.Equals("cc"))
            {
                context.Add(new RecipeQuantity
                {
                    RecipeId = idRecipe,
                    IngredientId = idIngredient,
                    Grams = double.Parse(dataRecipe.Quantity)
                });
            }
            else
            {
                var idMeasures = units.Find(u =>
                    u.Name.ToLower().Equals(dataRecipe.Units) && u.IngredientId == idIngredient)!.Id;
                switch (dataRecipe.Quantity.Length)
                {
                    case 1 or 2:
                        InsertMeasuresWhitIngredient(context, idRecipe, idMeasures,
                            dataRecipe.Quantity, "0", "0");
                        break;
                    case 3:
                    {
                        var numerator = dataRecipe.Quantity[0].ToString();
                        var denominator = dataRecipe.Quantity[2].ToString();
                        InsertMeasuresWhitIngredient(context, idRecipe, idMeasures, "0", numerator,
                            denominator);
                        break;
                    }
                    default:
                    {
                        var integerPart = dataRecipe.Quantity[0].ToString();
                        var numerator = dataRecipe.Quantity[2].ToString();
                        var denominator = dataRecipe.Quantity[4].ToString();
                        InsertMeasuresWhitIngredient(context, idRecipe, idMeasures, integerPart, numerator,
                            denominator);
                        break;
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"{dataRecipe.NameIngredients} {path}");
        }
    }
    
    private static void InsertMeasuresWhitIngredient(DbContext context,int idRecipe ,int ingredientIdMeasure, string integerPart,string numerator, string denominator)
    {
        context.Add(new RecipeMeasure
        {
            RecipeId = idRecipe,
            IngredientMeasureId = ingredientIdMeasure,
            IntegerPart = int.Parse(integerPart),
            Numerator = int.Parse(numerator),
            Denominator = int.Parse(denominator)
        });
    }
}