using API.Dto;

namespace API.Recipes;

public interface IRecipeService
{
    public Task<IEnumerable<RecipeDto>> FindAll();

    public Task<RecipeDto> FindByName(string name);

    public Task<RecipeDto> FindById(int id);

    public Task<IEnumerable<RecipeDto>> GetVegetarianRecipes();

    public Task<IEnumerable<RecipeDto>> GetOvoVegetarianRecipes();
    
    public Task<IEnumerable<RecipeDto>> GetLactoVegetarianRecipes();
    
    public Task<IEnumerable<RecipeDto>> GetOvoLactoVegetarianRecipes();

    public Task<IEnumerable<RecipeDto>> GetPollotarianRecipes();

    public Task<IEnumerable<RecipeDto>> GetPescetarianRecipes();

    public Task<IEnumerable<RecipeDto>> GetPolloPescetarianRecipes();

    public Task<List<RecipeDto>> FilterByPreparationTime(int lowerBound, int upperBound);

    public Task<List<RecipeDto>> FilterByPortions(int portions);

    public Task<List<RecipeDto>> FilterByPortions(int lowerBound, int upperBound);
}