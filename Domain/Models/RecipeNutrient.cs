using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class RecipeNutrient
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int NutrientId { get; set; }
        public double Quantity { get; set; }
        public int Unit { get; set; }

        public virtual Nutrient Nutrient { get; set; } = null!;
        public virtual Recipe Recipe { get; set; } = null!;
    }
}
