using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
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
    public ActionResult<DailyMealPlanDto> GenerateDailyMealPlan([Required] double energyTarget,
        [Required] Satiety breakfast, [Required] Satiety lunch, [Required] Satiety dinner, bool? includeBrunch = false,
        bool? includeLinner = false)
    {
        var timer = new Stopwatch();
        timer.Start();
        var mealConfigurations = new List<(MealTypeEnum MealType, SatietyEnum Satiety)>
        {
            (MealTypeEnum.Breakfast, SatietyEnum.FromToken(breakfast)),
            (MealTypeEnum.Snack, includeBrunch.GetValueOrDefault() ? SatietyEnum.Light : SatietyEnum.None),
            (MealTypeEnum.Lunch, SatietyEnum.FromToken(lunch)),
            (MealTypeEnum.Snack, includeLinner.GetValueOrDefault() ? SatietyEnum.Light : SatietyEnum.None),
            (MealTypeEnum.Dinner, SatietyEnum.FromToken(dinner)),
        };
        var dailyMealPlan = _dailyMealPlanService.GenerateDailyMealPlan(energyTarget, mealConfigurations);
        timer.Stop();
        Console.WriteLine($"Tiempo transcurrido total: {timer.Elapsed.Milliseconds} [ms]");
        return dailyMealPlan;
    }
}