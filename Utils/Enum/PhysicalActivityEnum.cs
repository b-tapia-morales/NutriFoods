using System.Collections.Immutable;
using Ardalis.SmartEnum;

namespace Utils.Enum;

public class PhysicalActivityEnum : SmartEnum<PhysicalActivityEnum>
{
    public static readonly PhysicalActivityEnum VerySedentary =
        new(nameof(VerySedentary), (int) PhysicalActivity.VerySedentary, PhysicalActivity.VerySedentary,
            "Muy Sedentaria", 0.30);

    public static readonly PhysicalActivityEnum Sedentary =
        new(nameof(Sedentary), (int) PhysicalActivity.Sedentary, PhysicalActivity.Sedentary, "Sedentaria", 0.50);

    public static readonly PhysicalActivityEnum Moderate =
        new(nameof(Moderate), (int) PhysicalActivity.Moderate, PhysicalActivity.Moderate, "Moderada", 0.75);

    public static readonly PhysicalActivityEnum Active =
        new(nameof(Active), (int) PhysicalActivity.Active, PhysicalActivity.Active, "Activa", 1.00);

    private static readonly IDictionary<PhysicalActivity, PhysicalActivityEnum> TokenDictionary =
        List.ToImmutableDictionary(e => e.Token, e => e);

    private static readonly IDictionary<string, PhysicalActivityEnum> ReadableNameDictionary = TokenDictionary
        .ToImmutableDictionary(e => e.Value.ReadableName, e => e.Value, StringComparer.InvariantCultureIgnoreCase);

    public PhysicalActivityEnum(string name, int value, PhysicalActivity token, string readableName,
        double multiplier) : base(name, value)
    {
        Token = token;
        ReadableName = readableName;
        Multiplier = multiplier;
    }

    public PhysicalActivity Token { get; }
    public string ReadableName { get; }
    public double Multiplier { get; }

    public static PhysicalActivityEnum? FromReadableName(string name) =>
        ReadableNameDictionary.ContainsKey(name) ? ReadableNameDictionary[name] : null;

    public static PhysicalActivityEnum FromToken(PhysicalActivity token) => TokenDictionary[token];
}

public enum PhysicalActivity
{
    VerySedentary = 1,
    Sedentary = 2,
    Moderate = 3,
    Active = 4
}