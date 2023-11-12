namespace Domain.Models;

public class MealPlan
{
    public int Id { get; set; }

    public int MealsPerDay { get; set; }

    public DateTime? CreatedOn { get; set; }

    public virtual ICollection<Consultation> Consultations { get; set; } = new List<Consultation>();

    public virtual ICollection<DailyPlan> DailyPlans { get; set; } = new List<DailyPlan>();
}
