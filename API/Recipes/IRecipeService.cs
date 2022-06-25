using API.Dto;
using Domain.Models;

namespace API.Recipes;

public interface IRecipeService
{
    public Task<IEnumerable<RecipeDto>> FindAll();

    public Task<RecipeDto> FindByName(string name);

    public Task<RecipeDto> FindById(int id);
    
    public Task<IReadOnlyCollection<RecipeDto>> GetVegetarianRecipes();

    public Task<IReadOnlyCollection<RecipeDto>> GetOvoVegetarianRecipes();

    public Task<IReadOnlyCollection<RecipeDto>> GetOvoLactoVegetarianRecipes();

    public Task<IReadOnlyCollection<RecipeDto>> GetLactoVegetarianRecipes();
}