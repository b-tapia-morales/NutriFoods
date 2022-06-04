using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class PrimaryGroup
    {
        public PrimaryGroup()
        {
            SecondaryGroups = new HashSet<SecondaryGroup>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<SecondaryGroup> SecondaryGroups { get; set; }
    }
}
