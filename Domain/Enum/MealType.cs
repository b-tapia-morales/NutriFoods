using Ardalis.SmartEnum;

namespace Domain.Enum;

public class MealType : SmartEnum<MealType>, IEnum<MealType, MealTypeToken>
{
    public static readonly MealType None =
        new(nameof(None), (int)MealTypeToken.None, "");

    public static readonly MealType Breakfast =
        new(nameof(Breakfast), (int)MealTypeToken.Breakfast, "Desayuno");

    public static readonly MealType Lunch =
        new(nameof(Lunch), (int)MealTypeToken.Lunch, "Almuerzo");

    public static readonly MealType Dinner =
        new(nameof(Dinner), (int)MealTypeToken.Dinner, "Cena");

    public static readonly MealType Snack =
        new(nameof(Snack), (int)MealTypeToken.Snack, "Merienda");

    private MealType(string name, int value, string readableName) : base(name, value) => ReadableName = readableName;

    public string ReadableName { get; }

    public static IReadOnlyCollection<MealType> Values() => List;
}

public enum MealTypeToken
{
    None,
    Breakfast,
    Lunch,
    Dinner,
    Snack
}