using Domain.Models;

namespace API.Recipes;

public interface IRecipeService
{
    public Task<List<Recipe>> GetVegetarianRecipes();

    public Task<List<Recipe>> GetOvoVegetarianRecipes();

    public Task<List<Recipe>> GetOvoLactoVegetarianRecipes();

    public Task<List<Recipe>> GetLactoVegetarianRecipes();
}