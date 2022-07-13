using API.Dto;

namespace API.Recipes;

public interface IRecipeRepository
{
    Task<List<RecipeDto>> FindAll();

    Task<RecipeDto> FindByName(string name);

    Task<RecipeDto> FindById(int id);

    Task<List<RecipeDto>> ExcludeSecondaryGroups(IEnumerable<int> ids);

    Task<List<RecipeDto>> ExcludeTertiaryGroups(IEnumerable<int> ids);

    Task<List<RecipeDto>> FilterByPreparationTime(int lowerBound, int upperBound);

    Task<List<RecipeDto>> FilterByPortions(int portions);

    Task<List<RecipeDto>> FilterByPortions(int lowerBound, int upperBound);
}