namespace Domain.Models;

public class ClinicalAnamnesis
{
    public Guid Id { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? LastUpdated { get; set; }

    public virtual ICollection<ClinicalSign> ClinicalSigns { get; set; } = null!;

    public virtual ICollection<Disease> Diseases { get; set; } = null!;

    public virtual ICollection<Ingestible> Ingestibles { get; set; } = null!;

    public virtual Consultation IdNavigation { get; set; } = null!;
}