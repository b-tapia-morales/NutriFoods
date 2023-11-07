using Ardalis.SmartEnum;

namespace Domain.Enum;

public class ConsultationType : SmartEnum<ConsultationType>, IEnum<ConsultationType, TypeToken>
{
    public static readonly ConsultationType None =
        new(nameof(None), (int)TypeToken.None, string.Empty);

    public static readonly ConsultationType Evaluation =
        new(nameof(Evaluation), (int)TypeToken.Evaluation, "Evaluación nutricional");

    public static readonly ConsultationType Control =
        new(nameof(Control), (int)TypeToken.Control, "Seguimiento nutricional");

    private ConsultationType(string name, int value, string readableName) : base(name, value) => ReadableName = readableName;

    public string ReadableName { get; }
}

public enum TypeToken
{
    None,
    Evaluation,
    Control
}