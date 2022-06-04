using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class UserBodyMetric
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public double BodyMassIndex { get; set; }
        public double? MuscleMassPercentage { get; set; }
        public int PhysicalActivityLevel { get; set; }
        public int? DietId { get; set; }

        public virtual Diet? Diet { get; set; }
        public virtual UserProfile User { get; set; } = null!;
    }
}
