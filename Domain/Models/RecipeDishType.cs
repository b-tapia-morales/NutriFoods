using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class RecipeDishType
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int DishTypeId { get; set; }

        public virtual DishType DishType { get; set; } = null!;
        public virtual Recipe Recipe { get; set; } = null!;
    }
}
