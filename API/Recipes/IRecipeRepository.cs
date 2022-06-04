using Domain.Models;

namespace API.Recipes;

public interface IRecipeRepository
{
    Task<List<Recipe>> GetRecipes();

    Task<List<Recipe>> GetVegetarianRecipes();

    Task<Recipe> FindRecipe(string name);

    Task<Recipe> FindRecipe(int id);
}