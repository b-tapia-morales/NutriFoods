using API.Dto;
using API.Genetic;
using API.Recipes;
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
        double proteinsPercent, MealType mealType = MealType.None, Satiety satiety = Satiety.None,
        int recipesAmount = 3)
    {
        var recipes = await _recipeRepository.FindAny();
        var (carbohydrates, lipids, proteins) =
            EnergyDistribution.Calculate(energyTarget, carbsPercent, fatsPercent, proteinsPercent);
        var dailyMenu =
            await Task.FromResult(
                _geneticAlgorithm.GenerateSolution(recipes, energyTarget, carbohydrates, lipids, proteins, recipesAmount));
        dailyMenu.MealType = MealTypeEnum.FromToken(mealType).ReadableName;
        dailyMenu.Satiety = SatietyEnum.FromToken(satiety).ReadableName;
        dailyMenu.EnergyTotal = CalculateNutrientTotal(dailyMenu, 1);
        dailyMenu.CarbohydratesTotal = CalculateNutrientTotal(dailyMenu, 2);
        dailyMenu.LipidsTotal = CalculateNutrientTotal(dailyMenu, 12);
        dailyMenu.ProteinsTotal = CalculateNutrientTotal(dailyMenu, 63);
        return dailyMenu;
    }

    public async Task<DailyMenuDto> GenerateDailyMenu(double energyTarget, MealType mealType = MealType.None,
        Satiety satiety = Satiety.None, int recipesAmount = 3)
    {
        return await GenerateDailyMenu(energyTarget, Carbohydrates.DefaultPercent.GetValueOrDefault(),
            Lipids.DefaultPercent.GetValueOrDefault(), Proteins.DefaultPercent.GetValueOrDefault(), mealType, satiety, recipesAmount);
    }

    private static double CalculateNutrientTotal(DailyMenuDto dailyMenu, int nutrientId)
    {
        return dailyMenu.MenuRecipes.Sum(e =>
            e.Portions * e.Recipe.Nutrients.First(r => r.Nutrient.Id == nutrientId).Quantity);
    }
}