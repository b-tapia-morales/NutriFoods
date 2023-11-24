using System.Collections.Immutable;
using Domain.Enum;
using NutrientRetrieval.Mapping.Statistics;
using Utils.Csv;

namespace API.ApplicationData;

public class ApplicationData : IApplicationData
{
    private const string ProjectDirectory = "NutrientRetrieval";
    private const string FileDirectory = "Files";
    private const string FileName = "NutrientAverages.csv";

    private static readonly string BaseDirectory =
        Directory.GetParent(Directory.GetCurrentDirectory())!.FullName;

    private static readonly string AbsolutePath =
        Path.Combine(BaseDirectory, ProjectDirectory, FileDirectory, FileName);

    public ApplicationData()
    {
        var rows =
            CsvUtils.RetrieveRows<AveragesRow, AveragesMapping>(AbsolutePath, DelimiterToken.Semicolon, true).ToList();
        CountDict = CreateDict(rows, e => e.Count);
        EnergyDict = CreateDict(rows, e => e.Energy);
        CarbohydratesDict = CreateDict(rows, e => e.Carbohydrates);
        FattyAcidsDict = CreateDict(rows, e => e.FattyAcids);
        ProteinsDict = CreateDict(rows, e => e.FattyAcids);
    }

    public IImmutableDictionary<MealTypes, int> CountDict { get; }
    public IImmutableDictionary<MealTypes, double> EnergyDict { get; }
    public IImmutableDictionary<MealTypes, double> CarbohydratesDict { get; }
    public IImmutableDictionary<MealTypes, double> FattyAcidsDict { get; }
    public IImmutableDictionary<MealTypes, double> ProteinsDict { get; }

    private static IImmutableDictionary<MealTypes, T> CreateDict<T>(IEnumerable<AveragesRow> rows,
        Func<AveragesRow, T> elementSelector) =>
        rows.ToImmutableDictionary(e => MealTypes.FromName(e.MealType), elementSelector);
}