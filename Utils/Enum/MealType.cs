using System.Collections.Immutable;
using Ardalis.SmartEnum;

namespace Utils.Enum;

public class MealType : SmartEnum<MealType>
{
    public static readonly MealType None =
        new(nameof(None), (int) MealTypeToken.None, "Ninguno");

    public static readonly MealType Breakfast =
        new(nameof(Breakfast), (int) MealTypeToken.Breakfast, "Desayuno");

    public static readonly MealType Lunch =
        new(nameof(Lunch), (int) MealTypeToken.Lunch, "Almuerzo");

    public static readonly MealType Dinner =
        new(nameof(Dinner), (int) MealTypeToken.Dinner, "Cena");
    
    public static readonly MealType Snack =
        new(nameof(Snack), (int) MealTypeToken.Snack, "Merienda");

    private static readonly IDictionary<MealTypeToken, MealType> TokenDictionary = new Dictionary<MealTypeToken, MealType>
    {
        {MealTypeToken.None, None},
        {MealTypeToken.Breakfast, Breakfast},
        {MealTypeToken.Lunch, Lunch},
        {MealTypeToken.Dinner, Dinner},
        {MealTypeToken.Snack, Snack},
    }.ToImmutableDictionary();

    private static readonly IDictionary<string, MealType> ReadableNameDictionary = TokenDictionary
        .ToImmutableDictionary(e => e.Value.ReadableName, e => e.Value, StringComparer.InvariantCultureIgnoreCase);

    public MealType(string name, int value, string readableName) : base(name, value) => ReadableName = readableName;

    private string ReadableName { get; }

    public static MealType FromReadableName(string name) => ReadableNameDictionary[name];

    public static MealType FromToken(MealTypeToken token) => TokenDictionary[token];
}

public enum MealTypeToken
{
    None = 0,
    Breakfast = 1,
    Lunch = 2,
    Dinner = 3,
    Snack = 4,
}