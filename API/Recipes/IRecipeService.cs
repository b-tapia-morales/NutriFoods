using API.Dto;
using Domain.Models;

namespace API.Recipes;

public interface IRecipeService
{
    public Task<IEnumerable<RecipeDto>> FindAll();

    public Task<RecipeDto> FindByName(string name);

    public Task<RecipeDto> FindById(int id);
    
    public Task<IEnumerable<RecipeDto>> GetVegetarianRecipes();

    public Task<IEnumerable<RecipeDto>> GetOvoVegetarianRecipes();

    public Task<IEnumerable<RecipeDto>> GetOvoLactoVegetarianRecipes();

    public Task<IEnumerable<RecipeDto>> GetLactoVegetarianRecipes();
}