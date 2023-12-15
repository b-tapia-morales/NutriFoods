// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global

namespace API.Dto.Insertion;

public class MinimalDailyPlan
{
    public string Day { get; set; } = null!;
    public string PhysicalActivityLevel { get; set; } = null!;
    public double PhysicalActivityFactor { get; set; }
    public double AdjustmentFactor { get; set; }
    public List<NutritionalValueDto> Nutrients { get; set; } = null!;
    public List<NutritionalTargetDto> Targets { get; set; } = null!;
    public List<MinimalDailyMenu> Menus { get; set; } = null!;
}

public class MinimalDailyMenu
{
    public double IntakePercentage { get; set; }
    public string MealType { get; set; } = null!;
    public string Hour { get; set; } = null!;
    public List<NutritionalValueDto> Nutrients { get; set; } = null!;
    public List<NutritionalTargetDto> Targets { get; set; } = null!;
    public List<MinimalMenuRecipe> Recipes { get; set; } = null!;
}

public class MinimalMenuRecipe
{
    public int RecipeId { get; set; }
    public int Portions { get; set; }
}