using Ardalis.SmartEnum;

namespace Domain.Enum;

public class PhysicalActivity : SmartEnum<PhysicalActivity>
{
    public static readonly PhysicalActivity VerySedentary = new(nameof(VerySedentary), "Muy Sedentaria", 1);
    public static readonly PhysicalActivity Sedentary = new(nameof(Sedentary), "Sedentaria", 2);
    public static readonly PhysicalActivity Moderate = new(nameof(Moderate), "Moderada", 3);
    public static readonly PhysicalActivity Active = new(nameof(Active), "Activa", 3);

    public PhysicalActivity(string name, string nameDisplay, int value) : base(name, value)
    {
        NameDisplay = nameDisplay;
    }

    public string NameDisplay { get; set; }
}