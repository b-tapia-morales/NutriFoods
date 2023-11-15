using Ardalis.SmartEnum;

namespace Domain.Enum;

public class BmiCategory : SmartEnum<BmiCategory>, IEnum<BmiCategory, BmiCategoryToken>
{
    public static readonly BmiCategory None =
        new(nameof(None), (int)BmiCategoryToken.None, string.Empty);

    public static readonly BmiCategory SevereThinness =
        new(nameof(SevereThinness), (int)BmiCategoryToken.SevereThinness, "Delgadez severa");

    public static readonly BmiCategory ModerateThinness =
        new(nameof(ModerateThinness), (int)BmiCategoryToken.ModerateThinness, "Delgadez moderada");

    public static readonly BmiCategory MildThinness =
        new(nameof(MildThinness), (int)BmiCategoryToken.MildThinness, "Delgadez leve");

    public static readonly BmiCategory Normal =
        new(nameof(Normal), (int)BmiCategoryToken.Normal, "Rango Normal");

    public static readonly BmiCategory Overweight =
        new(nameof(Overweight), (int)BmiCategoryToken.Overweight, "Sobrepeso");

    public static readonly BmiCategory MildObesity =
        new(nameof(MildObesity), (int)BmiCategoryToken.MildObesity, "Obesidad leve");

    public static readonly BmiCategory ModerateObesity =
        new(nameof(ModerateObesity), (int)BmiCategoryToken.ModerateObesity, "Obesidad moderada");

    public static readonly BmiCategory MorbidObesity =
        new(nameof(MorbidObesity), (int)BmiCategoryToken.MorbidObesity, "Obesidad mórbida");

    private BmiCategory(string name, int value, string readableName) : base(name, value) =>
        ReadableName = readableName;

    public string ReadableName { get; }
}

public enum BmiCategoryToken
{
    None,
    SevereThinness,
    ModerateThinness,
    MildThinness,
    Normal,
    Overweight,
    MildObesity,
    ModerateObesity,
    MorbidObesity,
}