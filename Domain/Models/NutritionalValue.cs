// ReSharper disable ClassWithVirtualMembersNeverInherited.Global

using Domain.Enum;

namespace Domain.Models;

public class NutritionalValue
{
    public int Id { get; set; }

    public Nutrients Nutrient { get; set; } = null!;

    public double Quantity { get; set; }

    public Units Unit { get; set; } = null!;

    public double ErrorMargin { get; set; }

    public virtual ICollection<DailyMenu> DailyMenus { get; set; } = new List<DailyMenu>();

    public virtual ICollection<DailyPlan> DailyPlans { get; set; } = new List<DailyPlan>();
}
