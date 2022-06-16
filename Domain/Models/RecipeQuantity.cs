using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class RecipeQuantity
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public double Grams { get; set; }
        public string? Description { get; set; }

        public virtual Ingredient Ingredient { get; set; } = null!;
        public virtual Recipe Recipe { get; set; } = null!;
    }
}
