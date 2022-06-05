using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class IngredientNutrient
    {
        public int Id { get; set; }
        public int IngredientId { get; set; }
        public int NutrientId { get; set; }
        public double Quantity { get; set; }
        public int? Unit { get; set; }

        public virtual Ingredient Ingredient { get; set; } = null!;
        public virtual Nutrient Nutrient { get; set; } = null!;
    }
}
