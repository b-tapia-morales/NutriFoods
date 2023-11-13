using NutrientRetrieval.Mapping.Ingredient;
using Utils.Csv;

namespace NutrientRetrieval.Tests;

public class IngredientInsertion
{
    private const string RelativePath = @"..\..\..\..\NutrientRetrieval\Files\IngredientIDs.csv";
    private static readonly string BasePath = Directory.GetParent(Directory.GetCurrentDirectory())!.FullName;
    private static readonly string AbsolutePath = Path.Combine(BasePath, RelativePath);

    [Fact]
    public void AllIdsAreUnique()
    {
        var rows = RowRetrieval.RetrieveRows<IngredientRow, IngredientMapping>(AbsolutePath).ToList();
        var nutriFoods = rows.Select(e => e.NutriFoodsId).ToList();
        Assert.True(nutriFoods.Count == nutriFoods.Distinct().Count());
    }

    [Fact]
    public void AllNamesAreUnique()
    {
        var rows = RowRetrieval.RetrieveRows<IngredientRow, IngredientMapping>(AbsolutePath).ToList();
        var nutriFoods = rows.Select(e => e.NutriFoodsName).ToList();
        Assert.True(nutriFoods.Count == nutriFoods.Distinct().Count());
    }
}