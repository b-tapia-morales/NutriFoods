using System.ComponentModel.DataAnnotations;
using API.Dto;
using Microsoft.AspNetCore.Mvc;
using Utils.Enum;

namespace API.DailyMealPlans;

[ApiController]
[Route("api/v1/daily-menus")]
public class DailyMealPlanController
{
    private readonly IDailyMealPlanService _dailyMealPlanService;

    public DailyMealPlanController(IDailyMealPlanService dailyMealPlanService)
    {
        _dailyMealPlanService = dailyMealPlanService;
    }

    [HttpGet]
    [Route("default-parameters")]
    public async Task<ActionResult<DailyMealPlanDto>> GenerateDailyMealPlan([Required] double energyTarget,
        [Required] Satiety breakfast, [Required] Satiety lunch, [Required] Satiety dinner, bool? includeBrunch = false,
        bool? includeLinner = false)
    {
        var mealConfigurations = new List<(MealTypeEnum MealType, SatietyEnum Satiety)>
        {
            (MealTypeEnum.Breakfast, SatietyEnum.FromToken(breakfast)),
            (MealTypeEnum.None, includeBrunch.GetValueOrDefault() ? SatietyEnum.Light : SatietyEnum.None),
            (MealTypeEnum.Lunch, SatietyEnum.FromToken(lunch)),
            (MealTypeEnum.None, includeLinner.GetValueOrDefault() ? SatietyEnum.Light : SatietyEnum.None),
            (MealTypeEnum.Dinner, SatietyEnum.FromToken(dinner)),
        };
        return await _dailyMealPlanService.GenerateDailyMealPlan(energyTarget, mealConfigurations);
    }
}