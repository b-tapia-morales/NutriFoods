using System.ComponentModel.DataAnnotations;
using API.ApplicationData;
using API.Dto;
using Domain.Enum;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Utils.Enumerable;

namespace API.DailyMenus;

[ApiController]
[Route("api/v1/daily-menus")]
public class DailyMenuController(IDailyMenuRepository repository, IApplicationData applicationData,
    IValidator<DailyMenuQuery> queryValidator, IValidator<DailyMenuDto> jsonValidator)
{
    [HttpGet]
    public async Task<ActionResult<DailyMenuDto>> GenerateMenu([FromBody] DailyMenuDto dailyMenu)
    {
        var results = await jsonValidator.ValidateAsync(dailyMenu);
        if (!results.IsValid)
            return new BadRequestObjectResult(
                $"""
                 Could not perform query because of the following errors:
                 {results.Errors.Select(e => e.ErrorMessage).ToJoinedString(Environment.NewLine)}
                 """
            );

        var chromosomeSize = applicationData.RatioPerPortion(IEnum<MealTypes, MealToken>.ToToken(dailyMenu.MealType),
            NutrientToken.Energy,
            dailyMenu.Targets.First(e => e.Nutrient == Nutrients.Energy.ReadableName).ExpectedQuantity);

        return await repository.GenerateMenu(dailyMenu, chromosomeSize);
    }

    [HttpGet]
    [Route("by-distribution")]
    public async Task<ActionResult<DailyMenuDto>> GenerateMenu([FromQuery, Required] MealToken mealToken,
        [FromQuery] string hour, [FromQuery] double energy, [FromQuery] double carbohydratesPct,
        [FromQuery] double fattyAcidsPct, [FromQuery] double proteinsPct, [FromQuery] double errorMargin)
    {
        var validation =
            new DailyMenuQuery(mealToken, hour, energy, carbohydratesPct, fattyAcidsPct, proteinsPct, errorMargin);
        var results = await queryValidator.ValidateAsync(validation);
        if (!results.IsValid)
            return new BadRequestObjectResult(
                $"""
                 Could not perform query because of the following errors:
                 {results.Errors.Select(e => e.ErrorMessage).ToJoinedString(Environment.NewLine)}
                 """
            );

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
        return await repository.GenerateMenu(dailyMenu, chromosomeSize);
    }
}