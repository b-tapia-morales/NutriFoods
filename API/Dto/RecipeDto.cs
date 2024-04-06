using Domain.Enum;
using Newtonsoft.Json;
using Utils.String;
using static Domain.Enum.NutrientExtensions;

namespace API.Dto;

public sealed class RecipeDto : IEquatable<RecipeDto>, IEqualityComparer<RecipeDto>
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public string Author { get; init; } = null!;
    public string Url { get; init; } = null!;
    public int Portions { get; init; }
    public int? Time { get; init; }
    public string? Difficulty { get; init; }
    public List<string> MealTypes { get; init; } = null!;
    public List<string> DishTypes { get; init; } = null!;
    public List<RecipeMeasureDto> Measures { get; init; } = null!;
    public List<RecipeQuantityDto> Quantities { get; init; } = null!;
    public List<RecipeStepDto> Steps { get; init; } = null!;
    public List<NutritionalValueDto> Nutrients { get; init; } = null!;
    [JsonIgnore] public IReadOnlyDictionary<string, NutritionalValueDto> NutrientDict { get; init; } = null!;

    public override bool Equals(object? obj) => 
        ReferenceEquals(this, obj) || obj is RecipeDto other && Equals(other);

    public bool Equals(RecipeDto? other)
    {
        if (ReferenceEquals(this, other))
            return true;
        if (ReferenceEquals(null, other))
            return false;
        return new Uri(Url).Equals(new Uri(other.Url)) &&
               string.Equals(Name.Standardize(), other.Name.Standardize(),
                   StringComparison.InvariantCultureIgnoreCase) &&
               string.Equals(Author.Standardize(), other.Author.Standardize(),
                   StringComparison.InvariantCultureIgnoreCase);
    }

    public bool Equals(RecipeDto? x, RecipeDto? y)
    {
        if (ReferenceEquals(x, y))
            return true;
        if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
            return false;
        return Equals(y);
    }

    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(Name.Standardize(), StringComparer.InvariantCultureIgnoreCase);
        hashCode.Add(Author.Standardize(), StringComparer.InvariantCultureIgnoreCase);
        hashCode.Add(new Uri(Url), EqualityComparer<Uri>.Default);
        return hashCode.ToHashCode();
    }

    public int GetHashCode(RecipeDto obj) => obj.GetHashCode();

    public static bool operator ==(RecipeDto? x, RecipeDto? y)
    {
        if (ReferenceEquals(x, y))
            return true;
        if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
            return false;
        return x.Equals(y);
    }

    public static bool operator !=(RecipeDto? x, RecipeDto? y) => !(x == y);
}

public static class RecipeExtensions
{
    public static IEnumerable<NutritionalValueDto> ToNutritionalValues(this IEnumerable<RecipeDto> recipes) =>
        recipes
            .SelectMany(e => e.Nutrients)
            .GroupBy(e => IEnum<Nutrients, NutrientToken>.ToValue(e.Nutrient))
            .Select(e => new NutritionalValueDto
            {
                Nutrient = e.Key.ReadableName,
                Quantity = e.Sum(x => x.Quantity),
                Unit = e.Key.Unit.ReadableName,
                DailyValue = e.Key.DailyValue == null ? null : e.Sum(x => x.Quantity) / e.Key.DailyValue
            });

    public static IEnumerable<MenuRecipeDto> ToMenus(this IEnumerable<RecipeDto> recipes) =>
        recipes
            .GroupBy(e => e.Url)
            .Select(e => new MenuRecipeDto
            {
                Recipe = e.First(),
                Portions = e.Count()
            });

    public static void FilterMacronutrients(this RecipeDto recipe) => recipe.Nutrients.RemoveAll(e =>
        !Macronutrients.Contains(IEnum<Nutrients, NutrientToken>.ToValue(e.Nutrient)));
}