using System.Diagnostics;
using API.Dto;
using API.Genetic;
using AutoMapper;
using Domain.Models;
using Utils.Enum;
using Utils.Nutrition;

namespace API.MealPlans;

public class MealPlanService : IMealPlanService
{
    private readonly IMapper _mapper;
    private readonly IGeneticAlgorithm _regime;

    public MealPlanService(IMapper mapper, IGeneticAlgorithm regime)
    {
        _mapper = mapper;
        _regime = regime;
    }

    public MealPlanDto GenerateMealPlan(double energyTarget, Satiety breakfastSatiety,
        Satiety lunchSatiety, Satiety dinnerSatiety)
    {
        var timeMeasure = new Stopwatch();
        var (carbohydratesTarget, lipidsTarget, proteinsTarget) = EnergyDistribution.Calculate(energyTarget);
        var values =
            new List<Satiety> { breakfastSatiety, lunchSatiety, dinnerSatiety };
        var mealsPerDay = values.Count(e => e != Satiety.None);
        var denominator = values.Sum(e => e.Value);
        var mealPlan = MapToMealPlan(mealsPerDay, energyTarget, carbohydratesTarget, lipidsTarget, proteinsTarget);
        var mealMenus = new List<MealMenuDto>();
        var mealtype = new MealType {Id = 1,Name = "unnombre"};
        timeMeasure.Start();
        foreach (var satiety in values)
        {
            if (satiety == Satiety.None) continue;
            var numerator = (double)satiety.Value;
            var ratio = (numerator / denominator) * energyTarget;
            var energy = EnergyDistribution.Calculate(ratio);
            var mealPlanSolution = _regime.GenerateSolution(3, 20,
                ratio, energy.Carbohydrates, energy.Lipids, energy.Proteins, mealtype);
            mealMenus.Add(mealPlanSolution);
            
        }
        timeMeasure.Stop();
        Console.WriteLine($"Tiempo de ejecucion : {timeMeasure.Elapsed.TotalMilliseconds} ms");
        mealPlan.MealMenus = mealMenus;
        return mealPlan;
    }

    private static MealPlanDto MapToMealPlan(int mealsPerDay, double energyTarget, double carbohydratesTarget,
        double lipidsTarget, double proteinsTarget)
    {
        return new MealPlanDto
        {
            MealsPerDay = mealsPerDay,
            EnergyTarget = energyTarget,
            CarbohydratesTarget = carbohydratesTarget,
            LipidsTarget = lipidsTarget,
            ProteinsTarget = proteinsTarget
        };
    }
}