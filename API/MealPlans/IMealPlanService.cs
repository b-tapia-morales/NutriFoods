using API.Dto;
using Domain.Enum;

namespace API.MealPlans;

public interface IMealPlanService
{
    MealPlanDto GenerateMealPlan(double energyTarget, Satiety breakfastSatiety, Satiety lunchSatiety,
        Satiety dinnerSatiety);
}