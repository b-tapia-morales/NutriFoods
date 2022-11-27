using API.Dto;
using API.Genetic;
using API.Recipes;
using Utils.Enum;
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

    public async Task<DailyMenuDto> GenerateDailyMenu(MealTypeEnum mealType, double energyTarget, double carbsPercent,
        double fatsPercent, double proteinsPercent)
    {
        var recipes = await _recipeRepository.FindAll();
        var dailyMenu = await Task.FromResult(_geneticAlgorithm.GenerateSolution(recipes, 3, energyTarget, carbsPercent,
            fatsPercent, proteinsPercent));
        dailyMenu.MealType = mealType.ReadableName;
        return dailyMenu;
    }

    public async Task<DailyMenuDto> GenerateDailyMenu(MealTypeEnum mealType, double energyTarget)
    {
        return await GenerateDailyMenu(mealType, energyTarget, Carbohydrates.DefaultPercent.GetValueOrDefault(),
            Lipids.DefaultPercent.GetValueOrDefault(), Proteins.DefaultPercent.GetValueOrDefault());
    }
}