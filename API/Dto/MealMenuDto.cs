using Domain.Enum;

namespace API.Dto;

public class MealMenuDto
{
    public MealTypeDto MealType { get; set; } = null!;
    public string Satiety { get; set; } = null!;
    public double EnergyTotal { get; set; }
    public double CarbohydratesTotal { get; set; }
    public double LipidsTotal { get; set; }
    public double ProteinsTotal { get; set; }
    public IEnumerable<MealMenuRecipeDto> MenuRecipes { get; set; } = null!;
}