using API.ApplicationData;
using API.Dto;
using API.Dto.Abridged;
using API.Optimizer;
using AutoMapper;
using Domain.Enum;

namespace API.DailyPlans;

public class DailyPlanRepository : IDailyPlanRepository
{
    private readonly IMapper _mapper;
    private readonly IApplicationData _applicationData;

    public DailyPlanRepository(IApplicationData applicationData, IMapper mapper)
    {
        _mapper = mapper;
        _applicationData = applicationData;
    }

    public async Task<DailyMenuDto> GenerateMenus(DailyMenuDto dailyMenu, IReadOnlyList<RecipeDto> recipes)
    {
        var mealType = IEnum<MealTypes, MealToken>.ToToken(dailyMenu.MealType);
        var energy = dailyMenu.Targets.First(e => e.Nutrient == Nutrients.Energy.ReadableName).ExpectedQuantity;
        var chromosomeSize = _applicationData.RatioPerPortion(mealType, NutrientToken.Energy, energy);
        var solution =
            await IEvolutionaryOptimizer<GeneticOptimizer>.GenerateSolutionAsync(recipes,
                dailyMenu.Targets.AsReadOnly(), chromosomeSize < 2 ? 2 : chromosomeSize);
        var abridgedRecipes = _mapper.Map<List<RecipeAbridged>>(solution);
        dailyMenu.Recipes = [..abridgedRecipes.ToMenus()];
        dailyMenu.Targets.IncludeActualValues(solution);
        return dailyMenu;
    }
}