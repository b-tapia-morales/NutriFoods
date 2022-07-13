using API.Dto;

namespace API.Recipes;

public interface IRecipeService
{
    public Task<List<RecipeDto>> FindAll();

    public Task<RecipeDto> FindByName(string name);

    public Task<RecipeDto> FindById(int id);

    public Task<List<RecipeDto>> GetVegetarianRecipes();

    public Task<List<RecipeDto>> GetOvoVegetarianRecipes();
    
    public Task<List<RecipeDto>> GetLactoVegetarianRecipes();
    
    public Task<List<RecipeDto>> GetOvoLactoVegetarianRecipes();

    public Task<List<RecipeDto>> GetPollotarianRecipes();

    public Task<List<RecipeDto>> GetPescetarianRecipes();

    public Task<List<RecipeDto>> GetPolloPescetarianRecipes();

    public Task<List<RecipeDto>> FilterByPreparationTime(int lowerBound, int upperBound);

    public Task<List<RecipeDto>> FilterByPortions(int portions);

    public Task<List<RecipeDto>> FilterByPortions(int lowerBound, int upperBound);
}