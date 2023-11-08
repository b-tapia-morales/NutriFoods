using Domain.Enum;
using Domain.Models;

namespace API.Dto;

public sealed class DailyMenuDto
{
    public int IntakePercentage { get; set; }
    public string MealType { get; set; } = string.Empty;
    public string Hour { get; set; } = string.Empty;
    public ICollection<DailyMenuNutrientDto> Nutrients { get; set; } = new List<DailyMenuNutrientDto>();
    public ICollection<MenuRecipeDto> Recipes { get; set; } = new List<MenuRecipeDto>();
}