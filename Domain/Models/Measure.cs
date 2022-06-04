using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Measure
    {
        public Measure()
        {
            IngredientMeasures = new HashSet<IngredientMeasure>();
        }

        public int Id { get; set; }
        public int IngredientId { get; set; }
        public string Name { get; set; } = null!;
        public double Grams { get; set; }
        public bool IsDefault { get; set; }

        public virtual Ingredient Ingredient { get; set; } = null!;
        public virtual ICollection<IngredientMeasure> IngredientMeasures { get; set; }
    }
}
