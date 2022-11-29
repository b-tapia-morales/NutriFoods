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
        var results = await Task.WhenAll(concurrentTasks);
        var dailyMenus = results.ToList();
        return new DailyMealPlanDto
        {
            DailyMenus = dailyMenus,
            EnergyTotal = dailyMenus.Sum(e => e.EnergyTotal),
            CarbohydratesTotal = dailyMenus.Sum(e => e.CarbohydratesTotal),
            LipidsTotal = dailyMenus.Sum(e => e.LipidsTotal),
            ProteinsTotal = dailyMenus.Sum(e => e.ProteinsTotal)
        };
    }

    private IEnumerable<Task<DailyMenuDto>> BuildTasks(double energyTarget,
        ICollection<(MealTypeEnum MealType, SatietyEnum Satiety)> mealConfigurations)
    {
        var denominator = mealConfigurations.Sum(e => e.Satiety.Value);
        foreach (var (mealType, satiety) in mealConfigurations)
        {
            if (satiety == SatietyEnum.None) continue;
            var numerator = (double) satiety.Value;
            var mealEnergyTarget = energyTarget * (numerator / denominator);
            yield return _dailyMenuService.GenerateDailyMenu(mealEnergyTarget, mealType.Token, satiety.Token);
        }
    }
}