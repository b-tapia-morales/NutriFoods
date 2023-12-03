namespace Domain.Models;

public class Patient
{
    public Guid Id { get; set; }

    public DateTime JoinedOn { get; set; }

    public Guid NutritionistId { get; set; }

    public Address? Address { get; set; }

    public virtual ICollection<Consultation> Consultations { get; set; } = null!;

    public virtual ContactInfo? ContactInfo { get; set; }

    public virtual Nutritionist Nutritionist { get; set; } = null!;

    public virtual PersonalInfo? PersonalInfo { get; set; }
}
