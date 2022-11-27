using System.ComponentModel.DataAnnotations;
using API.Dto;
using Microsoft.AspNetCore.Mvc;
using Utils.Enum;

namespace API.DailyMenus;

[ApiController]
[Route("api/v1/daily-meals")]
public class DailyMealController
{
    private readonly IDailyMenuService _dailyMenuService;

    public DailyMealController(IDailyMenuService dailyMenuService)
    {
        _dailyMenuService = dailyMenuService;
    }

    [HttpGet]
    [Route("default-percentages")]
    public async Task<ActionResult<DailyMenuDto>> GenerateDailyMenu([Required] double energyTarget,
        MealType mealType = MealType.None, Satiety satiety = Satiety.None)
    {
        return await _dailyMenuService.GenerateDailyMenu(energyTarget, mealType, satiety);
    }

    [HttpGet]
    [Route("custom-percentages")]
    public async Task<ActionResult<DailyMenuDto>> GenerateDailyMenu([Required] double energyTarget,
        [Required] double carbsPercent, [Required] double fatsPercent, [Required] double proteinsPercent,
        MealType mealType = MealType.None, Satiety satiety = Satiety.None)
    {
        return await _dailyMenuService.GenerateDailyMenu(energyTarget, carbsPercent, fatsPercent, proteinsPercent,
            mealType, satiety);
    }
}