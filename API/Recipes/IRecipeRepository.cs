using Domain.Models;

namespace API.Recipes;

public interface IRecipeRepository
{
    Task<List<Recipe>> GetRecipes();

    Task<List<Recipe>> ExcludeSecondaryGroups(IEnumerable<int> ids);

    Task<List<Recipe>> ExcludeTertiaryGroups(IEnumerable<int> ids);

    Task<Recipe> FindRecipe(string name);

    Task<Recipe> FindRecipe(int id);
}