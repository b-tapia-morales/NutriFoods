using Ardalis.SmartEnum;

namespace Utils.Enum;

public class Gender : SmartEnum<Gender>
{
    public static readonly Gender Male = new(nameof(Male), "Hombre", 1);
    public static readonly Gender Female = new(nameof(Female), "Mujer", 2);

    private static readonly Dictionary<string, Gender> Dictionary = new(StringComparer.InvariantCultureIgnoreCase)
    {
        {"Hombre", Male},
        {"Mujer", Female}
    };

    public static readonly IReadOnlyDictionary<string, Gender> ReadOnlyDictionary = Dictionary;

    public Gender(string name, string display, int value) : base(name, value)
    {
        Display = display;
    }

    public string Display { get; set; }
}