// ReSharper disable ConvertToPrimaryConstructor

using API.ApplicationData;
using API.Dto;
using API.Optimizer;
using Domain.Enum;

namespace API.DailyMenus;

public class DailyMenuRepository : IDailyMenuRepository
{
    private readonly IApplicationData _applicationData;

    public DailyMenuRepository(IApplicationData applicationData) => _applicationData = applicationData;

    public async Task<DailyMenuDto> GenerateMenu(DailyMenuDto dailyMenu, MealTypes mealType)
    {
        var recipes = _applicationData.MealRecipesDict[mealType].AsReadOnly();
        var energy = dailyMenu.Targets.First(e => e.Nutrient == Nutrients.Energy.ReadableName).ExpectedQuantity;
        var chromosomeSize = _applicationData.RatioPerPortion(mealType, NutrientToken.Energy, energy);
        var solution =
            await IEvolutionaryOptimizer<GeneticOptimizer>.GenerateSolutionAsync(recipes,
                dailyMenu.Targets.AsReadOnly(), chromosomeSize < 2 ? 2 : chromosomeSize);
        var nutritionalValues = new List<NutritionalValueDto>(solution.ToNutritionalValues());
        dailyMenu.Targets.IncludeActualValues(solution);
        dailyMenu.Nutrients = nutritionalValues;
        solution.ForEach(e => e.FilterMacronutrients());
        dailyMenu.Recipes = [..solution.ToMenus()];
        return dailyMenu;
    }
}