using Domain.Models;

namespace API.Dto;

public class DailyPlanDto
{
    public string Day { get; set; } = null!;
    public string PhysicalActivityLevel { get; set; } = null!;
    public double PhysicalActivityFactor { get; set; }
    public int AdjustmentFactor { get; set; }
    public ICollection<DailyPlanNutrientDto> Nutrients { get; set; } = new List<DailyPlanNutrientDto>();
    public ICollection<DailyPlanTargetDto> Targets { get; set; } = new List<DailyPlanTargetDto>();
    public ICollection<DailyMenuDto> Menus { get; set; } = new List<DailyMenuDto>();
}