using System;
using System.Collections.Generic;
using Domain.Enum;

namespace Domain.Models;

public class DailyPlan
{
    public int Id { get; set; }

    public List<Days> Days { get; set; } = null!;

    public PhysicalActivities PhysicalActivityLevel { get; set; } = null!;

    public double PhysicalActivityFactor { get; set; }

    public double AdjustmentFactor { get; set; }

    public virtual ICollection<DailyMenu> DailyMenus { get; set; } = null!;

    public virtual ICollection<Consultation> Consultations { get; set; } = null!;

    public virtual ICollection<NutritionalTarget> NutritionalTargets { get; set; } = null!;

    public virtual ICollection<NutritionalValue> NutritionalValues { get; set; } = null!;
}