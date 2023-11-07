using Ardalis.SmartEnum;

namespace Domain.Enum;

public class ConsultationPurpose : SmartEnum<ConsultationPurpose>, IEnum<ConsultationPurpose, PurposeToken>
{
    public static readonly ConsultationPurpose None =
        new(nameof(None), (int)PurposeToken.None, string.Empty);

    public static readonly ConsultationPurpose Lose =
        new(nameof(Lose), (int)PurposeToken.Lose, "Perder peso");

    public static readonly ConsultationPurpose Maintain =
        new(nameof(Maintain), (int)PurposeToken.Maintain, "Mantener peso");

    public static readonly ConsultationPurpose Gain =
        new(nameof(Gain), (int)PurposeToken.Gain, "Subir de peso");

    public static readonly ConsultationPurpose EatHealthier =
        new(nameof(EatHealthier), (int)PurposeToken.EatHealthier, "Comer más saludablemente");

    private ConsultationPurpose(string name, int value, string readableName) : base(name, value) =>
        ReadableName = readableName;

    public string ReadableName { get; }
}

public enum PurposeToken
{
    None,
    Lose,
    Maintain,
    Gain,
    EatHealthier
}