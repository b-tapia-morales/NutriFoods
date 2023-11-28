using System.Collections.Immutable;
using Domain.Enum;
using NutrientRetrieval.Mapping.Nutrient;
using Utils.Csv;
using Utils.Enumerable;
using Xunit.Abstractions;
using static System.StringComparison;
using static Utils.Csv.DelimiterToken;

namespace NutrientRetrieval.Tests;

public class NutrientMappingTest(ITestOutputHelper output)
{
    private const string RelativePath = @"..\..\..\..\NutrientRetrieval\Files\NutrientIDs.csv";
    private static readonly string BasePath = AppDomain.CurrentDomain.BaseDirectory;
    private static readonly string AbsolutePath = Path.Combine(BasePath, RelativePath);

    [Fact]
    public void AllNamesAreUnique()
    {
        var rows = CsvUtils.RetrieveRows<NutrientRow, NutrientMapping>(AbsolutePath, Semicolon, true)
            .ToList();
        var nutriFoods = rows.Select(e => e.NutriFoodsName);
        var foodDataCentral = rows.Select(e => e.NutriFoodsName);
        Assert.True(nutriFoods.Count() == foodDataCentral.Count());
    }

    [Fact]
    public void AllIdsAreUnique()
    {
        var rows = CsvUtils.RetrieveRows<NutrientRow, NutrientMapping>(AbsolutePath, Semicolon, true)
            .ToList();
        var nutriFoods = rows.Select(e => e.NutriFoodsId).ToList();
        var foodDataCentral = rows.Select(e => e.NutriFoodsId).ToList();
        Assert.True(nutriFoods.Count == nutriFoods.Distinct().Count());
        Assert.True(foodDataCentral.Count == foodDataCentral.Distinct().Count());
    }

    [Fact]
    public void AllIdsMatch()
    {
        var nutrifoods = CsvUtils.RetrieveRows<NutrientRow, NutrientMapping>(AbsolutePath, Semicolon, true)
            .ToDictionary(e => e.NutriFoodsId, e => e.NutriFoodsName);
        var enums = IEnum<Nutrients, NutrientToken>.Values.ToDictionary(e => e.Value, e => e.ReadableName);
        var notMatching = new Dictionary<int, string>();
        foreach (var (nutrifoodsId, nutrifoodsName) in nutrifoods)
        {
            if (enums.TryGetValue(nutrifoodsId, out var enumName) &&
                !string.Equals(nutrifoodsName, enumName, InvariantCultureIgnoreCase))
                notMatching.Add(nutrifoodsId, nutrifoodsName);
        }

        output.WriteLine(notMatching.ToJoinedString());
        Assert.True(notMatching.Count == 0);
    }

    [Fact]
    public void AllIdsAreFullyContained()
    {
        var rows = CsvUtils.RetrieveRows<NutrientRow, NutrientMapping>(AbsolutePath, Semicolon, true)
            .ToList();
        var nutriFoodsIds = rows.Select(e => e.NutriFoodsId).ToImmutableHashSet();
        var enums = IEnum<Nutrients, NutrientToken>.Values;
        var enumsIds = enums.Select(e => e.Value).ToImmutableHashSet();
        output.WriteLine(enums.Select(e => $"{e.Value} : {e.ReadableName}{Environment.NewLine}")
            .ToJoinedString(string.Empty));
        var exceptA = nutriFoodsIds.Except(enumsIds);
        var exceptB = enumsIds.Except(nutriFoodsIds);
        output.WriteLine(exceptA.ToJoinedString());
        output.WriteLine(exceptB.ToJoinedString());
        Assert.True(exceptA.Count == 0);
    }

    [Fact]
    public void AllNamesAreInEnum()
    {
        var names = CsvUtils.RetrieveRows<NutrientRow, NutrientMapping>(AbsolutePath, Semicolon, true)
            .Select(e => e.NutriFoodsName);
        var keys = IEnum<Nutrients, NutrientToken>.TokenDictionary.Values.Select(e => e.ReadableName)
            .ToImmutableHashSet();
        Assert.All(names, e => Assert.Contains(e, keys));
    }
}