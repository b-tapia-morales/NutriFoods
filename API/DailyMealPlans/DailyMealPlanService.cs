using System.Collections.Concurrent;
using API.DailyMenus;
using API.Dto;
using Utils.Enum;

namespace API.DailyMealPlans;

public class DailyMealPlanService : IDailyMealPlanService
{
    private readonly IDailyMenuService _dailyMenuService;

    public DailyMealPlanService(IDailyMenuService dailyMenuService)
    {
        _dailyMenuService = dailyMenuService;
    }

    public async Task<DailyMealPlanDto> GenerateDailyMealPlan(double energyTarget,
        ICollection<(MealTypeEnum MealType, SatietyEnum Satiety)> mealConfigurations)
    {
        var concurrentTasks = BuildTasks(energyTarget, mealConfigurations);
        await Task.WhenAll(concurrentTasks);
        var dailyMealPlan = new DailyMealPlanDto();
        foreach (var task in concurrentTasks)
        {
            dailyMealPlan.DailyMenus.Add(task.Result);
        }

        var mealsPerDay = mealConfigurations.Count(e => e.Satiety != SatietyEnum.None);
        dailyMealPlan.EnergyTotal = mealsPerDay * dailyMealPlan.EnergyTotal;
        dailyMealPlan.CarbohydratesTotal = mealsPerDay * dailyMealPlan.CarbohydratesTotal;
        dailyMealPlan.LipidsTotal = mealsPerDay * dailyMealPlan.LipidsTotal;
        dailyMealPlan.ProteinsTotal = mealsPerDay * dailyMealPlan.ProteinsTotal;
        return dailyMealPlan;
    }

    private List<Task<DailyMenuDto>> BuildTasks(double energyTarget,
        ICollection<(MealTypeEnum MealType, SatietyEnum Satiety)> mealConfigurations)
    {
        var denominator = mealConfigurations.Sum(e => e.Satiety.Value);
        var concurrentTasks = new List<Task<DailyMenuDto>>();
        foreach (var (mealType, satiety) in mealConfigurations)
        {
            if (satiety == SatietyEnum.None) continue;
            var numerator = (double) satiety.Value;
            var mealEnergyTarget = energyTarget * (numerator / denominator);
            concurrentTasks.Add(_dailyMenuService.GenerateDailyMenu(mealEnergyTarget, mealType.Token, satiety.Token));
        }

        return concurrentTasks;
    }
}