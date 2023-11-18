// ReSharper disable ClassWithVirtualMembersNeverInherited.Global

using Domain.Enum;

namespace Domain.Models;

public class NutritionalTarget
{
    public int Id { get; set; }

    public Nutrients Nutrient { get; set; } = null!;

    public double Quantity { get; set; }

    public Units Unit { get; set; } = null!;

    public ThresholdTypes ThresholdType { get; set; } = null!;

    public virtual ICollection<DailyMenu> DailyMenus { get; set; } = null!;

    public virtual ICollection<DailyPlan> DailyPlans { get; set; } = null!;
}