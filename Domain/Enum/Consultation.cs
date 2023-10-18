using Ardalis.SmartEnum;

namespace Domain.Enum;

public class Consultation : SmartEnum<Consultation>, IEnum<Consultation, ConsultationToken>
{
    public static readonly Consultation None =
        new(nameof(None), (int)ConsultationToken.None, string.Empty);

    public static readonly Consultation Evaluation =
        new(nameof(Evaluation), (int)ConsultationToken.Evaluation, "Evaluación nutricional");

    public static readonly Consultation Control =
        new(nameof(Control), (int)ConsultationToken.Control, "Seguimiento nutricional");

    public Consultation(string name, int value, string readableName) : base(name, value) => ReadableName = readableName;

    public string ReadableName { get; }
}

public enum ConsultationToken
{
    None,
    Evaluation,
    Control
}