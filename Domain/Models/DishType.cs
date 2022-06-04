using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class DishType
    {
        public DishType()
        {
            RecipeDishTypes = new HashSet<RecipeDishType>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<RecipeDishType> RecipeDishTypes { get; set; }
    }
}
