using Ardalis.SmartEnum;

namespace Domain.Enum;

public class Satiety : SmartEnum<Satiety>
{
    public static readonly Satiety Light = new(nameof(Light), "Ligero", 1);
    public static readonly Satiety Normal = new(nameof(Normal), "Normal", 2);
    public static readonly Satiety Filling = new(nameof(Filling), "Contundente", 3);

    public Satiety(string name, string nameDisplay, int value) : base(name, value)
    {
        NameDisplay = nameDisplay;
    }

    public string NameDisplay { get; set; }
}