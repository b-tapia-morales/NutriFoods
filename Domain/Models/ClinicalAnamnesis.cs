namespace Domain.Models;

public class ClinicalAnamnesis
{
    public Guid Id { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? LastUpdated { get; set; }

    public virtual ICollection<ClinicalSign> ClinicalSigns { get; set; } = new List<ClinicalSign>();

    public virtual ICollection<Disease> Diseases { get; set; } = new List<Disease>();
    
    public virtual ICollection<Ingestible> Ingestibles { get; set; } = new List<Ingestible>();

    public virtual Consultation IdNavigation { get; set; } = null!;
}
