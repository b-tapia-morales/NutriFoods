using Domain.Enum;
using Utils.Enumerable;
using Xunit.Abstractions;

namespace Domain.Tests;

public sealed class EnumsTest(ITestOutputHelper output)
{
    [Fact]
    public void ValuesPreserveOrder()
    {
        var values = IEnum<Regions, RegionToken>.Values();
        output.WriteLine(values.ToJoinedString());
        Assert.DoesNotContain(false, values.Zip(values.Skip(1), (a, b) => a.Value < b.Value));
    }

    [Fact]
    public void ValuesSkipNulls()
    {
        var values = IEnum<DishTypes, DishToken>.NonNullValues();
        output.WriteLine(values.ToJoinedString());
        Assert.All(values, e => Assert.NotEqual(e, DishTypes.None));
    }

    [Fact]
    public void TokenDictionaryPreservesOrder()
    {
        var dictionary = IEnum<MealTypes, MealToken>.TokenDictionary();
        output.WriteLine(dictionary.ToJoinedString());
        Assert.DoesNotContain(false, dictionary.Zip(dictionary.Skip(1), (a, b) => a.Value < b.Value));
    }

    [Fact]
    public void ValuesAreFilterable()
    {
        var values = IHierarchicalEnum<Nutrients, NutrientToken>.ByCategory(Nutrients.Minerals);
        output.WriteLine(values.ToJoinedString());
        Assert.All(values, e => Assert.Equal(e.Category, Nutrients.Minerals));
        Assert.DoesNotContain(false, values.Zip(values.Skip(1), (a, b) => a.Value < b.Value));
    }
}