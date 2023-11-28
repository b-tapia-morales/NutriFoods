using System.ComponentModel.DataAnnotations;
using API.Dto;
using Domain.Enum;
using Microsoft.AspNetCore.Mvc;

namespace API.DailyPlans;

[ApiController]
[Route("api/v1/daily-plans")]
public class DailyPlanController
{
    [HttpGet]
    [Route("by-distribution")]
    public async Task<DailyPlanDto> GeneratePlan([FromQuery, Required] DayToken day,
        [FromQuery, Required] IList<string> hours, [FromQuery, Required] double energy,
        [FromQuery, Required] double adjustmentFactor, [FromQuery, Required] PhysicalActivityToken activityLevel,
        [FromQuery, Required] double activityFactor, [FromQuery, Required] IDictionary<string, double> meals,
        [FromQuery, Required] IDictionary<string, double> macronutrients)
    {
        var totalMetabolicRate = (1 + adjustmentFactor) * energy * activityFactor;
        var mealsDict =
            meals.ToDictionary(e => IEnum<MealTypes, MealToken>.ToValue(e.Key), e => totalMetabolicRate * e.Value);
        var distributionDict =
            macronutrients.ToDictionary(e => IEnum<Nutrients, NutrientToken>.ToValue(e.Key),
                e => totalMetabolicRate * e.Value);
        return null!;
    }
}