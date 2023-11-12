using Domain.Enum;
using Xunit.Abstractions;

namespace Domain.Tests;

public sealed class EnumsTest
{
    private readonly ITestOutputHelper _output;

    public EnumsTest(ITestOutputHelper output) => _output = output;

    [Fact]
    public void ValuesPreserveOrder()
    {
        var values = IEnum<DishType, DishTypeToken>.Values();
        _output.WriteLine(string.Join(", ", values));
        Assert.DoesNotContain(false, values.Zip(values.Skip(1), (a, b) => a.Value < b.Value));
    }

    [Fact]
    public void ValuesSkipNulls()
    {
        var values = IEnum<DishType, DishTypeToken>.NonNullValues();
        _output.WriteLine(string.Join(", ", values));
        Assert.All(values, e => Assert.NotEqual(e, DishType.None));
    }

    [Fact]
    public void TokenDictionaryPreservesOrder()
    {
        var dictionary = IEnum<DishType, DishTypeToken>.TokenDictionary();
        _output.WriteLine(string.Join(", ", dictionary));
        Assert.DoesNotContain(false, dictionary.Zip(dictionary.Skip(1), (a, b) => a.Value < b.Value));
    }

    [Fact]
    public void ValuesAreFilterable()
    {
        var values = IHierarchicalEnum<Nutrient, NutrientToken>.ByCategory(Nutrient.Minerals);
        _output.WriteLine(string.Join(", ", values));
        Assert.All(values, e => Assert.Equal(e.Category, Nutrient.Minerals));
        Assert.DoesNotContain(false, values.Zip(values.Skip(1), (a, b) => a.Value < b.Value));
    }
}