using Ardalis.SmartEnum;

namespace Domain.Enum;

public class EatingSymptoms : SmartEnum<EatingSymptoms>, IEnum<EatingSymptoms, EatingSymptomToken>
{
    public static readonly EatingSymptoms None =
        new(nameof(None), (int)EatingSymptomToken.None, string.Empty);

    public static readonly EatingSymptoms Anorexia =
        new(nameof(Anorexia), (int)EatingSymptomToken.Anorexia, "Anorexia");

    public static readonly EatingSymptoms Hyporexia =
        new(nameof(Hyporexia), (int)EatingSymptomToken.Hyporexia, "Hiporexia");

    public static readonly EatingSymptoms Nausea =
        new(nameof(Nausea), (int)EatingSymptomToken.Nausea, "Náuseas");

    public static readonly EatingSymptoms Vomit =
        new(nameof(Vomit), (int)EatingSymptomToken.Vomit, "Vómito");

    public static readonly EatingSymptoms Flatulence =
        new(nameof(Flatulence), (int)EatingSymptomToken.Flatulence, "Flatulencia");

    public static readonly EatingSymptoms Constipation =
        new(nameof(Constipation), (int)EatingSymptomToken.Constipation, "Estreñimientos");

    public static readonly EatingSymptoms BowelMovements =
        new(nameof(BowelMovements), (int)EatingSymptomToken.BowelMovements, "Evacuaciones intestinales frecuentes");

    public static readonly EatingSymptoms Diarrhea =
        new(nameof(Diarrhea), (int)EatingSymptomToken.Diarrhea, "Diarrea");

    public static readonly EatingSymptoms Cramping =
        new(nameof(Cramping), (int)EatingSymptomToken.Cramping, "Acalambramiento");

    public static readonly EatingSymptoms Other =
        new(nameof(Other), (int)EatingSymptomToken.Other, "Otro");

    private EatingSymptoms(string name, int value, string readableName) : base(name, value) =>
        ReadableName = readableName;

    public string ReadableName { get; }
}

public enum EatingSymptomToken
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