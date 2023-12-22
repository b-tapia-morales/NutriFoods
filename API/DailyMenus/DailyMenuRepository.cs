// ReSharper disable ConvertToPrimaryConstructor

using API.ApplicationData;
using API.Dto;
using API.Optimizer;
using Domain.Enum;
using static Domain.Enum.IEnum<Domain.Enum.Nutrients, Domain.Enum.NutrientToken>;

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
        dailyMenu.Targets.Sort((e1, e2) => ToValue(e1.Nutrient).CompareTo(ToValue(e2.Nutrient)));
        dailyMenu.Nutrients = nutritionalValues;
        dailyMenu.Nutrients.Sort((e1, e2) => ToValue(e1.Nutrient).CompareTo(ToValue(e2.Nutrient)));
        solution.ForEach(e => e.FilterMacronutrients());
        dailyMenu.Recipes = [..solution.ToMenus()];
        return dailyMenu;
    }
}