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
        var (carbohydratesTarget, lipidsTarget, proteinsTarget) = EnergyDistribution.Calculate(energyTarget);
        var values =
            new List<Satiety> { breakfastSatiety, lunchSatiety, dinnerSatiety };
        var mealsPerDay = values.Count(e => e != Satiety.None);
        var denominator = values.Sum(e => e.Value);
        var mealPlan = MapToMealPlan(mealsPerDay, energyTarget, carbohydratesTarget, lipidsTarget, proteinsTarget);
        var mealMenus = new List<MealMenuDto>();
        _regime.GenerateSolution(3, 20, energyTarget,carbohydratesTarget,lipidsTarget,proteinsTarget);
        /*
        foreach (var satiety in values)
        {
            if (satiety == Satiety.None) continue;
            var numerator = (double) satiety.Value;
            var ratio = (numerator / denominator); 
            var geneticAlgorithm = new GeneticAlgorithm(4, ratio * energyTarget);
            var solution = geneticAlgorithm.GetRegimen();
            mealMenus.Add(MapToMealMenu(_mapper, solution, satiety));
        }
*/
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

    private static MealMenuDto MapToMealMenu(IMapper mapper, Solutions solution, Satiety satiety)
    {
        var recipes = solution.ListRecipe.Select(mapper.Map<Recipe, RecipeDto>);
        var mealMenuRecipes = recipes.Select(e => new MealMenuRecipeDto { Recipe = e, Quantity = 1 }).ToList();
        return new MealMenuDto
        {
            Satiety = satiety.Display,
            EnergyTotal = solution.CantKilocalories,
            CarbohydratesTotal = solution.CantCarbohydrates,
            LipidsTotal = solution.CantFats,
            ProteinsTotal = solution.CantProteins,
            MenuRecipes = mealMenuRecipes
        };
    }
}