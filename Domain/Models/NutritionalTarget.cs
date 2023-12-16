using System;
using System.Collections.Generic;
using Domain.Enum;

namespace Domain.Models;

public class NutritionalTarget
{
    public int Id { get; set; }

    public Nutrients Nutrient { get; set; } = null!;

    public double ExpectedQuantity { get; set; }

    public double? ActualQuantity { get; set; }

    public double ExpectedError { get; set; }

    public double? ActualError { get; set; }

    public Units Unit { get; set; } = null!;

    public ThresholdTypes ThresholdType { get; set; } = null!;

    public bool IsPriority { get; set; }

    public virtual ICollection<DailyMenu> DailyMenus { get; set; } = null!;

    public virtual ICollection<DailyPlan> DailyPlans { get; set; } = null!;
}