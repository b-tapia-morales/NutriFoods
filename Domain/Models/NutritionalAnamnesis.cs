namespace Domain.Models;

public sealed class NutritionalAnamnesis
{
    public Guid Id { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? LastUpdated { get; set; }

    public ICollection<AdverseFoodReaction> AdverseFoodReactions { get; set; } = new List<AdverseFoodReaction>();

    public ICollection<EatingSymptom> EatingSymptoms { get; set; } = new List<EatingSymptom>();

    public ICollection<FoodConsumption> FoodConsumptions { get; set; } = new List<FoodConsumption>();

    public ICollection<HarmfulHabit> HarmfulHabits { get; set; } = new List<HarmfulHabit>();

    public Consultation IdNavigation { get; set; } = null!;
}
