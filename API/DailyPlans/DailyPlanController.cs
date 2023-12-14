// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable ConvertToPrimaryConstructor
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using System.Collections.Concurrent;
using System.Threading.Tasks.Dataflow;
using API.ApplicationData;
using API.DailyMenus;
using API.Dto;
using Domain.Enum;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Utils.Enumerable;
using Utils.Parallel;

namespace API.DailyPlans;

[ApiController]
[Route("api/v1/daily-plans")]
public class DailyPlanController
{
    private readonly IValidator<DailyPlanDto> _planValidator;
    private readonly IDailyMenuRepository _dailyMenuRepository;

    public DailyPlanController(IDailyMenuRepository dailyMenuRepository, IValidator<DailyPlanDto> planValidator)
    {
        _dailyMenuRepository = dailyMenuRepository;
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

        var queue = new ConcurrentQueue<DailyMenuDto>();
        var tasks = _dailyMenuRepository.ToTasks(dailyPlan.Menus, false);
        await tasks.AsyncParallelForEach(e => Task.Run(() => { queue.Enqueue(e); }));

        dailyPlan.Menus.AddRange(queue);
        return dailyPlan;
    }

    [HttpPost]
    [Route("/distribution/")]
    public async Task<DailyPlanDto> GeneratePlan([FromBody] PlanConfiguration configuration)
    {
        var dailyPlan = new DailyPlanDto
        {
            Day = configuration.Day,
            AdjustmentFactor = configuration.AdjustmentFactor,
            PhysicalActivityLevel = configuration.ActivityLevel,
            PhysicalActivityFactor = configuration.ActivityFactor,
            Menus = []
        };

        var totalMetabolicRate = (1 + configuration.AdjustmentFactor) * configuration.BasalMetabolicRate *
                                 configuration.ActivityFactor;
        var menus = new List<DailyMenuDto>(DailyMenuExtensions.ToMenus(configuration, totalMetabolicRate));

        var queue = new ConcurrentQueue<DailyMenuDto>();
        var tasks = _dailyMenuRepository.ToTasks(menus, false);
        await tasks.AsyncParallelForEach(e => Task.Run(() => { queue.Enqueue(e); }));

        dailyPlan.Menus.AddRange(queue);
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