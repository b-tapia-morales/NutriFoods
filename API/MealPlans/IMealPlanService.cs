using API.Dto;
using Utils.Enum;

namespace API.MealPlans;

public interface IMealPlanService
{
    MealPlanDto GenerateMealPlan(double energyTarget, Satiety breakfastSatiety, Satiety lunchSatiety,
        Satiety dinnerSatiety);
}