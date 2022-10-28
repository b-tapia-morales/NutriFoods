using System.Collections.Immutable;
using Ardalis.SmartEnum;

namespace Utils.Enum;

public class DishType : SmartEnum<DishType>
{
    public static readonly DishType None =
        new(nameof(None), (int) DishTypeToken.None, "Ninguno");

    public static readonly DishType Entree =
        new(nameof(Entree), (int) DishTypeToken.Entree, "Plato de entrada");

    public static readonly DishType MainDish =
        new(nameof(MainDish), (int) DishTypeToken.MainDish, "Plato principal");

    public static readonly DishType Dessert =
        new(nameof(Dessert), (int) DishTypeToken.Dessert, "Postre");

    public static readonly DishType Appetizer =
        new(nameof(Appetizer), (int) DishTypeToken.Appetizer, "Aperitivo");

    public static readonly DishType Beverage =
        new(nameof(Beverage), (int) DishTypeToken.Beverage, "Bebida");

    public static readonly DishType Salad =
        new(nameof(Salad), (int) DishTypeToken.Salad, "Ensalada");

    public static readonly DishType Soup =
        new(nameof(Soup), (int) DishTypeToken.Soup, "Sopa");

    public static readonly DishType Pastries =
        new(nameof(Pastries), (int) DishTypeToken.Pastries, "Repostería");

    public static readonly DishType Bread =
        new(nameof(Bread), (int) DishTypeToken.Bread, "Pan");

    public static readonly DishType Sandwich =
        new(nameof(Sandwich), (int) DishTypeToken.Sandwich, "Sándwich");

    private static readonly IDictionary<DishTypeToken, DishType> TokenDictionary =
        new Dictionary<DishTypeToken, DishType>
        {
            {DishTypeToken.None, None},
            {DishTypeToken.Entree, Entree},
            {DishTypeToken.MainDish, MainDish},
            {DishTypeToken.Dessert, Dessert},
            {DishTypeToken.Appetizer, Appetizer},
            {DishTypeToken.Beverage, Beverage},
            {DishTypeToken.Salad, Salad},
            {DishTypeToken.Soup, Soup},
            {DishTypeToken.Pastries, Pastries},
            {DishTypeToken.Bread, Bread},
            {DishTypeToken.Sandwich, Sandwich}
        }.ToImmutableDictionary();

    private static readonly IDictionary<string, DishType> ReadableNameDictionary = TokenDictionary
        .ToImmutableDictionary(e => e.Value.ReadableName, e => e.Value, StringComparer.InvariantCultureIgnoreCase);

    public DishType(string name, int value, string readableName) : base(name, value) => ReadableName = readableName;

    public string ReadableName { get; }

    public static DishType? FromReadableName(string name) =>
        ReadableNameDictionary.ContainsKey(name) ? ReadableNameDictionary[name] : null;

    public static DishType FromToken(DishTypeToken token) => TokenDictionary[token];
}

public enum DishTypeToken
{
    None = 0,
    Entree = 1,
    MainDish = 2,
    Dessert = 3,
    Appetizer = 4,
    Beverage = 5,
    Salad = 6,
    Soup = 7,
    Pastries = 8,
    Bread = 9,
    Sandwich = 10
}