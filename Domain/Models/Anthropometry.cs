namespace Domain.Models;

public class Anthropometry
{
    public Guid Id { get; set; }

    public int Height { get; set; }

    public double Weight { get; set; }

    public double Bmi { get; set; }

    public double MuscleMassPercentage { get; set; }

    public double WaistCircumference { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? LastUpdated { get; set; }

    public Consultation IdNavigation { get; set; } = null!;
}
