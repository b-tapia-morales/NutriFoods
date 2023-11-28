using Ardalis.SmartEnum;

namespace Domain.Enum;

public class DishTypes : SmartEnum<DishTypes>, IEnum<DishTypes, DishToken>
{
    public static readonly DishTypes None =
        new(nameof(None), (int)DishToken.None, "");

    public static readonly DishTypes Entree =
        new(nameof(Entree), (int)DishToken.Entree, "Plato de entrada");

    public static readonly DishTypes Drink =
        new(nameof(Drink), (int)DishToken.Drink, "Bebida");

    public static readonly DishTypes Salad =
        new(nameof(Salad), (int)DishToken.Salad, "Ensalada");

    public static readonly DishTypes Bread =
        new(nameof(Bread), (int)DishToken.Bread, "Pan");

    public static readonly DishTypes MainDish =
        new(nameof(MainDish), (int)DishToken.MainDish, "Plato principal");

    public static readonly DishTypes Dessert =
        new(nameof(Dessert), (int)DishToken.Dessert, "Postre");

    public static readonly DishTypes Pastry =
        new(nameof(Pastry), (int)DishToken.Pastry, "Repostería");

    public static readonly DishTypes Sauce =
        new(nameof(Sauce), (int)DishToken.Sauce, "Salsa");

    public static readonly DishTypes Sandwich =
        new(nameof(Sandwich), (int)DishToken.Sandwich, "Sándwich");

    public static readonly DishTypes Soup =
        new(nameof(Soup), (int)DishToken.Soup, "Sopa");

    public static readonly DishTypes Vegetarian =
        new(nameof(Vegetarian), (int)DishToken.Vegetarian, "Vegetariano");

    public static readonly DishTypes Vegan =
        new(nameof(Vegan), (int)DishToken.Vegan, "Vegano");

    private DishTypes(string name, int value, string readableName) : base(name, value) => ReadableName = readableName;

    public string ReadableName { get; }
}

public enum DishToken
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