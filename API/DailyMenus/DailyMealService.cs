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

    public async Task<DailyMenuDto> GenerateDailyMenu(double energyTarget, double carbsPercent, double fatsPercent,
        double proteinsPercent, MealType mealType = MealType.None, Satiety satiety = Satiety.None)
    {
        var recipes = await _recipeRepository.FindAll();
        var dailyMenu = await Task.FromResult(_geneticAlgorithm.GenerateSolution(recipes, 3, energyTarget, carbsPercent,
            fatsPercent, proteinsPercent));
        dailyMenu.MealType = MealTypeEnum.FromToken(mealType).ReadableName;
        dailyMenu.Satiety = SatietyEnum.FromToken(satiety).ReadableName;
        return dailyMenu;
    }

    public async Task<DailyMenuDto> GenerateDailyMenu(double energyTarget,
        MealType mealType = MealType.None, Satiety satiety = Satiety.None)
    {
        return await GenerateDailyMenu(energyTarget, Carbohydrates.DefaultPercent.GetValueOrDefault(),
            Lipids.DefaultPercent.GetValueOrDefault(), Proteins.DefaultPercent.GetValueOrDefault(), mealType, satiety);
    }
}