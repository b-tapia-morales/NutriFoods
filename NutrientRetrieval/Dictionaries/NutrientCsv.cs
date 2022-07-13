using CsvHelper.Configuration;

namespace NutrientRetrieval.Dictionaries;

public sealed class NutrientRow
{
    public string FoodDataCentralName { get; set; } = null!;
    public string FoodDataCentralId { get; set; } = null!;
    public string NutriFoodsName { get; set; } = null!;
    public int NutriFoodsId { get; set; } = 0;
}

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