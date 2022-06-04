using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Diet
    {
        public Diet()
        {
            RecipeDiets = new HashSet<RecipeDiet>();
            UserBodyMetrics = new HashSet<UserBodyMetric>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<RecipeDiet> RecipeDiets { get; set; }
        public virtual ICollection<UserBodyMetric> UserBodyMetrics { get; set; }
    }
}
