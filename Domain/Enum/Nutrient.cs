using Ardalis.SmartEnum;

namespace Domain.Enum;

public class Nutrient : SmartEnum<Nutrient>, IHierarchicalEnum<Nutrient, NutrientToken>
{
    public static readonly Nutrient None =
        new(nameof(None), (int)NutrientToken.None, "", "", false, null);

    public static readonly Nutrient Energy =
        new(nameof(Energy), (int)NutrientToken.Energy, "Energía", "", true, null);

    public static readonly Nutrient Carbohydrates =
        new(nameof(Carbohydrates), (int)NutrientToken.Carbohydrates, "Carbohidratos", "", true, null);

    public static readonly Nutrient CarbohydratesTotal =
        new(nameof(CarbohydratesTotal), (int)NutrientToken.CarbohydratesTotal, "Carbohidratos, Total", "", false,
            Carbohydrates);

    public static readonly Nutrient Fiber =
        new(nameof(Fiber), (int)NutrientToken.Fiber, "Fibra", "", false, CarbohydratesTotal);

    public static readonly Nutrient Sugars =
        new(nameof(Sugars), (int)NutrientToken.Sugars, "Azúcares", "", false, CarbohydratesTotal);

    public static readonly Nutrient Sucrose =
        new(nameof(Sucrose), (int)NutrientToken.Sucrose, "Sucrosa", "Sacarosa", false, Sugars);

    public static readonly Nutrient Glucose =
        new(nameof(Glucose), (int)NutrientToken.Glucose, "Glucosa", "", false, Sugars);

    public static readonly Nutrient Fructose =
        new(nameof(Fructose), (int)NutrientToken.Fructose, "Fructosa", "Levulosa", false, Sugars);

    public static readonly Nutrient Lactose =
        new(nameof(Lactose), (int)NutrientToken.Lactose, "Lactosa", "", false, Sugars);

    public static readonly Nutrient Maltose =
        new(nameof(Maltose), (int)NutrientToken.Maltose, "Maltosa", "", false, Sugars);

    public static readonly Nutrient Galactose =
        new(nameof(Galactose), (int)NutrientToken.Galactose, "Galactosa", "", false, Sugars);

    public static readonly Nutrient Starch =
        new(nameof(Starch), (int)NutrientToken.Starch, "Almidón", "Fécula", false, CarbohydratesTotal);

    public static readonly Nutrient Minerals =
        new(nameof(Minerals), (int)NutrientToken.Minerals, "Minerales", "", true, null);

    public static readonly Nutrient Calcium =
        new(nameof(Calcium), (int)NutrientToken.Calcium, "Calcio", "Ca", false, Minerals);

    public static readonly Nutrient Iron =
        new(nameof(Iron), (int)NutrientToken.Iron, "Hierro", "Fe", false, Minerals);

    public static readonly Nutrient Magnesium =
        new(nameof(Magnesium), (int)NutrientToken.Magnesium, "Magnesio", "Mg", false, Minerals);

    public static readonly Nutrient Phosphorus =
        new(nameof(Phosphorus), (int)NutrientToken.Phosphorus, "Fósforo", "P", false, Minerals);

    public static readonly Nutrient Potassium =
        new(nameof(Potassium), (int)NutrientToken.Potassium, "Potasio", "K", false, Minerals);

    public static readonly Nutrient Sodium =
        new(nameof(Sodium), (int)NutrientToken.Sodium, "Sodio", "Na", false, Minerals);

    public static readonly Nutrient Zinc =
        new(nameof(Zinc), (int)NutrientToken.Zinc, "Zinc", "Zn", false, Minerals);

    public static readonly Nutrient Copper =
        new(nameof(Copper), (int)NutrientToken.Copper, "Cobre", "Cu", false, Minerals);

    public static readonly Nutrient Manganese =
        new(nameof(Manganese), (int)NutrientToken.Manganese, "Manganeso", "Mn", false, Minerals);

    public static readonly Nutrient Selenium =
        new(nameof(Selenium), (int)NutrientToken.Selenium, "Selenio", "Se", false, Minerals);

    public static readonly Nutrient Fluoride =
        new(nameof(Fluoride), (int)NutrientToken.Fluoride, "Fluoruro", "F", false, Minerals);

    private Nutrient(string name, int value, string readableName, string optionalName,
        bool isTopCategory, Nutrient? category) : base(name, value)
    {
        ReadableName = readableName;
        OptionalName = optionalName;
        IsTopCategory = isTopCategory;
        Category = category;
    }

    public string ReadableName { get; }
    public string OptionalName { get; }
    public bool IsTopCategory { get; }
    public Nutrient? Category { get; }

    public static IReadOnlyCollection<Nutrient> Values() => List;
}

public enum NutrientToken
{
    None,
    Energy,
    Carbohydrates,
    CarbohydratesTotal,
    Fiber,
    Sugars,
    Sucrose,
    Glucose,
    Fructose,
    Lactose,
    Maltose,
    Galactose,
    Starch,
    Minerals,
    Calcium,
    Iron,
    Magnesium,
    Phosphorus,
    Potassium,
    Sodium,
    Zinc,
    Copper,
    Manganese,
    Selenium,
    Fluoride
}