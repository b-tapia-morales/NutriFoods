namespace Domain.Models;

public class NutritionalAnamnesis
{
    public Guid Id { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? LastUpdated { get; set; }

    public virtual ICollection<AdverseFoodReaction> AdverseFoodReactions { get; set; } =
        new List<AdverseFoodReaction>();

    public virtual ICollection<EatingSymptom> EatingSymptoms { get; set; } = new List<EatingSymptom>();

    public virtual ICollection<FoodConsumption> FoodConsumptions { get; set; } = new List<FoodConsumption>();

    public virtual ICollection<HarmfulHabit> HarmfulHabits { get; set; } = new List<HarmfulHabit>();

    public Consultation IdNavigation { get; set; } = null!;
}