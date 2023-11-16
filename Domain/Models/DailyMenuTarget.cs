using System;
using System.Collections.Generic;
using Domain.Enum;

namespace Domain.Models;

public class DailyMenuTarget
{
    public int Id { get; set; }

    public int DailyMenuId { get; set; }

    public Nutrients Nutrient { get; set; } = null!;

    public double Quantity { get; set; }

    public Units Unit { get; set; } = null!;

    public ThresholdTypes ThresholdType { get; set; } = null!;

    public virtual DailyMenu DailyMenu { get; set; } = null!;
}
