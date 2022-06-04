using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class RecipeSection
    {
        public RecipeSection()
        {
            IngredientMeasures = new HashSet<IngredientMeasure>();
            IngredientQuantities = new HashSet<IngredientQuantity>();
            RecipeSteps = new HashSet<RecipeStep>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual Recipe IdNavigation { get; set; } = null!;
        public virtual ICollection<IngredientMeasure> IngredientMeasures { get; set; }
        public virtual ICollection<IngredientQuantity> IngredientQuantities { get; set; }
        public virtual ICollection<RecipeStep> RecipeSteps { get; set; }
    }
}
