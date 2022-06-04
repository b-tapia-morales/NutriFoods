using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class UserAllergy
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int IngredientId { get; set; }

        public virtual Ingredient Ingredient { get; set; } = null!;
        public virtual UserProfile User { get; set; } = null!;
    }
}
