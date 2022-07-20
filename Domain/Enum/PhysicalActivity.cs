using Ardalis.SmartEnum;

namespace Domain.Enum;

public class PhysicalActivity : SmartEnum<PhysicalActivity>
{
    public static readonly PhysicalActivity VerySedentary = new(nameof(VerySedentary), "Muy Sedentaria", 1, 0.30);
    public static readonly PhysicalActivity Sedentary = new(nameof(Sedentary), "Sedentaria", 2, 0.50);
    public static readonly PhysicalActivity Moderate = new(nameof(Moderate), "Moderada", 3, 0.75);
    public static readonly PhysicalActivity Active = new(nameof(Active), "Activa", 3, 1.00);

    private static readonly Dictionary<string, PhysicalActivity> Dictionary =
        new(StringComparer.InvariantCultureIgnoreCase)
        {
            {"Muy Sedentaria", VerySedentary},
            {"Sedentaria", Sedentary},
            {"Moderada", Moderate},
            {"Activa", Active}
        };

    public static readonly IReadOnlyDictionary<string, PhysicalActivity> ReadOnlyDictionary = Dictionary;

    public PhysicalActivity(string name, string display, int value, double multiplier) : base(name, value)
    {
        Display = display;
        Multiplier = multiplier;
    }

    public string Display { get; set; }
    public double Multiplier { get; set; }
}