using Ardalis.SmartEnum;

namespace Domain.Enum;

public class FoodGroup : SmartEnum<FoodGroup>, IHierarchicalEnum<FoodGroup, FoodGroupToken>
{
    public static readonly FoodGroup None =
        new(nameof(None), (int)FoodGroupToken.None, "", false, null);

    public static readonly FoodGroup Fruits =
        new(nameof(Fruits), (int)FoodGroupToken.Fruits, "Frutas", true, null);

    public static readonly FoodGroup Vegetables =
        new(nameof(Vegetables), (int)FoodGroupToken.Vegetables, "Vegetales", true, null);

    public static readonly FoodGroup Grains =
        new(nameof(Grains), (int)FoodGroupToken.Grains, "Granos", true, null);

    public static readonly FoodGroup ProteinFoods =
        new(nameof(ProteinFoods), (int)FoodGroupToken.ProteinFoods, "Alimentos Proteicos", true, null);

    public static readonly FoodGroup Dairy =
        new(nameof(Dairy), (int)FoodGroupToken.Dairy, "Lácteos", true, null);

    public static readonly FoodGroup Other =
        new(nameof(Other), (int)FoodGroupToken.Other, "Otros", true, null);

    public static readonly FoodGroup WholeFruit =
        new(nameof(WholeFruit), (int)FoodGroupToken.WholeFruit, "Fruta Entera", false, Fruits);

    public static readonly FoodGroup FruitJuice =
        new(nameof(FruitJuice), (int)FoodGroupToken.FruitJuice, "Jugo de Fruta", false, Fruits);

    public static readonly FoodGroup DarkGreenVegetables =
        new(nameof(DarkGreenVegetables), (int)FoodGroupToken.DarkGreenVegetables, "Vegetales verdes", false,
            Vegetables);

    public static readonly FoodGroup RedAndOrangeVegetables =
        new(nameof(RedAndOrangeVegetables), (int)FoodGroupToken.RedAndOrangeVegetables, "Vegetales rojos", false,
            Vegetables);

    public static readonly FoodGroup Legumes =
        new(nameof(Legumes), (int)FoodGroupToken.Legumes, "Legumbres", false, Vegetables);

    public static readonly FoodGroup StarchyVegetables =
        new(nameof(StarchyVegetables), (int)FoodGroupToken.StarchyVegetables, "Vegetales con almidón", false,
            Vegetables);

    public static readonly FoodGroup OtherVegetables =
        new(nameof(OtherVegetables), (int)FoodGroupToken.OtherVegetables, "Otros Vegetales", false, Vegetables);

    public static readonly FoodGroup WholeGrains =
        new(nameof(WholeGrains), (int)FoodGroupToken.WholeGrains, "Granos Enteros", false, Grains);

    public static readonly FoodGroup RefinedGrains =
        new(nameof(RefinedGrains), (int)FoodGroupToken.RefinedGrains, "Granos Refinados", false, Grains);

    public static readonly FoodGroup Seafood =
        new(nameof(Seafood), (int)FoodGroupToken.Seafood, "Pescados y Mariscos", false, ProteinFoods);

    public static readonly FoodGroup MeatPoultryAndEggs =
        new(nameof(MeatPoultryAndEggs), (int)FoodGroupToken.MeatPoultryAndEggs, "Carnes, Aves y Huevos", false,
            ProteinFoods);

    public static readonly FoodGroup NutsSeedsAndSoy =
        new(nameof(NutsSeedsAndSoy), (int)FoodGroupToken.NutsSeedsAndSoy, "Frutos secos, Semillas y Soya", false,
            ProteinFoods);

    public static readonly FoodGroup MilkAndYogurt =
        new(nameof(MilkAndYogurt), (int)FoodGroupToken.MilkAndYogurt, "Leche y Yogurt", false, Dairy);

    public static readonly FoodGroup Sugars =
        new(nameof(Sugars), (int)FoodGroupToken.Sugars, "Azúcares", false, Other);

    public static readonly FoodGroup Salts =
        new(nameof(Salts), (int)FoodGroupToken.Salts, "Sales", false, Other);

    public static readonly FoodGroup FatsAndOils =
        new(nameof(FatsAndOils), (int)FoodGroupToken.FatsAndOils, "Aceites y grasas", false, Other);

    public static readonly FoodGroup DressingsAndSauces =
        new(nameof(DressingsAndSauces), (int)FoodGroupToken.DressingsAndSauces, "Aderezos y salsas", false, Other);

    public static readonly FoodGroup SeasoningsAndSpices =
        new(nameof(SeasoningsAndSpices), (int)FoodGroupToken.SeasoningsAndSpices, "Condimentos y especias", false, Other);

    public static readonly FoodGroup Algae =
        new(nameof(Algae), (int)FoodGroupToken.Algae, "Algas", false, Other);

    public static readonly FoodGroup Cheese =
        new(nameof(Cheese), (int)FoodGroupToken.Cheese, "Queso", false, Dairy);

    public static readonly FoodGroup Fish =
        new(nameof(Fish), (int)FoodGroupToken.Fish, "Pescados", false, Seafood);

    public static readonly FoodGroup Shellfish =
        new(nameof(Shellfish), (int)FoodGroupToken.Shellfish, "Mariscos", false, Seafood);

    public static readonly FoodGroup Meat =
        new(nameof(Meat), (int)FoodGroupToken.Meat, "Carnes", false, MeatPoultryAndEggs);

    public static readonly FoodGroup Poultry =
        new(nameof(Poultry), (int)FoodGroupToken.Poultry, "Aves", false, MeatPoultryAndEggs);

    public static readonly FoodGroup Eggs =
        new(nameof(Eggs), (int)FoodGroupToken.Eggs, "Huevos", false, MeatPoultryAndEggs);

    public static readonly FoodGroup Nuts =
        new(nameof(Nuts), (int)FoodGroupToken.Nuts, "Frutos secos", false, NutsSeedsAndSoy);

    public static readonly FoodGroup Seeds =
        new(nameof(Seeds), (int)FoodGroupToken.Seeds, "Semillas", false, NutsSeedsAndSoy);

    public static readonly FoodGroup Soy =
        new(nameof(Soy), (int)FoodGroupToken.Soy, "Soya", false, NutsSeedsAndSoy);

    public static readonly FoodGroup Milk =
        new(nameof(Milk), (int)FoodGroupToken.Milk, "Leche", false, MilkAndYogurt);

    public static readonly FoodGroup Yogurt =
        new(nameof(Yogurt), (int)FoodGroupToken.Yogurt, "Yogurt", false, MilkAndYogurt);

    public static readonly FoodGroup Sweets =
        new(nameof(Sweets), (int)FoodGroupToken.Sweets, "Dulces", false, Sugars);

    public static readonly FoodGroup SaltySnacks =
        new(nameof(SaltySnacks), (int)FoodGroupToken.SaltySnacks, "Bocadillos Salados", false, Salts);

    private FoodGroup(string name, int value, string readableName, bool isTopCategory, FoodGroup? category) :
        base(name, value)
    {
        ReadableName = readableName;
        IsTopCategory = isTopCategory;
        Category = category;
    }

    public string ReadableName { get; }
    public bool IsTopCategory { get; }
    public FoodGroup? Category { get; }

    public static IReadOnlyCollection<FoodGroup> Values() => List;
}

public enum FoodGroupToken
{
    None,
    Fruits,
    Vegetables,
    Grains,
    ProteinFoods,
    Dairy,
    Other,
    WholeFruit,
    FruitJuice,
    DarkGreenVegetables,
    RedAndOrangeVegetables,
    Legumes,
    StarchyVegetables,
    OtherVegetables,
    WholeGrains,
    RefinedGrains,
    Seafood,
    MeatPoultryAndEggs,
    NutsSeedsAndSoy,
    MilkAndYogurt,
    Sugars,
    Salts,
    FatsAndOils,
    DressingsAndSauces,
    SeasoningsAndSpices,
    Algae,
    Cheese,
    Fish,
    Shellfish,
    Meat,
    Poultry,
    Eggs,
    Nuts,
    Seeds,
    Soy,
    Milk,
    Yogurt,
    Sweets,
    SaltySnacks
}