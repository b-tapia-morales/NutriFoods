// ReSharper disable ConvertToPrimaryConstructor

using System.ComponentModel.DataAnnotations;
using API.DailyMenus;
using API.Dto;
using API.Recipes;
using Domain.Enum;
using Microsoft.AspNetCore.Mvc;
using static Domain.Enum.IEnum<Domain.Enum.Nutrients, Domain.Enum.NutrientToken>;

namespace API.DailyPlans;

[ApiController]
[Route("api/v1/daily-plans")]
public class DailyPlanController
{
    private readonly IDailyMenuRepository _dailyMenuRepository;
    private readonly IRecipeRepository _recipeRepository;

    public DailyPlanController(IRecipeRepository recipeRepository, IDailyMenuRepository dailyMenuRepository)
    {
        _dailyMenuRepository = dailyMenuRepository;
        _recipeRepository = recipeRepository;
    }

    [HttpGet]
    [Route("by-distribution")]
    public async Task<DailyPlanDto> GeneratePlan([FromQuery] [Required] DayToken day,
        [FromQuery] [Required] double basalMetabolicRate,
        [FromQuery] [Required] double adjustmentFactor,
        [FromQuery] [Required] PhysicalActivityToken activityLevel,
        [FromQuery] [Required] double activityFactor,
        [FromQuery] [Required] IDictionary<string, double> macronutrientDist,
        [FromQuery] [Required] IDictionary<string, double> mealDist,
        [FromQuery] [Required] IDictionary<string, string> mealHours)
    {
        var totalMetabolicRate = (1 + adjustmentFactor) * basalMetabolicRate * activityFactor;
        var meals = MealTypeExtensions.MainTypes.Select(e => e.ReadableName);
        var recipes = await _recipeRepository.FindAll();
        var rawMenus = new List<DailyMenuDto>();
        foreach (var meal in meals)
        {
            var hour = mealHours[meal];
            var energy = mealDist[meal] * totalMetabolicRate;
            var distributionDict = macronutrientDist.ToDictionary(e => ToValue(e.Key), e => energy * e.Value);
            var targets = TargetExtensions.DistributionToTargets(distributionDict, energy, adjustmentFactor);
            rawMenus.Add(new DailyMenuDto
            {
                Hour = hour,
                IntakePercentage = (int)(adjustmentFactor * 100),
                MealType = meal,
                Targets = new List<NutritionalTargetDto>(targets)
            });
        }

        var dailyMenus = new List<DailyMenuDto>();
        await Parallel.ForEachAsync(rawMenus,
            async (dto, _) => { dailyMenus.Add(await _dailyMenuRepository.GenerateMenu(dto, recipes)); });


        return new DailyPlanDto
        {
            AdjustmentFactor = (int)(adjustmentFactor * 100),
            PhysicalActivityLevel = IEnum<PhysicalActivities, PhysicalActivityToken>.ToReadableName(activityLevel),
            PhysicalActivityFactor = activityFactor,
            Menus = dailyMenus
        };
    }
}