namespace API.Dto;

public class DailyPlanDto
{
    public string Day { get; set; } = null!;
    public string PhysicalActivityLevel { get; set; } = null!;
    public double PhysicalActivityFactor { get; set; }
    public double AdjustmentFactor { get; set; }
    public ICollection<NutritionalValueDto> Nutrients { get; set; } = null!;
    public ICollection<NutritionalTargetDto> Targets { get; set; } = null!;
    public ICollection<DailyMenuDto> Menus { get; set; } = null!;
}

public static class DailyPlanExtensions
{
    public static void AddMenuTargets(this DailyPlanDto dailyPlan)
    {
        foreach (var menu in dailyPlan.Menus)
        {
            var intakePercentage = menu.IntakePercentage;
            foreach (var target in dailyPlan.Targets)
            {
                menu.Targets.Add(new NutritionalTargetDto
                {
                    Nutrient = target.Nutrient,
                    ExpectedQuantity = intakePercentage * target.ExpectedQuantity,
                    ExpectedError = target.ExpectedError,
                    Unit = target.Unit,
                    ThresholdType = target.ThresholdType,
                    IsPriority = target.IsPriority
                });
            }
        }
    }
}