using Ardalis.SmartEnum;

namespace Domain.Enum;

public class FoodGroup : SmartEnum<FoodGroup>, IHierarchicalEnum<FoodGroup, FoodGroupToken>
{
    public static readonly FoodGroup None =
        new(nameof(None), (int)FoodGroupToken.None, "", null);

    public static readonly FoodGroup Fruits =
        new(nameof(Fruits), (int)FoodGroupToken.Fruits, "Frutas", null);

    public static readonly FoodGroup Vegetables =
        new(nameof(Vegetables), (int)FoodGroupToken.Vegetables, "Vegetales", null);

    public static readonly FoodGroup Grains =
        new(nameof(Grains), (int)FoodGroupToken.Grains, "Granos", null);

    public static readonly FoodGroup ProteinFoods =
        new(nameof(ProteinFoods), (int)FoodGroupToken.ProteinFoods, "Alimentos Proteicos", null);

    public static readonly FoodGroup Dairy =
        new(nameof(Dairy), (int)FoodGroupToken.Dairy, "Lácteos", null);

    public static readonly FoodGroup Other =
        new(nameof(Other), (int)FoodGroupToken.Other, "Otros", null);

    public static readonly FoodGroup WholeFruit =
        new(nameof(WholeFruit), (int)FoodGroupToken.WholeFruit, "Fruta Entera", Fruits);

    public static readonly FoodGroup FruitJuice =
        new(nameof(FruitJuice), (int)FoodGroupToken.FruitJuice, "Jugo de Fruta", Fruits);

    public static readonly FoodGroup DarkGreenVegetables =
        new(nameof(DarkGreenVegetables), (int)FoodGroupToken.DarkGreenVegetables, "Vegetales verdes", Vegetables);

    public static readonly FoodGroup RedAndOrangeVegetables =
        new(nameof(RedAndOrangeVegetables), (int)FoodGroupToken.RedAndOrangeVegetables, "Vegetales rojos", Vegetables);

    public static readonly FoodGroup Legumes =
        new(nameof(Legumes), (int)FoodGroupToken.Legumes, "Legumbres", Vegetables);

    public static readonly FoodGroup StarchyVegetables =
        new(nameof(StarchyVegetables), (int)FoodGroupToken.StarchyVegetables, "Vegetales con almidón", Vegetables);

    public static readonly FoodGroup OtherVegetables =
        new(nameof(OtherVegetables), (int)FoodGroupToken.OtherVegetables, "Otros Vegetales", Vegetables);

    public static readonly FoodGroup WholeGrains =
        new(nameof(WholeGrains), (int)FoodGroupToken.WholeGrains, "Granos Enteros", Grains);

    public static readonly FoodGroup RefinedGrains =
        new(nameof(RefinedGrains), (int)FoodGroupToken.RefinedGrains, "Granos Refinados", Grains);

    public static readonly FoodGroup Seafood =
        new(nameof(Seafood), (int)FoodGroupToken.Seafood, "Pescados y Mariscos", ProteinFoods);

    public static readonly FoodGroup MeatPoultryAndEggs =
        new(nameof(MeatPoultryAndEggs), (int)FoodGroupToken.MeatPoultryAndEggs, "Carnes, Aves y Huevos", ProteinFoods);

    public static readonly FoodGroup NutsSeedsAndSoy =
        new(nameof(NutsSeedsAndSoy), (int)FoodGroupToken.NutsSeedsAndSoy, "Frutos secos, Semillas y Soya",
            ProteinFoods);

    public static readonly FoodGroup MilkAndYogurt =
        new(nameof(MilkAndYogurt), (int)FoodGroupToken.MilkAndYogurt, "Leche y Yogurt", Dairy);

    public static readonly FoodGroup Sugars =
        new(nameof(Sugars), (int)FoodGroupToken.Sugars, "Azúcares", Other);

    public static readonly FoodGroup Salts =
        new(nameof(Salts), (int)FoodGroupToken.Salts, "Sales", Other);

    public static readonly FoodGroup FatsAndOils =
        new(nameof(FatsAndOils), (int)FoodGroupToken.FatsAndOils, "Aceites y grasas", Other);

    public static readonly FoodGroup DressingsAndSauces =
        new(nameof(DressingsAndSauces), (int)FoodGroupToken.DressingsAndSauces, "Aderezos y salsas", Other);

    public static readonly FoodGroup SeasoningsAndSpices =
        new(nameof(SeasoningsAndSpices), (int)FoodGroupToken.SeasoningsAndSpices, "Condimentos y especias", Other);

    public static readonly FoodGroup Algae =
        new(nameof(Algae), (int)FoodGroupToken.Algae, "Algas", Other);

    public static readonly FoodGroup Fish =
        new(nameof(Fish), (int)FoodGroupToken.Fish, "Pescados", Seafood);

    public static readonly FoodGroup Shellfish =
        new(nameof(Shellfish), (int)FoodGroupToken.Shellfish, "Mariscos", Seafood);

    public static readonly FoodGroup Meat =
        new(nameof(Meat), (int)FoodGroupToken.Meat, "Carnes", MeatPoultryAndEggs);

    public static readonly FoodGroup Poultry =
        new(nameof(Poultry), (int)FoodGroupToken.Poultry, "Aves", MeatPoultryAndEggs);

    public static readonly FoodGroup Eggs =
        new(nameof(Eggs), (int)FoodGroupToken.Eggs, "Huevos", MeatPoultryAndEggs);

    public static readonly FoodGroup Nuts =
        new(nameof(Nuts), (int)FoodGroupToken.Nuts, "Frutos secos", NutsSeedsAndSoy);

    public static readonly FoodGroup Seeds =
        new(nameof(Seeds), (int)FoodGroupToken.Seeds, "Semillas", NutsSeedsAndSoy);

    public static readonly FoodGroup Soy =
        new(nameof(Soy), (int)FoodGroupToken.Soy, "Soya", NutsSeedsAndSoy);

    public static readonly FoodGroup Milk =
        new(nameof(Milk), (int)FoodGroupToken.Milk, "Leche", MilkAndYogurt);

    public static readonly FoodGroup Yogurt =
        new(nameof(Yogurt), (int)FoodGroupToken.Yogurt, "Yogurt", MilkAndYogurt);
    
    public static readonly FoodGroup Cheese =
        new(nameof(Cheese), (int)FoodGroupToken.Cheese, "Queso", Dairy);

    public static readonly FoodGroup Sweets =
        new(nameof(Sweets), (int)FoodGroupToken.Sweets, "Dulces", Sugars);

    public static readonly FoodGroup SaltySnacks =
        new(nameof(SaltySnacks), (int)FoodGroupToken.SaltySnacks, "Bocadillos Salados", Salts);

    private FoodGroup(string name, int value, string readableName, FoodGroup? category) :
        base(name, value)
    {
        ReadableName = readableName;
        IsTopCategory = category == null;
        Category = category;
    }

    public string ReadableName { get; }
    public bool IsTopCategory { get; }
    public FoodGroup? Category { get; }
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
    Cheese,
    Sweets,
    SaltySnacks
}