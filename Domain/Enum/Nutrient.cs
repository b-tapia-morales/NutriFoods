using Ardalis.SmartEnum;

namespace Domain.Enum;

public class Nutrient : SmartEnum<Nutrient>, IHierarchicalEnum<Nutrient, NutrientToken>
{
    public static readonly Nutrient None =
        new(nameof(None), (int)NutrientToken.None, "", "", false, null);

    public static readonly Nutrient Energy =
        new(nameof(Energy), (int)NutrientToken.Energy, "Energía, total", "", true, null);

    public static readonly Nutrient Carbohydrates =
        new(nameof(Carbohydrates), (int)NutrientToken.Carbohydrates, "Carbohidratos, total", "", true, null);

    public static readonly Nutrient Fiber =
        new(nameof(Fiber), (int)NutrientToken.Fiber, "Fibra", "", false, Carbohydrates);

    public static readonly Nutrient Sugars =
        new(nameof(Sugars), (int)NutrientToken.Sugars, "Azúcares", "", true, Carbohydrates);

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
        new(nameof(Starch), (int)NutrientToken.Starch, "Almidón", "Fécula", false, Carbohydrates);

    public static readonly Nutrient FattyAcids =
        new(nameof(FattyAcids), (int)NutrientToken.FattyAcids, "Ácidos grasos, total", "", true, null);

    public static readonly Nutrient Saturated =
        new(nameof(Saturated), (int)NutrientToken.Saturated, "Ácidos grasos saturados, total", "", true, FattyAcids);

    public static readonly Nutrient Butanoic =
        new(nameof(Butanoic), (int)NutrientToken.Butanoic, "Ácido butanoico", "Ácido butírico", false, Saturated);

    public static readonly Nutrient Hexanoic =
        new(nameof(Hexanoic), (int)NutrientToken.Hexanoic, "Ácido hexanoico", "Ácido caproico", false, Saturated);

    public static readonly Nutrient Octanoic =
        new(nameof(Octanoic), (int)NutrientToken.Octanoic, "Ácido octanoico", "Ácido caprílico", false, Saturated);

    public static readonly Nutrient Decanoic =
        new(nameof(Decanoic), (int)NutrientToken.Decanoic, "Ácido decanoico", "Ácido cáprico", false, Saturated);

    public static readonly Nutrient Dodecanoic =
        new(nameof(Dodecanoic), (int)NutrientToken.Dodecanoic, "Ácido dodecanoico", "Ácido láurico", false, Saturated);

    public static readonly Nutrient Tridecanoic =
        new(nameof(Tridecanoic), (int)NutrientToken.Tridecanoic, "Ácido tridecanoico", "", false, Saturated);

    public static readonly Nutrient Tetradecanoic =
        new(nameof(Tetradecanoic), (int)NutrientToken.Tetradecanoic, "Ácido tetradecanoico", "Ácido mirístico", false,
            Saturated);

    public static readonly Nutrient Pentadecanoic =
        new(nameof(Pentadecanoic), (int)NutrientToken.Pentadecanoic, "Ácido pentadecanoico", "", false, Saturated);

    public static readonly Nutrient Hexadecanoic =
        new(nameof(Hexadecanoic), (int)NutrientToken.Hexadecanoic, "Ácido hexadecanoico", "Ácido palmítico", false,
            Saturated);

    public static readonly Nutrient Heptadecanoic =
        new(nameof(Heptadecanoic), (int)NutrientToken.Heptadecanoic, "Ácido heptadecanoico", "Ácido margárico", false,
            Saturated);

    public static readonly Nutrient Octadecanoic =
        new(nameof(Octadecanoic), (int)NutrientToken.Octadecanoic, "Ácido octadecanoico", "Ácido esteárico", false,
            Saturated);

    public static readonly Nutrient Eicosanoic =
        new(nameof(Eicosanoic), (int)NutrientToken.Eicosanoic, "Ácido eicosanoico", "Ácido araquídico", false,
            Saturated);

    public static readonly Nutrient Docosanoic =
        new(nameof(Docosanoic), (int)NutrientToken.Docosanoic, "Ácido docosanoico", "Ácido behénico", false, Saturated);

    public static readonly Nutrient Tetracosanoic =
        new(nameof(Tetracosanoic), (int)NutrientToken.Tetracosanoic, "Ácido tetracosanoico", "Ácido lignocérico", false,
            Saturated);

    public static readonly Nutrient Monounsaturated =
        new(nameof(Monounsaturated), (int)NutrientToken.Monounsaturated, "Ácidos grasos monoinsaturados, total", "",
            true, FattyAcids);

    public static readonly Nutrient Tetradecenoic =
        new(nameof(Tetradecenoic), (int)NutrientToken.Tetradecenoic, "Ácido tetradecenoico", "Ácido miristoleico",
            false, Monounsaturated);

    public static readonly Nutrient Pentadecenoic =
        new(nameof(Pentadecenoic), (int)NutrientToken.Pentadecenoic, "Ácido pentadecenoico", "", false,
            Monounsaturated);

    public static readonly Nutrient Hexadecenoic =
        new(nameof(Hexadecenoic), (int)NutrientToken.Hexadecenoic, "Ácido hexadecenoico", "Ácido palmitoleico", false,
            Monounsaturated);

    public static readonly Nutrient CisHexadecenoic =
        new(nameof(CisHexadecenoic), (int)NutrientToken.CisHexadecenoic, "Ácido cis-hexadecenoico", "", false,
            Monounsaturated);

    public static readonly Nutrient Heptadecenoic =
        new(nameof(Heptadecenoic), (int)NutrientToken.Heptadecenoic, "Ácido heptadecenoico", "", false,
            Monounsaturated);

    public static readonly Nutrient Iheptadecenoic =
        new(nameof(Iheptadecenoic), (int)NutrientToken.Iheptadecenoic, "Ácido i-heptadecenoico", "", false,
            Monounsaturated);

    public static readonly Nutrient Octadecenoic =
        new(nameof(Octadecenoic), (int)NutrientToken.Octadecenoic, "Ácido octadecenoico", "Ácido oleico", false,
            Monounsaturated);

    public static readonly Nutrient CisOctadecenoic =
        new(nameof(CisOctadecenoic), (int)NutrientToken.CisOctadecenoic, "Ácido cis-octadecenoico", "", false,
            Monounsaturated);

    public static readonly Nutrient Eicosenoic =
        new(nameof(Eicosenoic), (int)NutrientToken.Eicosenoic, "Ácido eicosenoico", "Ácido gadoleico", false,
            Monounsaturated);

    public static readonly Nutrient Docosenoic =
        new(nameof(Docosenoic), (int)NutrientToken.Docosenoic, "Ácido docosenoico", "", false, Monounsaturated);

    public static readonly Nutrient CisDocosenoic =
        new(nameof(CisDocosenoic), (int)NutrientToken.CisDocosenoic, "Ácido cis-docosenoico", "Ácido erúcico", false,
            Monounsaturated);

    public static readonly Nutrient CisTetracosenoic =
        new(nameof(CisTetracosenoic), (int)NutrientToken.CisTetracosenoic, "Ácido cis-tetracosenoico",
            "Ácido nervónico", false, Monounsaturated);

    public static readonly Nutrient Polyunsaturated =
        new(nameof(Polyunsaturated), (int)NutrientToken.Polyunsaturated, "Ácidos grasos poliinsaturados, total", "",
            true, FattyAcids);

    public static readonly Nutrient Octadecadienoic =
        new(nameof(Octadecadienoic), (int)NutrientToken.Octadecadienoic, "Ácido octadecadienoico", "Ácido linoleico",
            false, Polyunsaturated);

    public static readonly Nutrient CisOctadecadienoic =
        new(nameof(CisOctadecadienoic), (int)NutrientToken.CisOctadecadienoic, "Ácido cis-octadecadienoico", "", false,
            Polyunsaturated);

    public static readonly Nutrient Ioctadecadienoic =
        new(nameof(Ioctadecadienoic), (int)NutrientToken.Ioctadecadienoic, "Ácido i-octadecadienoico", "", false,
            Polyunsaturated);

    public static readonly Nutrient ConOctadecadienoic =
        new(nameof(ConOctadecadienoic), (int)NutrientToken.ConOctadecadienoic, "Ácido octadecadienoico conjugado", "",
            false, Polyunsaturated);

    public static readonly Nutrient Octadecatrienoic =
        new(nameof(Octadecatrienoic), (int)NutrientToken.Octadecatrienoic, "Ácido octadecatrienoico",
            "Ácido linolénico", false, Polyunsaturated);

    public static readonly Nutrient Cis3Octadecatrienoic =
        new(nameof(Cis3Octadecatrienoic), (int)NutrientToken.Cis3Octadecatrienoic, "Ácido cis3-octadecatrienoico",
            "Ácido α-linolénico", false, Polyunsaturated);

    public static readonly Nutrient Cis6Octadecatrienoic =
        new(nameof(Cis6Octadecatrienoic), (int)NutrientToken.Cis6Octadecatrienoic, "Ácido cis6-octadecatrienoico",
            "Ácido γ-linolénico", false, Polyunsaturated);

    public static readonly Nutrient Ioctadecatrienoic =
        new(nameof(Ioctadecatrienoic), (int)NutrientToken.Ioctadecatrienoic, "Ácido i-octadecatrienoico", "", false,
            Polyunsaturated);

    public static readonly Nutrient Octadecatetraenoic =
        new(nameof(Octadecatetraenoic), (int)NutrientToken.Octadecatetraenoic, "Ácido octadecatetraenoico", "", false,
            Polyunsaturated);

    public static readonly Nutrient Eicosadienoic =
        new(nameof(Eicosadienoic), (int)NutrientToken.Eicosadienoic, "Ácido eicosadienoico", "", false,
            Polyunsaturated);

    public static readonly Nutrient Eicosatrienoic =
        new(nameof(Eicosatrienoic), (int)NutrientToken.Eicosatrienoic, "Ácido eicosatrienoico", "", false,
            Polyunsaturated);

    public static readonly Nutrient N3Eicosatrienoic =
        new(nameof(N3Eicosatrienoic), (int)NutrientToken.N3Eicosatrienoic, "Ácido 3-eicosatrienoico", "", false,
            Polyunsaturated);

    public static readonly Nutrient N6Eicosatrienoic =
        new(nameof(N6Eicosatrienoic), (int)NutrientToken.N6Eicosatrienoic, "Ácido 6-eicosatrienoico", "", false,
            Polyunsaturated);

    public static readonly Nutrient Eicosatetraenoic =
        new(nameof(Eicosatetraenoic), (int)NutrientToken.Eicosatetraenoic, "Ácido eicosatetraenoico", "", false,
            Polyunsaturated);

    public static readonly Nutrient N6Eicosatetraenoic =
        new(nameof(N6Eicosatetraenoic), (int)NutrientToken.N6Eicosatetraenoic, "Ácido 6-eicosatetraenoico",
            "Ácido araquidónico", false, Polyunsaturated);

    public static readonly Nutrient Eicosapentaenoic =
        new(nameof(Eicosapentaenoic), (int)NutrientToken.Eicosapentaenoic, "Ácido eicosapentaenoico",
            "Ácido timnodónico", false, Polyunsaturated);

    public static readonly Nutrient N3Docosapentaenoic =
        new(nameof(N3Docosapentaenoic), (int)NutrientToken.N3Docosapentaenoic, "Ácido 3-docosapentaenoico", "", false,
            Polyunsaturated);

    public static readonly Nutrient N3Docosahexaenoic =
        new(nameof(N3Docosahexaenoic), (int)NutrientToken.N3Docosahexaenoic, "Ácido 3-docosahexaenoico", "", false,
            Polyunsaturated);
    
    public static readonly Nutrient Trans =
        new(nameof(Trans), (int)NutrientToken.Trans, "Ácidos grasos trans, total", "", false, null);

    public static readonly Nutrient TransMonoenoic =
        new(nameof(TransMonoenoic), (int)NutrientToken.TransMonoenoic, "Ácido 3-docosapentaenoico", "", false, Trans);

    public static readonly Nutrient TransPolinoic =
        new(nameof(TransPolinoic), (int)NutrientToken.TransPolinoic, "Ácido 3-docosahexaenoico", "", false, Trans);

    public static readonly Nutrient Minerals =
        new(nameof(Minerals), (int)NutrientToken.Minerals, "Minerales", "", false, null);

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

    private Nutrient(string name, int value, string readableName, string optionalName, bool isTotal,
        Nutrient? category) : base(name, value)
    {
        ReadableName = readableName;
        OptionalName = optionalName;
        IsTopCategory = category == null;
        IsTotal = isTotal;
        Category = category;
    }

    public string ReadableName { get; }
    public string OptionalName { get; }
    public bool IsTopCategory { get; }
    public bool IsTotal { get; }
    public Nutrient? Category { get; }
}

public enum NutrientToken
{
    None,
    // Energy
    Energy,
    // Carbohydrates
    Carbohydrates,
    Fiber,
    Sugars,
    Sucrose,
    Glucose,
    Fructose,
    Lactose,
    Maltose,
    Galactose,
    Starch,
    // Fatty Acids
    FattyAcids,
    // Saturated Fatty Acids
    Saturated,
    Butanoic,
    Hexanoic,
    Octanoic,
    Decanoic,
    Dodecanoic,
    Tridecanoic,
    Tetradecanoic,
    Pentadecanoic,
    Hexadecanoic,
    Heptadecanoic,
    Octadecanoic,
    Eicosanoic,
    Docosanoic,
    Tetracosanoic,
    // Monounsaturated Fatty Acids
    Monounsaturated,
    Tetradecenoic,
    Pentadecenoic,
    Hexadecenoic,
    CisHexadecenoic,
    Heptadecenoic,
    Iheptadecenoic,
    Octadecenoic,
    CisOctadecenoic,
    Eicosenoic,
    Docosenoic,
    CisDocosenoic,
    CisTetracosenoic,
    // Polyunsaturated Fatty Acids
    Polyunsaturated,
    Octadecadienoic,
    CisOctadecadienoic,
    Ioctadecadienoic,
    ConOctadecadienoic,
    Octadecatrienoic,
    Cis3Octadecatrienoic,
    Cis6Octadecatrienoic,
    Ioctadecatrienoic,
    Octadecatetraenoic,
    Eicosadienoic,
    Eicosatrienoic,
    N3Eicosatrienoic,
    N6Eicosatrienoic,
    Eicosatetraenoic,
    N6Eicosatetraenoic,
    Eicosapentaenoic,
    N3Docosapentaenoic,
    N3Docosahexaenoic,
    // Trans Fatty Acids
    Trans,
    TransMonoenoic,
    TransPolinoic,
    // Minerals
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
    Fluoride,
}