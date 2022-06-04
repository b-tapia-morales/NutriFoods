using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class IngredientMeasure
    {
        public int Id { get; set; }
        public int RecipeSectionId { get; set; }
        public int MeasureId { get; set; }
        public int IntegerPart { get; set; }
        public int Numerator { get; set; }
        public int Denominator { get; set; }
        public string? Description { get; set; }

        public virtual Measure Measure { get; set; } = null!;
        public virtual RecipeSection RecipeSection { get; set; } = null!;
    }
}
