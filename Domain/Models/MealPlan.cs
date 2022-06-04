using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class MealPlan
    {
        public MealPlan()
        {
            MealMenus = new HashSet<MealMenu>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int MealsPerDay { get; set; }
        public double EnergyTarget { get; set; }
        public double CarbohydratesTarget { get; set; }
        public double LipidsTarget { get; set; }
        public double ProteinsTarget { get; set; }

        public virtual UserProfile User { get; set; } = null!;
        public virtual ICollection<MealMenu> MealMenus { get; set; }
    }
}
