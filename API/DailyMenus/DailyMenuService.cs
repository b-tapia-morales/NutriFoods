using API.Dto;
using API.Genetic;
using API.Recipes;
using Microsoft.OpenApi.Extensions;
using Utils.Averages;
using Utils.Enum;
using Utils.Nutrition;
using static Utils.Nutrition.Macronutrient;

namespace API.DailyMenus;

public class DailyMenuService : IDailyMenuService
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly IGeneticAlgorithm _geneticAlgorithm;

    public DailyMenuService(IRecipeRepository recipeRepository, IGeneticAlgorithm geneticAlgorithm)
    {
        _recipeRepository = recipeRepository;
        _geneticAlgorithm = geneticAlgorithm;
    }

    public async Task<DailyMenuDto> GenerateDailyMenu(double energyTarget, double carbsPercent, double fatsPercent,
        double proteinsPercent, MealType mealType = MealType.None, Satiety satiety = Satiety.None)
    {
        var (carbohydrates, lipids, proteins) =
            EnergyDistribution.Calculate(energyTarget, carbsPercent, fatsPercent, proteinsPercent);
        var recipes = await RecipesToPopulation(mealType, energyTarget, carbohydrates, lipids, proteins);

        var fromToken = mealType is MealType.None or MealType.Snack
            ? MealTypeEnum.None
            : MealTypeEnum.FromToken(mealType);
        var recipesAmount =
            RecipeDistribution.CalculateRecipesAmount(energyTarget, carbohydrates, lipids, proteins, fromToken);
        Console.WriteLine($"{mealType.GetDisplayName()} {energyTarget}");
        var dailyMenu =
            await Task.FromResult(
                _geneticAlgorithm.GenerateSolution(recipes, energyTarget, carbohydrates, lipids, proteins,
                    recipesAmount));
        dailyMenu.MealType = MealTypeEnum.FromToken(mealType).ReadableName;
        dailyMenu.Satiety = SatietyEnum.FromToken(satiety).ReadableName;
        dailyMenu.EnergyTotal = CalculateNutrientTotal(dailyMenu, 1);
        dailyMenu.CarbohydratesTotal = CalculateNutrientTotal(dailyMenu, 2);
        dailyMenu.LipidsTotal = CalculateNutrientTotal(dailyMenu, 12);
        dailyMenu.ProteinsTotal = CalculateNutrientTotal(dailyMenu, 63);
        return dailyMenu;
    }

    public async Task<DailyMenuDto> GenerateDailyMenu(double energyTarget, MealType mealType = MealType.None,
        Satiety satiety = Satiety.None)
    {
        return await GenerateDailyMenu(energyTarget, Carbohydrates.DefaultPercent.GetValueOrDefault(),
            Lipids.DefaultPercent.GetValueOrDefault(), Proteins.DefaultPercent.GetValueOrDefault(), mealType, satiety);
    }

    private static double CalculateNutrientTotal(DailyMenuDto dailyMenu, int nutrientId)
    {
        return dailyMenu.MenuRecipes.Sum(e =>
            e.Portions * e.Recipe.Nutrients.First(r => r.Nutrient.Id == nutrientId).Quantity);
    }

    private Task<List<RecipeDto>> RecipesToPopulation(MealType mealType, double energyTarget, double carbohydrates,
        double lipids, double proteins)
    {
        var data = RecipeDistribution.CalculateDistributionLimits(energyTarget, carbohydrates, lipids, proteins);
        return mealType switch
        {
            MealType.Breakfast => RecipesToPopulationMealType(mealType, energyTarget, 550, data.EnergyLimits,
                data.CarbohydratesLimits, data.LipidsLimits, data.ProteinsLimits),
            MealType.Lunch => RecipesToPopulationMealType(mealType, energyTarget, 530, data.EnergyLimits,
                data.CarbohydratesLimits, data.LipidsLimits, data.ProteinsLimits),
            MealType.Dinner => RecipesToPopulationMealType(mealType, energyTarget, 615, data.EnergyLimits,
                data.CarbohydratesLimits, data.LipidsLimits, data.ProteinsLimits),
            _ => _recipeRepository.FilterByMacronutrientDistribution(data.EnergyLimits, data.CarbohydratesLimits,
                data.LipidsLimits, data.ProteinsLimits)
        };
    }

    private async Task<List<RecipeDto>> RecipesToPopulationMealType(MealType mealType, double energyTarget,
        double limits,
        double energyLimits, double carbohydratesLimits, double lipidsLimits, double proteinsLimits)
    {
        if (energyTarget <= limits)
        {
            return await _recipeRepository.FilterByMacronutrientDistribution(energyLimits,
                carbohydratesLimits,
                lipidsLimits, proteinsLimits);
        }

        return await (mealType is MealType.None or MealType.Snack
            ? _recipeRepository.FindWithPortions()
            : _recipeRepository.FindByMealType(mealType));
    }
}