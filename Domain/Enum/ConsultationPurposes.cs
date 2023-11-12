using Ardalis.SmartEnum;

namespace Domain.Enum;

public class ConsultationPurposes : SmartEnum<ConsultationPurposes>,
    IEnum<ConsultationPurposes, ConsultationPurposeToken>
{
    public static readonly ConsultationPurposes None =
        new(nameof(None), (int)ConsultationPurposeToken.None, string.Empty);

    public static readonly ConsultationPurposes Lose =
        new(nameof(Lose), (int)ConsultationPurposeToken.Lose, "Perder peso");

    public static readonly ConsultationPurposes Maintain =
        new(nameof(Maintain), (int)ConsultationPurposeToken.Maintain, "Mantener peso");

    public static readonly ConsultationPurposes Gain =
        new(nameof(Gain), (int)ConsultationPurposeToken.Gain, "Subir de peso");

    public static readonly ConsultationPurposes EatHealthier =
        new(nameof(EatHealthier), (int)ConsultationPurposeToken.EatHealthier, "Comer más saludablemente");

    private ConsultationPurposes(string name, int value, string readableName) : base(name, value) =>
        ReadableName = readableName;

    public string ReadableName { get; }
}

public enum ConsultationPurposeToken
{
    None,
    Lose,
    Maintain,
    Gain,
    EatHealthier
}