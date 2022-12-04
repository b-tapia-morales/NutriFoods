using System.Collections.Immutable;
using Ardalis.SmartEnum;

namespace Utils.Enum;

public class DishTypeEnum : SmartEnum<DishTypeEnum>
{
    public static readonly DishTypeEnum None =
        new(nameof(None), (int) DishType.None, DishType.None, "Ninguno");

    public static readonly DishTypeEnum Entree =
        new(nameof(Entree), (int) DishType.Entree, DishType.Entree, "Plato de entrada");

    public static readonly DishTypeEnum MainDish =
        new(nameof(MainDish), (int) DishType.MainDish, DishType.MainDish, "Plato principal");

    public static readonly DishTypeEnum Dessert =
        new(nameof(Dessert), (int) DishType.Dessert, DishType.Dessert, "Postre");

    public static readonly DishTypeEnum Appetizer =
        new(nameof(Appetizer), (int) DishType.Appetizer, DishType.Appetizer, "Aperitivo");

    public static readonly DishTypeEnum Beverage =
        new(nameof(Beverage), (int) DishType.Beverage, DishType.Beverage, "Bebida");

    public static readonly DishTypeEnum Salad =
        new(nameof(Salad), (int) DishType.Salad, DishType.Salad, "Ensalada");

    public static readonly DishTypeEnum Soup =
        new(nameof(Soup), (int) DishType.Soup, DishType.Soup, "Sopa");

    public static readonly DishTypeEnum Pastries =
        new(nameof(Pastries), (int) DishType.Pastries, DishType.Pastries, "Repostería");

    public static readonly DishTypeEnum Bread =
        new(nameof(Bread), (int) DishType.Bread, DishType.Bread, "Pan");

    public static readonly DishTypeEnum Sandwich =
        new(nameof(Sandwich), (int) DishType.Sandwich, DishType.Sandwich, "Sándwich");

    private static readonly IDictionary<DishType, DishTypeEnum> TokenDictionary =
        new Dictionary<DishType, DishTypeEnum>
        {
            {DishType.None, None},
            {DishType.Entree, Entree},
            {DishType.MainDish, MainDish},
            {DishType.Dessert, Dessert},
            {DishType.Appetizer, Appetizer},
            {DishType.Beverage, Beverage},
            {DishType.Salad, Salad},
            {DishType.Soup, Soup},
            {DishType.Pastries, Pastries},
            {DishType.Bread, Bread},
            {DishType.Sandwich, Sandwich}
        }.ToImmutableDictionary();
    
    private static readonly IDictionary<string, DishTypeEnum> ReadableNameDictionary = TokenDictionary
        .ToImmutableDictionary(e => e.Value.ReadableName, e => e.Value, StringComparer.InvariantCultureIgnoreCase);
    
    public static IReadOnlyCollection<DishTypeEnum> Values { get; } =
        TokenDictionary.Values.OrderBy(e => e.Value).ToList();

    public static IReadOnlyCollection<DishTypeEnum> NonNullValues { get; } =
        TokenDictionary.Values.OrderBy(e => e.Value).Skip(1).ToList();

    public DishTypeEnum(string name, int value, DishType token, string readableName) : base(name, value)
    {
        Token = token;
        ReadableName = readableName;
    }

    public DishType Token { get; }
    public string ReadableName { get; }
    
    public static DishTypeEnum? FromReadableName(string name) =>
        ReadableNameDictionary.ContainsKey(name) ? ReadableNameDictionary[name] : null;

    public static DishTypeEnum FromToken(DishType token) => TokenDictionary[token];
}

public enum DishType
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