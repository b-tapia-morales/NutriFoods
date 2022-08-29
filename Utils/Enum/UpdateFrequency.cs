using Ardalis.SmartEnum;

namespace Utils.Enum;

public class UpdateFrequency : SmartEnum<UpdateFrequency>
{
    public static readonly UpdateFrequency Weekly = new(nameof(Weekly), "Semanalmente", 1);
    public static readonly UpdateFrequency Monthly = new(nameof(Monthly), "Mensualmente", 2);

    private static readonly Dictionary<string, UpdateFrequency> Dictionary =
        new(StringComparer.InvariantCultureIgnoreCase)
        {
            {"Semanalmente", Weekly},
            {"Mensualmente", Monthly},
        };

    public static readonly IReadOnlyDictionary<string, UpdateFrequency> ReadOnlyDictionary = Dictionary;

    public UpdateFrequency(string name, string display, int value) : base(name, value)
    {
        Display = display;
    }

    public string Display { get; set; }
}