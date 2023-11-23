// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable ClassNeverInstantiated.Global

using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Domain.Enum;

namespace NutrientRetrieval.Mapping.Statistics;

public sealed class AveragesMapping : ClassMap<AveragesRow>
{
    public AveragesMapping()
    {
        Map(p => p.MealType).Index(0);
        Map(p => p.Energy).Index(1);
        Map(p => p.Carbohydrates).Index(2);
        Map(p => p.FattyAcids).Index(3);
        Map(p => p.Proteins).Index(4);
    }
}