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
    public ActionResult<DailyPlanDto> GenerateDailyMealPlan([Required] double energyTarget,
        [Required] bool isLunchFilling, [Required] Satiety breakfast, [Required] Satiety dinner,
        bool? includeBrunch = false, bool? includeLinner = false, DayOfTheWeek? dayOfWeek = DayOfTheWeek.None)
    {
        var timer = new Stopwatch();
        timer.Start();
        var mealConfigurations = new List<(MealTypeEnum MealType, SatietyEnum Satiety)>
        {
            (MealTypeEnum.Breakfast, SatietyEnum.FromToken(breakfast)),
            (MealTypeEnum.Snack, includeBrunch.GetValueOrDefault() ? SatietyEnum.Light : SatietyEnum.None),
            (MealTypeEnum.Lunch, SatietyEnum.FromToken(isLunchFilling ? Satiety.Filling : Satiety.Normal)),
            (MealTypeEnum.Snack, includeLinner.GetValueOrDefault() ? SatietyEnum.Light : SatietyEnum.None),
            (MealTypeEnum.Dinner, SatietyEnum.FromToken(dinner)),
        };
        var dailyMealPlan =
            _dailyMealPlanService.GenerateDailyMealPlan(energyTarget, mealConfigurations,
                dayOfWeek ?? DayOfTheWeek.None);
        timer.Stop();
        Console.WriteLine($"Tiempo transcurrido total: {timer.Elapsed.Milliseconds} [ms]");
        return dailyMealPlan;
    }
}