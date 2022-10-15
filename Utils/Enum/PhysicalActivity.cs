using System.Collections.Immutable;
using Ardalis.SmartEnum;

namespace Utils.Enum;

public class PhysicalActivity : SmartEnum<PhysicalActivity>
{
    public static readonly PhysicalActivity VerySedentary =
        new(nameof(VerySedentary), (int) PhysicalActivityToken.VerySedentary, "Muy Sedentaria", 0.30);

    public static readonly PhysicalActivity Sedentary =
        new(nameof(Sedentary), (int) PhysicalActivityToken.Sedentary, "Sedentaria", 0.50);

    public static readonly PhysicalActivity Moderate =
        new(nameof(Moderate), (int) PhysicalActivityToken.Moderate, "Moderada", 0.75);

    public static readonly PhysicalActivity Active =
        new(nameof(Active), (int) PhysicalActivityToken.Active, "Activa", 1.00);

    private static readonly IDictionary<PhysicalActivityToken, PhysicalActivity> TokenDictionary =
        new Dictionary<PhysicalActivityToken, PhysicalActivity>
        {
            {PhysicalActivityToken.VerySedentary, VerySedentary},
            {PhysicalActivityToken.Sedentary, Sedentary},
            {PhysicalActivityToken.Moderate, Moderate},
            {PhysicalActivityToken.Active, Active}
        }.ToImmutableDictionary();

    private static readonly IDictionary<string, PhysicalActivity> ReadableNameDictionary = TokenDictionary
        .ToImmutableDictionary(e => e.Value.ReadableName, e => e.Value, StringComparer.InvariantCultureIgnoreCase);

    public PhysicalActivity(string name, int value, string readableName, double multiplier) : base(name, value)
    {
        ReadableName = readableName;
        Multiplier = multiplier;
    }

    public string ReadableName { get; }
    public double Multiplier { get; }

    public static PhysicalActivity? FromReadableName(string name) =>
        ReadableNameDictionary.ContainsKey(name) ? ReadableNameDictionary[name] : null;

    public static PhysicalActivity? FromToken(PhysicalActivityToken token) =>
        TokenDictionary.ContainsKey(token) ? TokenDictionary[token] : null;
}

public enum PhysicalActivityToken
{
    VerySedentary = 1,
    Sedentary = 2,
    Moderate = 3,
    Active = 4
}