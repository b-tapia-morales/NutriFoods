// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable ConvertToPrimaryConstructor
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using API.DailyMenus;
using API.Dto;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Utils.Enumerable;

namespace API.DailyPlans;

[ApiController]
[Route("api/v1/daily-plans")]
public class DailyPlanController
{
    private readonly IValidator<PlanConfiguration> _planValidator;
    private readonly IDailyMenuRepository _dailyMenuRepository;

    public DailyPlanController(IDailyMenuRepository dailyMenuRepository, IValidator<PlanConfiguration> planValidator)
    {
        _dailyMenuRepository = dailyMenuRepository;
        _planValidator = planValidator;
    }

    [HttpPost]
    [Route("")]
    public async Task<ActionResult<DailyPlanDto>> GeneratePlan([FromBody] PlanConfiguration configuration)
    {
        var results = await _planValidator.ValidateAsync(configuration);
        if (!results.IsValid)
            return new BadRequestObjectResult(
                $"""
                 Could not perform query because of the following errors:
                 {results.Errors.Select(e => e.ErrorMessage).ToJoinedString(Environment.NewLine)}
                 """
            );

        var totalMetabolicRate = (1 + configuration.AdjustmentFactor / 2) * configuration.BasalMetabolicRate *
                                 configuration.ActivityFactor;

        var dailyPlan = new DailyPlanDto
        {
            Days = configuration.Days,
            AdjustmentFactor = configuration.AdjustmentFactor,
            PhysicalActivityLevel = configuration.ActivityLevel,
            PhysicalActivityFactor = configuration.ActivityFactor,
            Menus = [],
            Targets = [],
            Nutrients = []
        };

        dailyPlan.Targets.AddRange([..configuration.ToTargets(totalMetabolicRate)]);
        dailyPlan.Targets.AddRange(configuration.Targets);

        var menus = new List<DailyMenuDto>(DailyMenuExtensions.ToMenus(configuration, totalMetabolicRate));
        dailyPlan.Menus.AddRange([..await _dailyMenuRepository.Parallelize(menus, false)]);

        dailyPlan.AddNutritionalValues();
        dailyPlan.AddTargetValues();
        return dailyPlan;
    }
}

public class PlanConfiguration
{
    public List<string> Days { get; set; } = null!;
    public double BasalMetabolicRate { get; set; }
    public double AdjustmentFactor { get; set; }
    public string ActivityLevel { get; set; } = null!;
    public double ActivityFactor { get; set; }
    public IDictionary<string, double> Distribution { get; set; } = null!;
    public IList<MealConfiguration> MealConfigurations { get; set; } = null!;
    public IList<NutritionalTargetDto> Targets { get; set; } = null!;
}

public class MealConfiguration
{
    public string MealType { get; set; } = null!;
    public string Hour { get; set; } = null!;
    public double IntakePercentage { get; set; }
}