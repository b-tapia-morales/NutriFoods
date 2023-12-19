// ReSharper disable NonReadonlyMemberInGetHashCode

using Domain.Enum;
using Newtonsoft.Json;
using static System.StringComparison;
using static Domain.Enum.NutrientExtensions;

namespace API.Dto;

public sealed class RecipeDto : IEquatable<RecipeDto>, IEqualityComparer<RecipeDto>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string Url { get; set; } = null!;
    public int Portions { get; set; }
    public int? Time { get; set; }
    public string? Difficulty { get; set; }
    public List<string> MealTypes { get; set; } = null!;
    public List<string> DishTypes { get; set; } = null!;
    public List<RecipeMeasureDto> Measures { get; set; } = null!;
    public List<RecipeQuantityDto> Quantities { get; set; } = null!;
    public List<RecipeStepDto> Steps { get; set; } = null!;
    public List<NutritionalValueDto> Nutrients { get; set; } = null!;
    [JsonIgnore] public IReadOnlyDictionary<string, NutritionalValueDto> NutrientDict { get; set; } = null!;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj))
            return true;
        return !ReferenceEquals(null, obj) && obj.GetType() == GetType() && Equals((RecipeDto)obj);
    }

    public bool Equals(RecipeDto? other)
    {
        if (ReferenceEquals(this, other))
            return true;
        return !ReferenceEquals(null, other) && string.Equals(Url, other.Url, InvariantCultureIgnoreCase);
    }

    public bool Equals(RecipeDto? x, RecipeDto? y) => !ReferenceEquals(null, x) && x.Equals(y);

    public override int GetHashCode() => Url.ToLower().GetHashCode();

    public int GetHashCode(RecipeDto recipe) => recipe.Url.ToLower().GetHashCode();

    public static bool operator ==(RecipeDto? x, RecipeDto? y) => !ReferenceEquals(null, x) && x.Equals(y);

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

    public static void FilterTargetNutrients(this RecipeDto recipe)
    {
        var targetNutrients =
            new HashSet<Nutrients>(recipe.Nutrients.Select(e => IEnum<Nutrients, NutrientToken>.ToValue(e.Nutrient)));
        recipe.Nutrients.RemoveAll(e =>
            !targetNutrients.Contains(IEnum<Nutrients, NutrientToken>.ToValue(e.Nutrient)));
    }
}