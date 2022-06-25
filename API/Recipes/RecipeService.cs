using Domain.Models;

namespace API.Recipes;

public class RecipeService : IRecipeService
{
    private readonly List<int> _nonLactoVegetarianGroups = new() {10, 11, 12, 13, 14};
    private readonly List<int> _nonOvoLactoVegetarianGroups = new() {10, 11, 12, 13};
    private readonly List<int> _nonOvoVegetarianGroups = new() {10, 11, 12, 13, 18, 19, 20};
    private readonly List<int> _nonVegetarianGroups = new() {10, 11};

    private readonly IRecipeRepository _repository;

    public RecipeService(IRecipeRepository repository)
    {
        _repository = repository;
    }

    public Task<List<Recipe>> GetVegetarianRecipes()
    {
        return _repository.ExcludeSecondaryGroups(_nonVegetarianGroups);
    }

    public Task<List<Recipe>> GetOvoVegetarianRecipes()
    {
        return _repository.ExcludeTertiaryGroups(_nonOvoVegetarianGroups);
    }

    public Task<List<Recipe>> GetOvoLactoVegetarianRecipes()
    {
        return _repository.ExcludeTertiaryGroups(_nonOvoLactoVegetarianGroups);
    }

    public Task<List<Recipe>> GetLactoVegetarianRecipes()
    {
        return _repository.ExcludeTertiaryGroups(_nonLactoVegetarianGroups);
    }
}