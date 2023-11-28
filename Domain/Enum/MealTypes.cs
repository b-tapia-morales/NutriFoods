using Ardalis.SmartEnum;

namespace Domain.Enum;

public class MealTypes : SmartEnum<MealTypes>, IEnum<MealTypes, MealToken>
{
    public static readonly MealTypes None =
        new(nameof(None), (int)MealToken.None, "");

    public static readonly MealTypes Breakfast =
        new(nameof(Breakfast), (int)MealToken.Breakfast, "Desayuno");

    public static readonly MealTypes Lunch =
        new(nameof(Lunch), (int)MealToken.Lunch, "Almuerzo");

    public static readonly MealTypes Dinner =
        new(nameof(Dinner), (int)MealToken.Dinner, "Cena");

    public static readonly MealTypes Snack =
        new(nameof(Snack), (int)MealToken.Snack, "Merienda");

    private MealTypes(string name, int value, string readableName) : base(name, value) => ReadableName = readableName;

    public string ReadableName { get; }
}

public enum MealToken
{
    None,
    Breakfast,
    Lunch,
    Dinner,
    Snack
}