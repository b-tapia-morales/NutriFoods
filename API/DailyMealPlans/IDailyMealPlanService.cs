using API.Dto;
using Utils.Enum;

namespace API.DailyMealPlans;

public interface IDailyMealPlanService
{
    public Task<DailyMealPlanDto> GenerateDailyMealPlan(double energyTarget,
        ICollection<(MealTypeEnum MealType, SatietyEnum Satiety)> mealConfigurations);
}