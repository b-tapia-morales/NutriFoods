using System.Collections.Immutable;
using Ardalis.SmartEnum;

namespace Utils.Enum;

public class MealTypeEnum : SmartEnum<MealTypeEnum>
{
    public static readonly MealTypeEnum None =
        new(nameof(None), (int) MealType.None, MealType.None, "Ninguno");

    public static readonly MealTypeEnum Breakfast =
        new(nameof(Breakfast), (int) MealType.Breakfast, MealType.Breakfast, "Desayuno");

    public static readonly MealTypeEnum Lunch =
        new(nameof(Lunch), (int) MealType.Lunch, MealType.Lunch, "Almuerzo");

    public static readonly MealTypeEnum Dinner =
        new(nameof(Dinner), (int) MealType.Dinner, MealType.Dinner, "Cena");

    public static readonly MealTypeEnum Snack =
        new(nameof(Snack), (int) MealType.Snack, MealType.Snack, "Merienda");

    private static readonly IDictionary<MealType, MealTypeEnum> TokenDictionary =
        new Dictionary<MealType, MealTypeEnum>
        {
            {MealType.None, None},
            {MealType.Breakfast, Breakfast},
            {MealType.Lunch, Lunch},
            {MealType.Dinner, Dinner},
            {MealType.Snack, Snack},
        }.ToImmutableDictionary();

    private static readonly IDictionary<string, MealTypeEnum> ReadableNameDictionary = TokenDictionary
        .ToImmutableDictionary(e => e.Value.ReadableName, e => e.Value, StringComparer.InvariantCultureIgnoreCase);

    public static IReadOnlyCollection<MealTypeEnum> Values { get; } =
        TokenDictionary.Values.OrderBy(e => e.Value).ToList();

    public static IReadOnlyCollection<MealTypeEnum> NonNullValues { get; } =
        TokenDictionary.Values.OrderBy(e => e.Value).Skip(1).ToList();

    public MealTypeEnum(string name, int value, MealType token, string readableName) : base(name, value)
    {
        Token = token;
        ReadableName = readableName;
    }

    public MealType Token { get; }
    public string ReadableName { get; }

    public static MealTypeEnum? FromReadableName(string name) =>
        ReadableNameDictionary.ContainsKey(name) ? ReadableNameDictionary[name] : null;

    public static MealTypeEnum FromToken(MealType token) => TokenDictionary[token];
}

public enum MealType
{
    None = 0,
    Breakfast = 1,
    Lunch = 2,
    Dinner = 3,
    Snack = 4
}