using Ardalis.SmartEnum;

namespace Domain.Enum;

public class PhysicalActivity : SmartEnum<PhysicalActivity>
{
    public static readonly PhysicalActivity VerySedentary = new PhysicalActivity(nameof(VerySedentary), "Muy Sedentaria", 1);
    public static readonly PhysicalActivity Sedentary = new PhysicalActivity(nameof(Sedentary), "Sedentaria", 2);
    public static readonly PhysicalActivity Moderate = new PhysicalActivity(nameof(Moderate), "Moderada", 3);
    public static readonly PhysicalActivity Active = new PhysicalActivity(nameof(Active), "Activa", 3);

    public string NameDisplay { get; set; }

    public PhysicalActivity(string name, string nameDisplay, int value) : base(name, value) => NameDisplay = nameDisplay;
}