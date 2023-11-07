namespace Domain.Models;

public sealed class MealPlan
{
    public int Id { get; set; }

    public int MealsPerDay { get; set; }

    public DateTime? CreatedOn { get; set; }

    public ICollection<Consultation> Consultations { get; set; } = new List<Consultation>();

    public ICollection<DailyPlan> DailyPlans { get; set; } = new List<DailyPlan>();
}
