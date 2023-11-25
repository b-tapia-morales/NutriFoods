using API.Dto;
using Domain.Enum;
using Microsoft.AspNetCore.Mvc;
using Utils.Enumerable;

namespace API.DailyMenus;

[ApiController]
[Route("api/v1/daily-menus")]
public class DailyMenuController(IDailyMenuRepository repository)
{

    [HttpGet]
    [Route("distribution")]
    public DailyMenuDto GenerateMenu([FromQuery] MealToken mealToken, [FromQuery] string hour,
        [FromQuery] double energy, [FromQuery] double carbohydratesPct, [FromQuery] double fattyAcidsPct,
        [FromQuery] double proteinsPct, [FromQuery] double errorMargin)
    {
        var distributionDict =
            NutrientExtensions.GramsDistributionDict(energy, carbohydratesPct, fattyAcidsPct, proteinsPct);
        var targets =
            new List<NutritionalTargetDto>(
                TargetExtensions.DistributionToTargets(distributionDict, energy, errorMargin));

        var dailyMenu = new DailyMenuDto
        {
            IntakePercentage = (int)(errorMargin * 100),
            MealType = IEnum<MealTypes, MealToken>.ToReadableName(mealToken),
            Hour = hour,
            Targets = new List<NutritionalTargetDto>(targets)
        };
        return repository.GenerateMenu(dailyMenu);
    }
    
    [HttpGet]
    [Route("async")]
    public async Task<DailyMenuDto> GenerateMenuAsync([FromQuery] MealToken mealToken, [FromQuery] string hour,
        [FromQuery] double energy, [FromQuery] double carbohydratesPct, [FromQuery] double fattyAcidsPct,
        [FromQuery] double proteinsPct, [FromQuery] double errorMargin)
    {
        var distributionDict =
            NutrientExtensions.GramsDistributionDict(energy, carbohydratesPct, fattyAcidsPct, proteinsPct);
        var targets =
            new List<NutritionalTargetDto>(
                TargetExtensions.DistributionToTargets(distributionDict, energy, errorMargin));

        var dailyMenu = new DailyMenuDto
        {
            IntakePercentage = (int)(errorMargin * 100),
            MealType = IEnum<MealTypes, MealToken>.ToReadableName(mealToken),
            Hour = hour,
            Targets = new List<NutritionalTargetDto>(targets)
        };
        
        return await repository.GenerateMenuAsync(dailyMenu);
    }
}