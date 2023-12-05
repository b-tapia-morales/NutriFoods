// ReSharper disable ConvertToPrimaryConstructor
// ReSharper disable ClassNeverInstantiated.Global

using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using API.DailyMenus;
using API.Dto;
using API.Recipes;
using Domain.Enum;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Utils.Enumerable;
using static Domain.Enum.IEnum<Domain.Enum.Nutrients, Domain.Enum.NutrientToken>;

namespace API.DailyPlans;

[ApiController]
[Route("api/v1/daily-plans")]
public class DailyPlanController
{
    private readonly IValidator<DailyPlanDto> _planValidator;
    private readonly IDailyMenuRepository _dailyMenuRepository;
    private readonly IRecipeRepository _recipeRepository;

    public DailyPlanController(IDailyMenuRepository dailyMenuRepository, IRecipeRepository recipeRepository,
        IValidator<DailyPlanDto> planValidator)
    {
        _dailyMenuRepository = dailyMenuRepository;
        _recipeRepository = recipeRepository;
        _planValidator = planValidator;
    }

    [HttpGet]
    [Route("")]
    public async Task<ActionResult<DailyPlanDto>> GeneratePlan([FromBody] DailyPlanDto dailyPlan)
    {
        var results = await _planValidator.ValidateAsync(dailyPlan);
        if (!results.IsValid)
            return new BadRequestObjectResult(
                $"""
                 Could not perform query because of the following errors:
                 {results.Errors.Select(e => e.ErrorMessage).ToJoinedString(Environment.NewLine)}
                 """
            );

        dailyPlan.AddMenuTargets();
        var recipes = await _recipeRepository.FindAll();
        var bag = new ConcurrentBag<DailyMenuDto>();

        await Parallel.ForEachAsync(dailyPlan.Menus,
            async (menu, _) => { bag.Add(await _dailyMenuRepository.GenerateMenu(menu, recipes)); });

        dailyPlan.Menus = bag.OrderBy(e => IEnum<MealTypes, MealToken>.ToValue(e.MealType)).ToList();
        return dailyPlan;
    }

    [HttpPost]
    [Route("by-distribution")]
    public async Task<DailyPlanDto> GeneratePlan([FromQuery] [Required] DayToken day,
        [FromQuery] double basalMetabolicRate,
        [FromQuery] PhysicalActivityToken activityLevel,
        [FromQuery] double activityFactor,
        [FromQuery] IDictionary<string, double> macronutrientDist,
        [FromBody] IEnumerable<MealConfiguration> mealConfigurations,
        [FromQuery] double adjustmentFactor = 1e-1)
    {
        var totalMetabolicRate = (1 + adjustmentFactor) * basalMetabolicRate * activityFactor;
        var rawMenus = new List<DailyMenuDto>();
        foreach (var configuration in mealConfigurations)
        {
            var energy = configuration.IntakePercentage * totalMetabolicRate;
            var distributionDict = macronutrientDist.ToDictionary(e => ToValue(e.Key), e => energy * e.Value);
            var targets =
                TargetExtensions.DistributionToTargets(distributionDict, totalMetabolicRate, adjustmentFactor);
            rawMenus.Add(new DailyMenuDto
            {
                Hour = configuration.Hour,
                MealType = configuration.MealType,
                IntakePercentage = configuration.IntakePercentage,
                Targets = new List<NutritionalTargetDto>(targets)
            });
        }

        var recipes = await _recipeRepository.FindAll();
        var bag = new ConcurrentBag<DailyMenuDto>();
        await Parallel.ForEachAsync(rawMenus,
            async (menu, _) => { bag.Add(await _dailyMenuRepository.GenerateMenu(menu, recipes)); });


        return new DailyPlanDto
        {
            AdjustmentFactor = adjustmentFactor,
            PhysicalActivityLevel = IEnum<PhysicalActivities, PhysicalActivityToken>.ToReadableName(activityLevel),
            PhysicalActivityFactor = activityFactor,
            Menus = bag.OrderBy(e => IEnum<MealTypes, MealToken>.ToValue(e.MealType)).ToList()
        };
    }
}

public class MealConfiguration
{
    public string MealType { get; set; } = null!;
    public string Hour { get; set; } = null!;
    public double IntakePercentage { get; set; }
}