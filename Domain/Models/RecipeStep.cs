using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class RecipeStep
    {
        public int Id { get; set; }
        public int RecipeSectionId { get; set; }
        public int Step { get; set; }
        public string? Description { get; set; }

        public virtual RecipeSection RecipeSection { get; set; } = null!;
    }
}
