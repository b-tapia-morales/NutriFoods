using Ardalis.SmartEnum;
using static Domain.Enum.FoodGroups;

namespace Domain.Enum;

public class Diets : SmartEnum<Diets>, IEnum<Diets, DietToken>
{
    public static readonly Diets None =
        new(nameof(None), (int)DietToken.None, string.Empty, Array.Empty<FoodGroups>());

    public static readonly Diets Vegetarian =
        new(nameof(Vegetarian), (int)DietToken.Vegetarian, "Vegano",
            new[] { Fish, Shellfish, Meat, Poultry, Eggs, Milk, Yogurt, Cheese });

    public static readonly Diets OvoVegetarian =
        new(nameof(OvoVegetarian), (int)DietToken.OvoVegetarian, "Ovovegetariano",
            new[] { Fish, Shellfish, Meat, Poultry, Milk, Yogurt, Cheese });

    public static readonly Diets LactoVegetarian =
        new(nameof(LactoVegetarian), (int)DietToken.LactoVegetarian, "Lactovegetariano",
            new[] { Fish, Shellfish, Meat, Poultry, Eggs });

    public static readonly Diets OvoLactoVegetarian =
        new(nameof(OvoLactoVegetarian), (int)DietToken.OvoLactoVegetarian, "Ovolactovegetariano",
            new[] { Fish, Shellfish, Meat, Poultry });

    public static readonly Diets Vegan =
        new(nameof(Vegan), (int)DietToken.Vegan, "Vegano", Vegetarian.InconsumableGroups);

    public static readonly Diets Pollotarian =
        new(nameof(Pollotarian), (int)DietToken.Pollotarian, "Pollotariano",
            new[] { Fish, Shellfish, Meat });

    public static readonly Diets Pescetarian =
        new(nameof(Pescetarian), (int)DietToken.Pescetarian, "Pescetariano",
            new[] { Meat, Poultry });

    public static readonly Diets PolloPescetarian =
        new(nameof(PolloPescetarian), (int)DietToken.PolloPescetarian, "Pollopescetariano",
            new[] { Meat });

    private Diets(string name, int value, string readableName, IReadOnlyCollection<FoodGroups> inconsumableGroups) :
        base(name, value)
    {
        ReadableName = readableName;
        InconsumableGroups = inconsumableGroups;
    }

    public string ReadableName { get; }
    public IReadOnlyCollection<FoodGroups> InconsumableGroups { get; }
}

public enum DietToken
{
    None,
    Vegetarian,
    OvoVegetarian,
    LactoVegetarian,
    OvoLactoVegetarian,
    Vegan,
    Pollotarian,
    Pescetarian,
    PolloPescetarian,
}