// ReSharper disable ConvertToPrimaryConstructor

using System.ComponentModel.DataAnnotations;
using API.ApplicationData;
using API.Dto;
using Domain.Enum;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Utils.Enumerable;
using static Domain.Enum.IEnum<Domain.Enum.MealTypes, Domain.Enum.MealToken>;

namespace API.DailyMenus;

[ApiController]
[Route("api/v1/daily-menus")]
public class DailyMenuController
{
    private readonly IApplicationData _applicationData;
    private readonly IDailyMenuRepository _dailyMenuRepository;
    private readonly IValidator<DailyMenuQuery> _queryValidator;
    private readonly IValidator<DailyMenuDto> _jsonValidator;

    public DailyMenuController(
        IApplicationData applicationData,
        IDailyMenuRepository dailyMenuRepository,
        IValidator<DailyMenuQuery> queryValidator,
        IValidator<DailyMenuDto> jsonValidator)
    {
        _applicationData = applicationData;
        _dailyMenuRepository = dailyMenuRepository;
        _queryValidator = queryValidator;
        _jsonValidator = jsonValidator;
    }

    [HttpPost]
    [Route("")]
    public async Task<ActionResult<DailyMenuDto>> GenerateMenu([FromBody] DailyMenuDto dailyMenu)
    {
        var results = await _jsonValidator.ValidateAsync(dailyMenu);
        if (!results.IsValid)
            return new BadRequestObjectResult(
                $"""
                 Could not perform query because of the following errors:
                 {results.Errors.Select(e => e.ErrorMessage).ToJoinedString(Environment.NewLine)}
                 """
            );

        return await _dailyMenuRepository.GenerateMenu(dailyMenu, MealTypes.None);
    }

    [HttpGet]
    [Route("/distribution/")]
    public async Task<ActionResult<DailyMenuDto>> GenerateMenu([FromQuery, Required] MealToken mealToken,
        [FromQuery] string hour, [FromQuery] double energy, [FromQuery] double carbohydratesPct,
        [FromQuery] double fattyAcidsPct, [FromQuery] double proteinsPct, [FromQuery] double errorMargin)
    {
        var validation =
            new DailyMenuQuery(mealToken, hour, energy, carbohydratesPct, fattyAcidsPct, proteinsPct, errorMargin);
        var results = await _queryValidator.ValidateAsync(validation);
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
                TargetExtensions.MacroDistributionToTargets(distributionDict, energy, errorMargin));
        var dailyMenu = new DailyMenuDto
        {
            IntakePercentage = errorMargin,
            MealType = ToReadableName(mealToken),
            Hour = hour,
            Targets = [..targets]
        };

        return await _dailyMenuRepository.GenerateMenu(dailyMenu, MealTypes.None);
    }
}