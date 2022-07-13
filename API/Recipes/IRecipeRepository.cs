using API.Dto;

namespace API.Recipes;

public interface IRecipeRepository
{
    Task<IEnumerable<RecipeDto>> FindAll();

    Task<RecipeDto> FindByName(string name);

    Task<RecipeDto> FindById(int id);

    Task<IEnumerable<RecipeDto>> ExcludeSecondaryGroups(IEnumerable<int> ids);

    Task<IEnumerable<RecipeDto>> ExcludeTertiaryGroups(IEnumerable<int> ids);

    Task<IEnumerable<RecipeDto>> FilterByPreparationTime(int lowerBound, int upperBound);

    Task<IEnumerable<RecipeDto>> FilterByPortions(int portions);

    Task<IEnumerable<RecipeDto>> FilterByPortions(int lowerBound, int upperBound);
}