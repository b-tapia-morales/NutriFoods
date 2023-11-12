using System.Collections.Immutable;
using System.Reflection;
using Domain.Enum;
using NutrientRetrieval.NutrientCalculation;
using Utils.Csv;
using static Utils.Csv.DelimiterToken;

namespace NutrientRetrieval.Tests;

public class NutrientMappingTest
{
    private const string RelativePath = @"..\..\..\..\NutrientRetrieval\Files\NutrientIDs.csv";
    private static readonly string BasePath = AppDomain.CurrentDomain.BaseDirectory;
    private static readonly string AbsolutePath = Path.Combine(BasePath, RelativePath);

    [Fact]
    public void AllNamesAreUnique()
    {
        var rows = RowRetrieval.RetrieveRows<NutrientRow, NutrientMapping>(AbsolutePath, Semicolon, true)
            .ToList();
        var nutriFoods = rows.Select(e => e.NutriFoodsName);
        var foodDataCentral = rows.Select(e => e.NutriFoodsName);
        Assert.True(nutriFoods.Count() == foodDataCentral.Count());
    }

    [Fact]
    public void AllIdsAreUnique()
    {
        var rows = RowRetrieval.RetrieveRows<NutrientRow, NutrientMapping>(AbsolutePath, Semicolon, true)
            .ToList();
        var nutriFoods = rows.Select(e => e.NutriFoodsId).ToList();
        var foodDataCentral = rows.Select(e => e.NutriFoodsId).ToList();
        Assert.True(nutriFoods.Count == nutriFoods.Distinct().Count());
        Assert.True(foodDataCentral.Count == foodDataCentral.Distinct().Count());
    }

    [Fact]
    public void AllNamesAreInEnum()
    {
        var names = RowRetrieval.RetrieveRows<NutrientRow, NutrientMapping>(AbsolutePath, Semicolon, true)
            .Select(e => e.NutriFoodsName);
        var keys = IEnum<Nutrient, NutrientToken>.TokenDictionary().Values.Select(e => e.ReadableName)
            .ToImmutableHashSet();
        Assert.All(names, e => Assert.Contains(e, keys));
    }
}