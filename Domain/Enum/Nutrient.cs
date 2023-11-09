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

    public static readonly Nutrient SaturatedFattyAcids =
        new(nameof(SaturatedFattyAcids), (int)NutrientToken.SaturatedFattyAcids, "Ácidos grasos saturados, total", "",
            true, FattyAcids);

    public static readonly Nutrient ButanoicAcid =
        new(nameof(ButanoicAcid), (int)NutrientToken.ButanoicAcid, "Ácido butanoico", "Ácido butírico", false,
            SaturatedFattyAcids);

    public static readonly Nutrient HexanoicAcid =
        new(nameof(HexanoicAcid), (int)NutrientToken.HexanoicAcid, "Ácido hexanoico", "Ácido caproico", false,
            SaturatedFattyAcids);

    public static readonly Nutrient OctanoicAcid =
        new(nameof(OctanoicAcid), (int)NutrientToken.OctanoicAcid, "Ácido octanoico", "Ácido caprílico", false,
            SaturatedFattyAcids);

    public static readonly Nutrient DecanoicAcid =
        new(nameof(DecanoicAcid), (int)NutrientToken.DecanoicAcid, "Ácido decanoico", "Ácido cáprico", false,
            SaturatedFattyAcids);

    public static readonly Nutrient DodecanoicAcid =
        new(nameof(DodecanoicAcid), (int)NutrientToken.DodecanoicAcid, "Ácido dodecanoico", "Ácido láurico", false,
            SaturatedFattyAcids);

    public static readonly Nutrient TridecanoicAcid =
        new(nameof(TridecanoicAcid), (int)NutrientToken.TridecanoicAcid, "Ácido tridecanoico", "", false,
            SaturatedFattyAcids);

    public static readonly Nutrient TetradecanoicAcid =
        new(nameof(TetradecanoicAcid), (int)NutrientToken.TetradecanoicAcid, "Ácido tetradecanoico", "Ácido mirístico",
            false, SaturatedFattyAcids);

    public static readonly Nutrient PentadecanoicAcid =
        new(nameof(PentadecanoicAcid), (int)NutrientToken.PentadecanoicAcid, "Ácido pentadecanoico", "", false,
            SaturatedFattyAcids);

    public static readonly Nutrient HexadecanoicAcid =
        new(nameof(HexadecanoicAcid), (int)NutrientToken.HexadecanoicAcid, "Ácido hexadecanoico", "Ácido palmítico",
            false, SaturatedFattyAcids);

    public static readonly Nutrient HeptadecanoicAcid =
        new(nameof(HeptadecanoicAcid), (int)NutrientToken.HeptadecanoicAcid, "Ácido heptadecanoico", "Ácido margárico",
            false, SaturatedFattyAcids);

    public static readonly Nutrient OctadecanoicAcid =
        new(nameof(OctadecanoicAcid), (int)NutrientToken.OctadecanoicAcid, "Ácido octadecanoico", "Ácido esteárico",
            false, SaturatedFattyAcids);

    public static readonly Nutrient EicosanoicAcid =
        new(nameof(EicosanoicAcid), (int)NutrientToken.EicosanoicAcid, "Ácido eicosanoico", "Ácido araquídico", false,
            SaturatedFattyAcids);

    public static readonly Nutrient DocosanoicAcid =
        new(nameof(DocosanoicAcid), (int)NutrientToken.DocosanoicAcid, "Ácido docosanoico", "Ácido behénico", false,
            SaturatedFattyAcids);

    public static readonly Nutrient TetracosanoicAcid =
        new(nameof(TetracosanoicAcid), (int)NutrientToken.TetracosanoicAcid, "Ácido tetracosanoico",
            "Ácido lignocérico", false, SaturatedFattyAcids);

    public static readonly Nutrient MonounsaturatedFattyAcids =
        new(nameof(MonounsaturatedFattyAcids), (int)NutrientToken.MonounsaturatedFattyAcids,
            "Ácidos grasos monoinsaturados, total", "", true, FattyAcids);

    public static readonly Nutrient TetradecenoicAcid =
        new(nameof(TetradecenoicAcid), (int)NutrientToken.TetradecenoicAcid, "Ácido tetradecenoico",
            "Ácido miristoleico", false, MonounsaturatedFattyAcids);

    public static readonly Nutrient PentadecenoicAcid =
        new(nameof(PentadecenoicAcid), (int)NutrientToken.PentadecenoicAcid, "Ácido pentadecenoico", "", false,
            MonounsaturatedFattyAcids);

    public static readonly Nutrient HexadecenoicAcid =
        new(nameof(HexadecenoicAcid), (int)NutrientToken.HexadecenoicAcid, "Ácido hexadecenoico", "Ácido palmitoleico",
            false, MonounsaturatedFattyAcids);

    public static readonly Nutrient CisHexadecenoicAcid =
        new(nameof(CisHexadecenoicAcid), (int)NutrientToken.CisHexadecenoicAcid, "Ácido cis-hexadecenoico", "", false,
            MonounsaturatedFattyAcids);

    public static readonly Nutrient HeptadecenoicAcid =
        new(nameof(HeptadecenoicAcid), (int)NutrientToken.HeptadecenoicAcid, "Ácido heptadecenoico", "", false,
            MonounsaturatedFattyAcids);

    public static readonly Nutrient IheptadecenoicAcid =
        new(nameof(IheptadecenoicAcid), (int)NutrientToken.IheptadecenoicAcid, "Ácido i-heptadecenoico", "", false,
            MonounsaturatedFattyAcids);

    public static readonly Nutrient OctadecenoicAcid =
        new(nameof(OctadecenoicAcid), (int)NutrientToken.OctadecenoicAcid, "Ácido octadecenoico", "Ácido oleico", false,
            MonounsaturatedFattyAcids);

    public static readonly Nutrient CisOctadecenoicAcid =
        new(nameof(CisOctadecenoicAcid), (int)NutrientToken.CisOctadecenoicAcid, "Ácido cis-octadecenoico", "", false,
            MonounsaturatedFattyAcids);

    public static readonly Nutrient EicosenoicAcid =
        new(nameof(EicosenoicAcid), (int)NutrientToken.EicosenoicAcid, "Ácido eicosenoico", "Ácido gadoleico", false,
            MonounsaturatedFattyAcids);

    public static readonly Nutrient DocosenoicAcid =
        new(nameof(DocosenoicAcid), (int)NutrientToken.DocosenoicAcid, "Ácido docosenoico", "", false,
            MonounsaturatedFattyAcids);

    public static readonly Nutrient CisDocosenoicAcid =
        new(nameof(CisDocosenoicAcid), (int)NutrientToken.CisDocosenoicAcid, "Ácido cis-docosenoico", "Ácido erúcico",
            false, MonounsaturatedFattyAcids);

    public static readonly Nutrient CisTetracosenoicAcid =
        new(nameof(CisTetracosenoicAcid), (int)NutrientToken.CisTetracosenoicAcid, "Ácido cis-tetracosenoico",
            "Ácido nervónico", false, MonounsaturatedFattyAcids);

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
    Energy,
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
    FattyAcids,
    SaturatedFattyAcids,
    ButanoicAcid,
    HexanoicAcid,
    OctanoicAcid,
    DecanoicAcid,
    DodecanoicAcid,
    TridecanoicAcid,
    TetradecanoicAcid,
    PentadecanoicAcid,
    HexadecanoicAcid,
    HeptadecanoicAcid,
    OctadecanoicAcid,
    EicosanoicAcid,
    DocosanoicAcid,
    TetracosanoicAcid,
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
    MonounsaturatedFattyAcids,
    TetradecenoicAcid,
    PentadecenoicAcid,
    HexadecenoicAcid,
    CisHexadecenoicAcid,
    HeptadecenoicAcid,
    IheptadecenoicAcid,
    OctadecenoicAcid,
    CisOctadecenoicAcid,
    EicosenoicAcid,
    DocosenoicAcid,
    CisDocosenoicAcid,
    CisTetracosenoicAcid
}