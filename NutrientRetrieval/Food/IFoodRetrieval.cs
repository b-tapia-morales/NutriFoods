using Domain.Models;
using Microsoft.EntityFrameworkCore;
using NutrientRetrieval.Dictionaries;
using NutrientRetrieval.Request;
using Utils.Csv;
using static Utils.Csv.DelimiterToken;

namespace NutrientRetrieval.Food;

public interface IFoodRetrieval<in T> where T : class, IFood
{
    public void RetrieveFromApi(string connectionString, string format)
    {
        var directory = Directory.GetParent(Directory.GetCurrentDirectory())!.FullName;
        var path = Path.Combine(directory, "NutrientRetrieval", "Files", "NutrientIDs.csv");
        var options = new DbContextOptionsBuilder<NutrifoodsDbContext>()
            .UseNpgsql(connectionString)
            .Options;
        using var context = new NutrifoodsDbContext(options);
        var nutrientsDictionary = RowRetrieval.RetrieveRows<NutrientRow, NutrientMapping>(path, Semicolon, true)
            .ToDictionary(e => e.FoodDataCentralId, e => e.NutriFoodsId);
        var foodsDictionary = DataCentral.RetrieveByList<T>(format).Result.ToDictionary(e => e.Key, e => e.Value);
        foreach (var pair in foodsDictionary)
        {
            InsertNutrients(context, nutrientsDictionary, pair.Key, pair.Value);
            //InsertMeasures(context, pair.Key, pair.Value);
        }

        context.SaveChanges();
    }

    void InsertNutrients(NutrifoodsDbContext context, IReadOnlyDictionary<string, int> dictionary, int ingredientId,
        T food);

    void InsertMeasures(NutrifoodsDbContext context, int ingredientId, T food);
}