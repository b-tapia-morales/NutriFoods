using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class UserProfile
    {
        public UserProfile()
        {
            MealPlans = new HashSet<MealPlan>();
            UserAllergies = new HashSet<UserAllergy>();
            UserBodyMetrics = new HashSet<UserBodyMetric>();
        }

        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ApiKey { get; set; } = null!;
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public DateOnly Birthdate { get; set; }
        public int Gender { get; set; }
        public DateTime JoinedOn { get; set; }

        public virtual ICollection<MealPlan> MealPlans { get; set; }
        public virtual ICollection<UserAllergy> UserAllergies { get; set; }
        public virtual ICollection<UserBodyMetric> UserBodyMetrics { get; set; }
    }
}
