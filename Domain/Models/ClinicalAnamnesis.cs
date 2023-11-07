namespace Domain.Models;

public sealed class ClinicalAnamnesis
{
    public Guid Id { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? LastUpdated { get; set; }

    public ICollection<ClinicalSign> ClinicalSigns { get; set; } = new List<ClinicalSign>();

    public ICollection<Disease> Diseases { get; set; } = new List<Disease>();

    public Consultation IdNavigation { get; set; } = null!;

    public ICollection<Medication> Medications { get; set; } = new List<Medication>();
}
