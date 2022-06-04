using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class RecipeDiet
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int DietId { get; set; }

        public virtual Diet Diet { get; set; } = null!;
        public virtual Recipe Recipe { get; set; } = null!;
    }
}
