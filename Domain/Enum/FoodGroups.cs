using Ardalis.SmartEnum;

namespace Domain.Enum;

public class FoodGroups : SmartEnum<FoodGroups>, IHierarchicalEnum<FoodGroups, FoodGroupToken>
{
    public static readonly FoodGroups None =
        new(nameof(None), (int)FoodGroupToken.None, "", null);

    public static readonly FoodGroups Fruits =
        new(nameof(Fruits), (int)FoodGroupToken.Fruits, "Frutas", null);

    public static readonly FoodGroups Vegetables =
        new(nameof(Vegetables), (int)FoodGroupToken.Vegetables, "Vegetales", null);

    public static readonly FoodGroups Grains =
        new(nameof(Grains), (int)FoodGroupToken.Grains, "Granos", null);

    public static readonly FoodGroups ProteinFoods =
        new(nameof(ProteinFoods), (int)FoodGroupToken.ProteinFoods, "Alimentos Proteicos", null);

    public static readonly FoodGroups Dairy =
        new(nameof(Dairy), (int)FoodGroupToken.Dairy, "Lácteos", null);

    public static readonly FoodGroups Other =
        new(nameof(Other), (int)FoodGroupToken.Other, "Otros", null);

    public static readonly FoodGroups WholeFruit =
        new(nameof(WholeFruit), (int)FoodGroupToken.WholeFruit, "Fruta Entera", Fruits);

    public static readonly FoodGroups FruitJuice =
        new(nameof(FruitJuice), (int)FoodGroupToken.FruitJuice, "Jugo de Fruta", Fruits);

    public static readonly FoodGroups DarkGreenVegetables =
        new(nameof(DarkGreenVegetables), (int)FoodGroupToken.DarkGreenVegetables, "Vegetales verdes", Vegetables);

    public static readonly FoodGroups RedAndOrangeVegetables =
        new(nameof(RedAndOrangeVegetables), (int)FoodGroupToken.RedAndOrangeVegetables, "Vegetales rojos", Vegetables);

    public static readonly FoodGroups Legumes =
        new(nameof(Legumes), (int)FoodGroupToken.Legumes, "Legumbres", Vegetables);

    public static readonly FoodGroups StarchyVegetables =
        new(nameof(StarchyVegetables), (int)FoodGroupToken.StarchyVegetables, "Vegetales con almidón", Vegetables);

    public static readonly FoodGroups OtherVegetables =
        new(nameof(OtherVegetables), (int)FoodGroupToken.OtherVegetables, "Otros Vegetales", Vegetables);

    public static readonly FoodGroups WholeGrains =
        new(nameof(WholeGrains), (int)FoodGroupToken.WholeGrains, "Granos Enteros", Grains);

    public static readonly FoodGroups RefinedGrains =
        new(nameof(RefinedGrains), (int)FoodGroupToken.RefinedGrains, "Granos Refinados", Grains);

    public static readonly FoodGroups Seafood =
        new(nameof(Seafood), (int)FoodGroupToken.Seafood, "Pescados y Mariscos", ProteinFoods);

    public static readonly FoodGroups MeatPoultryAndEggs =
        new(nameof(MeatPoultryAndEggs), (int)FoodGroupToken.MeatPoultryAndEggs, "Carnes, Aves y Huevos", ProteinFoods);

    public static readonly FoodGroups NutsSeedsAndSoy =
        new(nameof(NutsSeedsAndSoy), (int)FoodGroupToken.NutsSeedsAndSoy, "Frutos secos, Semillas y Soya",
            ProteinFoods);

    public static readonly FoodGroups MilkAndYogurt =
        new(nameof(MilkAndYogurt), (int)FoodGroupToken.MilkAndYogurt, "Leche y Yogurt", Dairy);

    public static readonly FoodGroups Sugars =
        new(nameof(Sugars), (int)FoodGroupToken.Sugars, "Azúcares", Other);

    public static readonly FoodGroups Salts =
        new(nameof(Salts), (int)FoodGroupToken.Salts, "Sales", Other);

    public static readonly FoodGroups FatsAndOils =
        new(nameof(FatsAndOils), (int)FoodGroupToken.FatsAndOils, "Aceites y grasas", Other);

    public static readonly FoodGroups DressingsAndSauces =
        new(nameof(DressingsAndSauces), (int)FoodGroupToken.DressingsAndSauces, "Aderezos y salsas", Other);

    public static readonly FoodGroups SeasoningsAndSpices =
        new(nameof(SeasoningsAndSpices), (int)FoodGroupToken.SeasoningsAndSpices, "Condimentos y especias", Other);

    public static readonly FoodGroups Algae =
        new(nameof(Algae), (int)FoodGroupToken.Algae, "Algas", Other);

    public static readonly FoodGroups Fish =
        new(nameof(Fish), (int)FoodGroupToken.Fish, "Pescados", Seafood);

    public static readonly FoodGroups Shellfish =
        new(nameof(Shellfish), (int)FoodGroupToken.Shellfish, "Mariscos", Seafood);

    public static readonly FoodGroups Meat =
        new(nameof(Meat), (int)FoodGroupToken.Meat, "Carnes", MeatPoultryAndEggs);

    public static readonly FoodGroups Poultry =
        new(nameof(Poultry), (int)FoodGroupToken.Poultry, "Aves", MeatPoultryAndEggs);

    public static readonly FoodGroups Eggs =
        new(nameof(Eggs), (int)FoodGroupToken.Eggs, "Huevos", MeatPoultryAndEggs);

    public static readonly FoodGroups Nuts =
        new(nameof(Nuts), (int)FoodGroupToken.Nuts, "Frutos secos", NutsSeedsAndSoy);

    public static readonly FoodGroups Seeds =
        new(nameof(Seeds), (int)FoodGroupToken.Seeds, "Semillas", NutsSeedsAndSoy);

    public static readonly FoodGroups Soy =
        new(nameof(Soy), (int)FoodGroupToken.Soy, "Soya", NutsSeedsAndSoy);

    public static readonly FoodGroups Milk =
        new(nameof(Milk), (int)FoodGroupToken.Milk, "Leche", MilkAndYogurt);

    public static readonly FoodGroups Yogurt =
        new(nameof(Yogurt), (int)FoodGroupToken.Yogurt, "Yogurt", MilkAndYogurt);
    
    public static readonly FoodGroups Cheese =
        new(nameof(Cheese), (int)FoodGroupToken.Cheese, "Queso", Dairy);

    public static readonly FoodGroups Sweets =
        new(nameof(Sweets), (int)FoodGroupToken.Sweets, "Dulces", Sugars);

    public static readonly FoodGroups SaltySnacks =
        new(nameof(SaltySnacks), (int)FoodGroupToken.SaltySnacks, "Bocadillos Salados", Salts);

    private FoodGroups(string name, int value, string readableName, FoodGroups? category) :
        base(name, value)
    {
        ReadableName = readableName;
        Category = category;
    }

    public string ReadableName { get; }
    public FoodGroups? Category { get; }
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