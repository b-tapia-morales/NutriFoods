namespace Domain.Models;

public class NutritionalAnamnesis
{
    public Guid Id { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? LastUpdated { get; set; }

    public virtual ICollection<AdverseFoodReaction> AdverseFoodReactions { get; set; } = null!;

    public virtual ICollection<EatingSymptom> EatingSymptoms { get; set; } = null!;

    public virtual ICollection<FoodConsumption> FoodConsumptions { get; set; } = null!;

    public virtual ICollection<HarmfulHabit> HarmfulHabits { get; set; } = null!;

    public virtual Consultation IdNavigation { get; set; } = null!;
}