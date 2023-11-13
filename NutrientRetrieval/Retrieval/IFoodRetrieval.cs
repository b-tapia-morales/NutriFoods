using Domain.Enum;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using NutrientRetrieval.Food;
using NutrientRetrieval.Mapping.Nutrient;
using NutrientRetrieval.Request;
using Utils.Csv;
using static Utils.Csv.DelimiterToken;

namespace NutrientRetrieval.Retrieval;

public interface IFoodRetrieval<in TFood, TNutrient>
    where TFood : class, IFood<TNutrient>
    where TNutrient : class, IFoodNutrient
{
    const string ConnectionString =
        "Host=localhost;Database=nutrifoods_db;Username=nutrifoods_dev;Password=MVmYneLqe91$";
    
    const string ProjectDirectory = "NutrientRetrieval";
    const string FileDirectory = "Files";
    const string FileName = "NutrientIDs.csv";

    string Format { get; }

    void RetrieveFromApi()
    {
        var baseDirectory = Directory.GetParent(Directory.GetCurrentDirectory())!.FullName;
        var absolutePath = Path.Combine(baseDirectory, ProjectDirectory, FileDirectory, FileName);
        var options = new DbContextOptionsBuilder<NutrifoodsDbContext>()
            .UseNpgsql(ConnectionString)
            .Options;
        using var context = new NutrifoodsDbContext(options);
        var nutrientsDictionary = RowRetrieval.RetrieveRows<NutrientRow, NutrientMapping>(absolutePath, Semicolon, true)
            .ToDictionary(e => e.FoodDataCentralId, e => e.NutriFoodsId);
        var foodsDictionary = DataCentral.RetrieveByList<TFood, TNutrient>(Format).Result
            .ToDictionary(e => e.Key, e => e.Value);
        foreach (var pair in foodsDictionary)
        {
            InsertNutrients(context, nutrientsDictionary, pair.Key, pair.Value);
        }

        context.SaveChanges();
    }

    void InsertMeasures(NutrifoodsDbContext context, int ingredientId, TFood food);

    void InsertNutrients(NutrifoodsDbContext context, IReadOnlyDictionary<string, int> dictionary, int ingredientId,
        TFood food)
    {
        if (food.FoodNutrients.Length == 0) return;

        foreach (var foodNutrient in food.FoodNutrients)
        {
            var fdcNutrientId = foodNutrient.Number;
            if (!dictionary.ContainsKey(fdcNutrientId))
                continue;

            var nutrientId = dictionary[fdcNutrientId];
            var nutrient = Nutrients.FromValue(nutrientId);
            context.IngredientNutrients.Add(new IngredientNutrient
            {
                IngredientId = ingredientId,
                Nutrient = nutrient,
                Quantity = foodNutrient.Amount,
                Unit = nutrient.Unit
            });
        }
    }
}