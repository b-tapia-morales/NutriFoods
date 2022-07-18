using System.ComponentModel.DataAnnotations;
using API.Dto;
using API.Utils.Nutrition;
using Domain.Enum;
using Microsoft.AspNetCore.Mvc;

namespace API.MealPlans;

[ApiController]
[Route("api/v1/meal-plans")]
public class MealPlanController
{
    private readonly IMealPlanService _mealPlanService;

    public MealPlanController(IMealPlanService mealPlanService)
    {
        _mealPlanService = mealPlanService;
    }

    [HttpGet]
    [Route("metrics-based")]
    public ActionResult<MealPlanDto> GenerateBasedOnMetrics([Required] string gender, [Required] int height,
        [Required] double weight, [Required] int age, [Required] string physicalActivity,
        [Required] string isLunchFilling, [Required] string breakfastSatiety, [Required] string dinnerSatiety)
    {
        if (age is < 18 or > 60)
        {
            return new BadRequestObjectResult(
                "Minimum and maximum values allowed for age are 18 and 60 respectively)");
        }

        if (height is < 150 or > 200)
        {
            return new BadRequestObjectResult(
                "Minimum and maximum values allowed for height are 150 and 200 [cm] respectively)");
        }

        if (weight is < 50 or > 150)
        {
            return new BadRequestObjectResult(
                "Minimum and maximum values allowed for weight are 50 and 150 [kg] respectively)");
        }

        if (!Gender.TryFromName(gender, true, out var genderValue))
        {
            return new BadRequestObjectResult(
                $"Provided argument {genderValue} does not correspond to a valid gender.");
        }

        if (!PhysicalActivity.TryFromName(physicalActivity, true, out var activityValue))
        {
            return new BadRequestObjectResult(
                $"Provided argument {activityValue} does not correspond to a valid gender.");
        }

        var totalMetabolicRate = TotalMetabolicRate.Calculate(genderValue, weight, height, age, activityValue);
        Console.WriteLine(totalMetabolicRate);
        return GenerateBasedOnMbr(totalMetabolicRate, isLunchFilling, breakfastSatiety, dinnerSatiety);
    }

    [HttpGet]
    [Route("mbr-based")]
    public ActionResult<MealPlanDto> GenerateBasedOnMbr([Required] double totalMetabolicRate,
        [Required] string isLunchFilling, [Required] string breakfastSatiety, [Required] string dinnerSatiety)
    {
        var lunchValue = isLunchFilling.ToLower();
        if (lunchValue != "y" && lunchValue != "n")
        {
            return new BadRequestObjectResult(
                $"Expected a confirmation value for whether or not lunch would be filling (Expected 'y' or 'n', received {isLunchFilling} instead.");
        }

        if (!Satiety.TryFromName(breakfastSatiety, true, out var breakfastChoice) ||
            !Satiety.TryFromName(dinnerSatiety, true, out var dinnerChoice))
        {
            return new BadRequestObjectResult(
                "Arguments for either breakfast or dinner satiety does not correspond to a valid value.");
        }

        var lunchChoice = lunchValue == "y" ? Satiety.Filling : Satiety.Normal;
        return _mealPlanService.GenerateMealPlan(totalMetabolicRate, breakfastChoice, lunchChoice, dinnerChoice);
    }
}