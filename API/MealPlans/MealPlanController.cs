using System.ComponentModel.DataAnnotations;
using API.Dto;
using API.Users;
using API.Utils.Nutrition;
using Domain.Enum;
using Microsoft.AspNetCore.Mvc;

namespace API.MealPlans;

[ApiController]
[Route("api/v1/meal-plans")]
public class MealPlanController
{
    private readonly IMealPlanService _mealPlanService;
    private readonly IUserRepository _userRepository;

    public MealPlanController(IMealPlanService mealPlanService, IUserRepository userRepository)
    {
        _mealPlanService = mealPlanService;
        _userRepository = userRepository;
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

    [HttpGet]
    [Route("user-based")]
    public ActionResult<MealPlanDto> GenerateBasedOnUser([Required] string apiKey,
        [Required] string isLunchFilling, [Required] string breakfastSatiety, [Required] string dinnerSatiety)
    {
        var user = _userRepository.Find(apiKey).Result;
        if (user == null)
        {
            return new NotFoundObjectResult("There's no user with the specified Api key.");
        }

        if (!user.BodyMetrics.Any())
        {
            return new NotFoundObjectResult(
                "There's a user with the specified Api key but it has no body metrics associated.");
        }

        var latestMetric = user.BodyMetrics.Last();
        var age = DateOnly.FromDateTime(DateTime.Now).Year - DateOnly.Parse(user.Birthdate).Year;
        return GenerateBasedOnMetrics(
            Gender.ReadOnlyDictionary[user.Gender].Name,
            (int) latestMetric.Height, latestMetric.Weight, age,
            PhysicalActivity.ReadOnlyDictionary[latestMetric.PhysicalActivityLevel].Name, isLunchFilling,
            breakfastSatiety, dinnerSatiety);
    }
}