using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class RecipeStep
    {
        public int Id { get; set; }
        public int Recipe { get; set; }
        public int Step { get; set; }
        public string? Description { get; set; }

        public virtual Recipe RecipeNavigation { get; set; } = null!;
    }
}
