// ReSharper disable ConvertToPrimaryConstructor
// ReSharper disable ClassNeverInstantiated.Global

using System.Collections.Concurrent;
using API.ApplicationData;
using API.DailyMenus;
using API.Dto;
using API.Recipes;
using Domain.Enum;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Utils.Enumerable;

namespace API.DailyPlans;

[ApiController]
[Route("api/v1/daily-plans")]
public class DailyPlanController
{
    private readonly IApplicationData _applicationData;
    private readonly IValidator<DailyPlanDto> _planValidator;
    private readonly IDailyMenuRepository _dailyMenuRepository;
    private readonly IRecipeRepository _recipeRepository;

    public DailyPlanController(IApplicationData applicationData, IDailyMenuRepository dailyMenuRepository,
        IRecipeRepository recipeRepository,
        IValidator<DailyPlanDto> planValidator)
    {
        _applicationData = applicationData;
        _dailyMenuRepository = dailyMenuRepository;
        _recipeRepository = recipeRepository;
        _planValidator = planValidator;
    }

    [HttpPost]
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
        var recipes = _applicationData.MealRecipesDict[MealTypes.None];
        var bag = new ConcurrentBag<DailyMenuDto>();

        await Parallel.ForEachAsync(dailyPlan.Menus,
            async (menu, _) => { bag.Add(await _dailyMenuRepository.GenerateMenu(menu, recipes)); });

        dailyPlan.Menus = bag.OrderBy(e => IEnum<MealTypes, MealToken>.ToValue(e.MealType)).ToList();
        return dailyPlan;
    }

    [HttpPost]
    [Route("by-distribution")]
    public async Task<DailyPlanDto> GeneratePlan([FromBody] PlanConfiguration configuration)
    {
        var totalMetabolicRate = (1 + configuration.AdjustmentFactor) * configuration.BasalMetabolicRate *
                                 configuration.ActivityFactor;
        var menus = new List<DailyMenuDto>(DailyMenuExtensions.ToMenus(configuration, totalMetabolicRate));

        var bag = new ConcurrentBag<DailyMenuDto>();
        ParallelOptions parallelOptions = new()
        {
            MaxDegreeOfParallelism = configuration.MealConfigurations.Count
        };
        await Parallel.ForEachAsync(menus, parallelOptions,
            async (menu, _) =>
            {
                var mealType = IEnum<MealTypes, MealToken>.ToValue(menu.MealType);
                var recipes = _applicationData.MealRecipesDict[mealType];
                bag.Add(await _dailyMenuRepository.GenerateMenu(menu, recipes).ConfigureAwait(false));
            });

        var dailyPlan = new DailyPlanDto
        {
            Day = configuration.Day,
            AdjustmentFactor = configuration.AdjustmentFactor,
            PhysicalActivityLevel = configuration.ActivityLevel,
            PhysicalActivityFactor = configuration.ActivityFactor,
            Menus = bag.OrderBy(e => IEnum<MealTypes, MealToken>.ToValue(e.MealType)).ToList()
        };
        dailyPlan.AddMenuTargets();
        return dailyPlan;
    }
}

public class PlanConfiguration
{
    public string Day { get; set; } = null!;
    public double BasalMetabolicRate { get; set; }
    public double AdjustmentFactor { get; set; }
    public string ActivityLevel { get; set; } = null!;
    public double ActivityFactor { get; set; }
    public IDictionary<string, double> Distribution { get; set; } = null!;
    public IList<MealConfiguration> MealConfigurations { get; set; } = null!;
}

public class MealConfiguration
{
    public string MealType { get; set; } = null!;
    public string Hour { get; set; } = null!;
    public double IntakePercentage { get; set; }
}