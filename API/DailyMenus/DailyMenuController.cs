using API.Dto;
using Domain.Enum;
using Microsoft.AspNetCore.Mvc;

namespace API.DailyMenus;

[ApiController]
[Route("api/v1/daily-menus")]
public class DailyMenuController(IDailyMenuRepository repository)
{
    [HttpGet]
    [Route("")]
    public async Task<DailyMenuDto> GenerateMenu([FromBody] DailyMenuDto dailyMenu) =>
        await repository.GenerateMenuAsync(dailyMenu);

    [HttpGet]
    [Route("distribution")]
    public async Task<DailyMenuDto> GenerateMenu([FromQuery] MealToken mealToken, [FromQuery] double energy,
        [FromQuery] int carbohydratesPct, [FromQuery] int fattyAcidsPct, [FromQuery] int proteinsPct,
        [FromQuery] int errorMargin)
    {
        var dailyMenu = new DailyMenuDto
        {
            IntakePercentage = errorMargin,
            MealType = IEnum<MealTypes, MealToken>.ToReadableName(mealToken),
            Targets = new List<NutritionalTargetDto>(TargetExtensions.DistributionToTargets(energy, carbohydratesPct,
                fattyAcidsPct, proteinsPct, errorMargin))
        };
        return await repository.GenerateMenuAsync(dailyMenu);
    }
}