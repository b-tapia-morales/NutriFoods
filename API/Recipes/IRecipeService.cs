using API.Dto;

namespace API.Recipes;

public interface IRecipeService
{
    Task<List<RecipeDto>> FindAll();

    Task<RecipeDto> FindByName(string name);

    Task<RecipeDto> FindById(int id);

    Task<List<RecipeDto>> GetVegetarianRecipes();

    Task<List<RecipeDto>> GetOvoVegetarianRecipes();

    Task<List<RecipeDto>> GetLactoVegetarianRecipes();

    Task<List<RecipeDto>> GetOvoLactoVegetarianRecipes();

    Task<List<RecipeDto>> GetPollotarianRecipes();

    Task<List<RecipeDto>> GetPescetarianRecipes();

    Task<List<RecipeDto>> GetPolloPescetarianRecipes();

    Task<List<RecipeDto>> FilterByPreparationTime(int lowerBound, int upperBound);

    Task<List<RecipeDto>> FilterByPortions(int portions);

    Task<List<RecipeDto>> FilterByPortions(int lowerBound, int upperBound);

    Task<List<RecipeDto>> FilterByEnergy(int lowerBound, int upperBound);
    
    Task<List<RecipeDto>> FilterByCarbohydrates(int lowerBound, int upperBound);
    
    Task<List<RecipeDto>> FilterByLipids(int lowerBound, int upperBound);
    
    Task<List<RecipeDto>> FilterByProteins(int lowerBound, int upperBound);
}