namespace API.Dto;

public sealed class NutritionalAnamnesisDto
{
    public DateTime? CreatedOn { get; set; }
    public DateTime? LastUpdated { get; set; }
    public List<HarmfulHabitDto> HarmfulHabits { get; set; } = null!;
    public List<EatingSymptomDto> EatingSymptoms { get; set; } = null!;
    public List<AdverseFoodReactionDto> AdverseFoodReactions { get; set; } = null!;
    public List<FoodConsumptionDto> FoodConsumptions { get; set; } = null!;
}
