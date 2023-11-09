namespace API.Dto;

public class DailyPlanDto
{
    public string Day { get; set; } = null!;
    public string PhysicalActivityLevel { get; set; } = null!;
    public double PhysicalActivityFactor { get; set; }
    public int AdjustmentFactor { get; set; }
    public ICollection<DailyPlanNutrientDto> Nutrients { get; set; } = null!;
    public ICollection<DailyPlanTargetDto> Targets { get; set; } = null!;
    public ICollection<DailyMenuDto> Menus { get; set; } = null!;
}