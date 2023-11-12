namespace Domain.Models;

public class Patient
{
    public Guid Id { get; set; }

    public DateTime JoinedOn { get; set; }

    public Guid NutritionistId { get; set; }

    public Address? Address { get; set; }

    public virtual ICollection<Consultation> Consultations { get; set; } = new List<Consultation>();

    public ContactInfo? ContactInfo { get; set; }

    public Nutritionist Nutritionist { get; set; } = null!;

    public PersonalInfo? PersonalInfo { get; set; }
}
