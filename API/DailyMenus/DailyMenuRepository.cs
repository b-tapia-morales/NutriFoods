// ReSharper disable ConvertToPrimaryConstructor

using API.ApplicationData;
using API.Dto;
using API.Dto.Abridged;
using API.Optimizer;
using API.Recipes;
using AutoMapper;
using Domain.Enum;
using Utils.Enumerable;

namespace API.DailyMenus;

public class DailyMenuRepository : IDailyMenuRepository
{
    private readonly IMapper _mapper;
    private readonly IApplicationData _applicationData;

    public DailyMenuRepository(IMapper mapper, IApplicationData applicationData)
    {
        _mapper = mapper;
        _applicationData = applicationData;
    }

    public async Task<DailyMenuDto> GenerateMenu(DailyMenuDto dailyMenu, IReadOnlyList<RecipeDto> recipes)
    {
        var mealType = IEnum<MealTypes, MealToken>.ToToken(dailyMenu.MealType);
        var energy = dailyMenu.Targets.First(e => e.Nutrient == Nutrients.Energy.ReadableName).ExpectedQuantity;
        var chromosomeSize = _applicationData.RatioPerPortion(mealType, NutrientToken.Energy, energy);
        var solution =
            await IEvolutionaryOptimizer<GeneticOptimizer>.GenerateSolutionAsync(recipes,
                dailyMenu.Targets.AsReadOnly(), chromosomeSize);
        var abridgedRecipes = _mapper.Map<List<RecipeAbridged>>(solution);
        var menus = new List<MenuRecipeDto>(abridgedRecipes.ToMenus());
        var nutritionalValues = new List<NutritionalValueDto>(solution.ToNutritionalValues());
        dailyMenu.Targets.IncludeActualValues(solution);
        dailyMenu.Nutrients = nutritionalValues;
        dailyMenu.Recipes = menus;
        return dailyMenu;
    }
}