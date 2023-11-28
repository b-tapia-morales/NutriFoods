namespace API.Dto;

public sealed class NutritionalAnamnesisDto
{
    public DateTime? CreatedOn { get; set; }
    public DateTime? LastUpdated { get; set; }
    public ICollection<HarmfulHabitDto> HarmfulHabits { get; set; } = null!;
    public ICollection<EatingSymptomDto> EatingSymptoms { get; set; } = null!;
    public ICollection<AdverseFoodReactionDto> AdverseFoodReactions { get; set; } = null!;
    public ICollection<FoodConsumptionDto> FoodConsumptions { get; set; } = null!;
}
