using Ardalis.SmartEnum;

namespace Domain.Enum;

public class DishType : SmartEnum<DishType>, IEnum<DishType, DishTypeToken>
{
    public static readonly DishType None =
        new(nameof(None), (int)DishTypeToken.None, "");

    public static readonly DishType Entree =
        new(nameof(Entree), (int)DishTypeToken.Entree, "Plato de entrada");

    public static readonly DishType Drink =
        new(nameof(Drink), (int)DishTypeToken.Drink, "Bebida");

    public static readonly DishType Salad =
        new(nameof(Salad), (int)DishTypeToken.Salad, "Ensalada");

    public static readonly DishType Bread =
        new(nameof(Bread), (int)DishTypeToken.Bread, "Pan");

    public static readonly DishType MainDish =
        new(nameof(MainDish), (int)DishTypeToken.MainDish, "Plato principal");

    public static readonly DishType Dessert =
        new(nameof(Dessert), (int)DishTypeToken.Dessert, "Postre");

    public static readonly DishType Pastry =
        new(nameof(Pastry), (int)DishTypeToken.Pastry, "Repostería");

    public static readonly DishType Sauce =
        new(nameof(Sauce), (int)DishTypeToken.Sauce, "Salsa");

    public static readonly DishType Sandwich =
        new(nameof(Sandwich), (int)DishTypeToken.Sandwich, "Sándwich");

    public static readonly DishType Soup =
        new(nameof(Soup), (int)DishTypeToken.Soup, "Sopa");

    public static readonly DishType Vegetarian =
        new(nameof(Vegetarian), (int)DishTypeToken.Vegetarian, "Vegetariano");

    public static readonly DishType Vegan =
        new(nameof(Vegan), (int)DishTypeToken.Vegan, "Vegano");

    private DishType(string name, int value, string readableName) : base(name, value) => ReadableName = readableName;

    public string ReadableName { get; }
}

public enum DishTypeToken
{
    None,
    Entree,
    Drink,
    Salad,
    Bread,
    MainDish,
    Dessert,
    Pastry,
    Sauce,
    Sandwich,
    Soup,
    Vegetarian,
    Vegan
}