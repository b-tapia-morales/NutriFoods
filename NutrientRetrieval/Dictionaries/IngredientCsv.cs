using CsvHelper.Configuration;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace NutrientRetrieval.Dictionaries;

public sealed class IngredientRow
{
    public int NutriFoodsId { get; set; } = 0;
    public string NutriFoodsName { get; set; } = null!;
    public int? FoodDataCentralId { get; set; } = 0;
}

public sealed class IngredientMapping : ClassMap<IngredientRow>
{
    public IngredientMapping()
    {
        Map(p => p.NutriFoodsId).Index(0);
        Map(p => p.NutriFoodsName).Index(1);
        Map(p => p.FoodDataCentralId).Index(2).Optional();
    }
}