using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class RecipeSection
    {
        public RecipeSection()
        {
            RecipeMeasures = new HashSet<RecipeMeasure>();
            RecipeQuantities = new HashSet<RecipeQuantity>();
            RecipeSteps = new HashSet<RecipeStep>();
        }

        public int Id { get; set; }
        public int RecipeId { get; set; }
        public string Name { get; set; } = null!;

        public virtual Recipe Recipe { get; set; } = null!;
        public virtual ICollection<RecipeMeasure> RecipeMeasures { get; set; }
        public virtual ICollection<RecipeQuantity> RecipeQuantities { get; set; }
        public virtual ICollection<RecipeStep> RecipeSteps { get; set; }
    }
}
