// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable ClassNeverInstantiated.Global

using CsvHelper.Configuration;

namespace NutrientRetrieval.Mapping.Nutrient;

public sealed class NutrientMapping : ClassMap<NutrientRow>
{
    public NutrientMapping()
    {
        Map(p => p.FoodDataCentralName).Index(0);
        Map(p => p.FoodDataCentralId).Index(1);
        Map(p => p.NutriFoodsName).Index(2).Optional();
        Map(p => p.NutriFoodsId).Index(3).Optional();
    }
}