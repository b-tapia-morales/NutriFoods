using Ardalis.SmartEnum;

namespace Domain.Enum;

public class Satiety : SmartEnum<Satiety>
{
    public static readonly Satiety None = new(nameof(None), "Ninguna", 0);
    public static readonly Satiety Light = new(nameof(Light), "Ligera", 1);
    public static readonly Satiety Normal = new(nameof(Normal), "Normal", 2);
    public static readonly Satiety Filling = new(nameof(Filling), "Contundente", 3);

    private static readonly Dictionary<string, Satiety> Dictionary = new(StringComparer.InvariantCultureIgnoreCase)
    {
        {"Ninguna", None},
        {"Ligera", Light},
        {"Normal", Normal},
        {"Contundente", Filling}
    };

    public static readonly IReadOnlyDictionary<string, Satiety> ReadOnlyDictionary = Dictionary;

    public Satiety(string name, string display, int value) : base(name, value)
    {
        Display = display;
    }

    public string Display { get; set; }
}