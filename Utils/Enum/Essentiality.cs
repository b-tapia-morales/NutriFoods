using Ardalis.SmartEnum;

namespace Utils.Enum;

public class Essentiality : SmartEnum<Essentiality>
{
    public static readonly Essentiality Indispensable = new(nameof(Indispensable), "Indispensable", 0);
    public static readonly Essentiality Conditional = new(nameof(Conditional), "Condicional", 1);
    public static readonly Essentiality Dispensable = new(nameof(Dispensable), "Dispensable", 2);

    private static readonly Dictionary<string, Essentiality> Dictionary = new(StringComparer.InvariantCultureIgnoreCase)
    {
        {"Indispensable", Indispensable},
        {"Condicional", Conditional},
        {"Dispensable", Dispensable}
    };

    public static readonly IReadOnlyDictionary<string, Essentiality> ReadOnlyDictionary = Dictionary;

    public Essentiality(string name, string display, int value) : base(name, value)
    {
        Display = display;
    }

    public string Display { get; set; }
}