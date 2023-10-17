using Ardalis.SmartEnum;

namespace Domain.Enum;

public class EatingSymptoms : SmartEnum<EatingSymptoms>, IEnum<EatingSymptoms, EatingSymptomsToken>
{
    public static readonly EatingSymptoms None =
        new(nameof(None), (int)EatingSymptomsToken.None, string.Empty);

    public static readonly EatingSymptoms Anorexia =
        new(nameof(Anorexia), (int)EatingSymptomsToken.Anorexia, "Anorexia");

    public static readonly EatingSymptoms Hyporexia =
        new(nameof(Hyporexia), (int)EatingSymptomsToken.Hyporexia, "Hiporexia");

    public static readonly EatingSymptoms Nausea =
        new(nameof(Nausea), (int)EatingSymptomsToken.Nausea, "Náuseas");

    public static readonly EatingSymptoms Vomit =
        new(nameof(Vomit), (int)EatingSymptomsToken.Vomit, "Vómito");

    public static readonly EatingSymptoms Flatulence =
        new(nameof(Flatulence), (int)EatingSymptomsToken.Flatulence, "Flatulencia");

    public static readonly EatingSymptoms Constipation =
        new(nameof(Constipation), (int)EatingSymptomsToken.Constipation, "Estreñimientos");

    public static readonly EatingSymptoms BowelMovements =
        new(nameof(BowelMovements), (int)EatingSymptomsToken.BowelMovements, "Evacuaciones intestinales frecuentes");

    public static readonly EatingSymptoms Diarrhea =
        new(nameof(Diarrhea), (int)EatingSymptomsToken.Diarrhea, "Diarrea");

    public static readonly EatingSymptoms Cramping =
        new(nameof(Cramping), (int)EatingSymptomsToken.Cramping, "Acalambramiento");

    public static readonly EatingSymptoms Other =
        new(nameof(Other), (int)EatingSymptomsToken.Other, "Otro");

    private EatingSymptoms(string name, int value, string readableName) : base(name, value) =>
        ReadableName = readableName;

    public string ReadableName { get; }
}

public enum EatingSymptomsToken
{
    None,
    Anorexia,
    Hyporexia,
    Nausea,
    Vomit,
    Flatulence,
    Constipation,
    BowelMovements,
    Diarrhea,
    Cramping,
    Other
}