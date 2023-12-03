namespace API.Dto;

public sealed class DailyMenuDto
{
    public double IntakePercentage { get; set; }
    public string MealType { get; set; } = null!;
    public string Hour { get; set; } = null!;
    public ICollection<NutritionalValueDto> Nutrients { get; set; } = null!;
    public ICollection<NutritionalTargetDto> Targets { get; set; } = null!;
    public ICollection<MenuRecipeDto> Recipes { get; set; } = null!;
}