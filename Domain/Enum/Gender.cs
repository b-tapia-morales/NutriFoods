using Ardalis.SmartEnum;

namespace Domain.Enum;

public class Gender : SmartEnum<Gender>
{
    public static readonly Gender Male = new Gender(nameof(Male), "Hombre", 1);
    public static readonly Gender Female = new Gender(nameof(Female), "Mujer", 2);

    public string NameDisplay { get; set; }

    public Gender(string name, string nameDisplay, int value) : base(name, value) => NameDisplay = nameDisplay;
}