using Ardalis.SmartEnum;

namespace Domain.Enum;

public class Essentiality : SmartEnum<Essentiality>
{
    public static readonly Essentiality Indispensable = new Essentiality(nameof(Indispensable), "Indispensable", 1);
    public static readonly Essentiality Conditional = new Essentiality(nameof(Conditional), "Condicional", 2);
    public static readonly Essentiality Dispensable = new Essentiality(nameof(Dispensable), "Dispensable", 3);

    public string NameDisplay { get; set; }

    public Essentiality(string name, string nameDisplay, int value) : base(name, value) => NameDisplay = nameDisplay;
}