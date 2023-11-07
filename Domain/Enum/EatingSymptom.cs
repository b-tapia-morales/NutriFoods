using Ardalis.SmartEnum;

namespace Domain.Enum;

public class EatingSymptom : SmartEnum<EatingSymptom>, IEnum<EatingSymptom, EatingSymptomToken>
{
    public static readonly EatingSymptom None =
        new(nameof(None), (int)EatingSymptomToken.None, string.Empty);

    public static readonly EatingSymptom Anorexia =
        new(nameof(Anorexia), (int)EatingSymptomToken.Anorexia, "Anorexia");

    public static readonly EatingSymptom Hyporexia =
        new(nameof(Hyporexia), (int)EatingSymptomToken.Hyporexia, "Hiporexia");

    public static readonly EatingSymptom Nausea =
        new(nameof(Nausea), (int)EatingSymptomToken.Nausea, "Náuseas");

    public static readonly EatingSymptom Vomit =
        new(nameof(Vomit), (int)EatingSymptomToken.Vomit, "Vómito");

    public static readonly EatingSymptom Flatulence =
        new(nameof(Flatulence), (int)EatingSymptomToken.Flatulence, "Flatulencia");

    public static readonly EatingSymptom Constipation =
        new(nameof(Constipation), (int)EatingSymptomToken.Constipation, "Estreñimientos");

    public static readonly EatingSymptom BowelMovements =
        new(nameof(BowelMovements), (int)EatingSymptomToken.BowelMovements, "Evacuaciones intestinales frecuentes");

    public static readonly EatingSymptom Diarrhea =
        new(nameof(Diarrhea), (int)EatingSymptomToken.Diarrhea, "Diarrea");

    public static readonly EatingSymptom Cramping =
        new(nameof(Cramping), (int)EatingSymptomToken.Cramping, "Acalambramiento");

    public static readonly EatingSymptom Other =
        new(nameof(Other), (int)EatingSymptomToken.Other, "Otro");

    private EatingSymptom(string name, int value, string readableName) : base(name, value) =>
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