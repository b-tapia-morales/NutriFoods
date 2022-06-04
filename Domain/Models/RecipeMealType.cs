using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class RecipeMealType
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int MealTypeId { get; set; }

        public virtual MealType MealType { get; set; } = null!;
        public virtual Recipe Recipe { get; set; } = null!;
    }
}
