// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable ClassNeverInstantiated.Global

using CsvHelper.Configuration;

namespace NutrientRetrieval.Mapping.Ingredient;

public sealed class IngredientMapping : ClassMap<IngredientRow>
{
    public IngredientMapping()
    {
        Map(p => p.NutriFoodsId).Index(0);
        Map(p => p.NutriFoodsName).Index(1);
        Map(p => p.FoodDataCentralId).Index(2).Optional();
    }
}