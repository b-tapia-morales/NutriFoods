using API.Dto;
using Domain.Models;

namespace API.Recipes;

public interface IRecipeRepository
{
    Task<IEnumerable<RecipeDto>> FindAll();

    Task<RecipeDto> FindByName(string name);

    Task<RecipeDto> FindById(int id);
    
    Task<IReadOnlyCollection<RecipeDto>> ExcludeSecondaryGroups(IEnumerable<int> ids);

    Task<IReadOnlyCollection<RecipeDto>> ExcludeTertiaryGroups(IEnumerable<int> ids);
}