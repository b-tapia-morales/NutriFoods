using Domain.Enum;
using Xunit.Abstractions;

namespace Domain.Tests;

public sealed class EnumsTest(ITestOutputHelper output)
{
    [Fact]
    public void ValuesPreserveOrder()
    {
        var values = IEnum<Regions, RegionToken>.Values();
        output.WriteLine(string.Join(", ", values));
        Assert.DoesNotContain(false, values.Zip(values.Skip(1), (a, b) => a.Value < b.Value));
    }

    [Fact]
    public void ValuesSkipNulls()
    {
        var values = IEnum<DishTypes, DishToken>.NonNullValues();
        output.WriteLine(string.Join(", ", values));
        Assert.All(values, e => Assert.NotEqual(e, DishTypes.None));
    }

    [Fact]
    public void TokenDictionaryPreservesOrder()
    {
        var dictionary = IEnum<MealTypes, MealToken>.TokenDictionary();
        output.WriteLine(string.Join(", ", dictionary));
        Assert.DoesNotContain(false, dictionary.Zip(dictionary.Skip(1), (a, b) => a.Value < b.Value));
    }

    [Fact]
    public void ValuesAreFilterable()
    {
        var values = IHierarchicalEnum<Nutrients, NutrientToken>.ByCategory(Nutrients.Minerals);
        output.WriteLine(string.Join(", ", values));
        Assert.All(values, e => Assert.Equal(e.Category, Nutrients.Minerals));
        Assert.DoesNotContain(false, values.Zip(values.Skip(1), (a, b) => a.Value < b.Value));
    }
}