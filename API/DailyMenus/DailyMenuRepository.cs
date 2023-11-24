using API.Dto;
using API.Dto.Abridged;
using API.Optimizer;
using API.Recipes;
using AutoMapper;
using Utils.Enumerable;

namespace API.DailyMenus;

public class DailyMenuRepository(IMapper mapper, IRecipeRepository recipeRepository) : IDailyMenuRepository
{
    public async Task<DailyMenuDto> GenerateMenuAsync(DailyMenuDto dailyMenu) =>
        await GenerateMenuAsync(dailyMenu, await recipeRepository.FindAll());

    public async Task<DailyMenuDto> GenerateMenuAsync(DailyMenuDto dailyMenu, IReadOnlyList<RecipeDto> recipes)
    {
        var solution =
            await IEvolutionaryOptimizer<GeneticOptimizer>.GenerateSolutionAsync(recipes,
                dailyMenu.Targets.AsReadOnly());
        var abridgedRecipes = mapper.Map<List<RecipeAbridged>>(solution);
        var menus = new List<MenuRecipeDto>(abridgedRecipes.ToMenus());
        var nutritionalValues = new List<NutritionalValueDto>(solution.ToNutritionalValues());
        dailyMenu.Targets.IncludeActualValues(solution);
        dailyMenu.Nutrients = nutritionalValues;
        dailyMenu.Recipes = menus;
        return dailyMenu;
    }
}