using System.ComponentModel.DataAnnotations;
using API.ApplicationData;
using API.Dto;
using Domain.Enum;
using Microsoft.AspNetCore.Mvc;

namespace API.DailyMenus;

[ApiController]
[Route("api/v1/daily-menus")]
public class DailyMenuController(IDailyMenuRepository repository, IApplicationData applicationData)
{
    [HttpGet]
    [Route("by-distribution")]
    public async Task<DailyMenuDto> GenerateMenuAsync([FromQuery] [Required] MealToken mealToken,
        [FromQuery] string hour, [FromQuery] double energy, [FromQuery] double carbohydratesPct, 
        [FromQuery] double fattyAcidsPct, [FromQuery] double proteinsPct, [FromQuery] double errorMargin)
    {
        var distributionDict =
            NutrientExtensions.GramsDistributionDict(energy, carbohydratesPct, fattyAcidsPct, proteinsPct);
        var targets =
            new List<NutritionalTargetDto>(
                TargetExtensions.DistributionToTargets(distributionDict, energy, errorMargin));

        var mealType = IEnum<MealTypes, MealToken>.ToValue(mealToken);
        var dailyMenu = new DailyMenuDto
        {
            IntakePercentage = (int)(errorMargin * 100),
            MealType = mealType.ReadableName,
            Hour = hour,
            Targets = new List<NutritionalTargetDto>(targets)
        };

        var chromosomeSize = applicationData.RatioPerPortion(mealToken, NutrientToken.Energy, energy);
        var ratio = applicationData.DefaultRatio;
        return await repository.GenerateMenuAsync(dailyMenu, mealType, energy, chromosomeSize, ratio);
    }
}