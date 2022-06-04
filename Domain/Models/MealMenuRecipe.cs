using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class MealMenuRecipe
    {
        public int Id { get; set; }
        public int MealMenuId { get; set; }
        public int RecipeId { get; set; }
        public int Quantity { get; set; }

        public virtual MealMenu MealMenu { get; set; } = null!;
        public virtual Recipe Recipe { get; set; } = null!;
    }
}
