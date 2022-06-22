using Ardalis.SmartEnum;

namespace Domain.Enum;

public class Satiety : SmartEnum<Satiety>
{
    public static readonly Satiety Light = new Satiety(nameof(Light), "Ligero", 1);
    public static readonly Satiety Normal = new Satiety(nameof(Normal), "Normal", 2);
    public static readonly Satiety Filling = new Satiety(nameof(Filling), "Contundente", 3);

    public string NameDisplay { get; set; }

    public Satiety(string name, string nameDisplay, int value) : base(name, value) => NameDisplay = nameDisplay;
}