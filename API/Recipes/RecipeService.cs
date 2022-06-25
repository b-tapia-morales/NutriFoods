using API.Dto;
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

    public async Task<IEnumerable<RecipeDto>> FindAll()
    {
        return await _repository.FindAll();
    }

    public async Task<RecipeDto> FindByName(string name)
    {
        return await _repository.FindByName(name.Trim().ToLower());
    }

    public async Task<RecipeDto> FindById(int id)
    {
        return await _repository.FindById(id);
    }
    
    public async Task<IReadOnlyCollection<RecipeDto>> GetVegetarianRecipes()
    {
        return await _repository.ExcludeSecondaryGroups(_nonVegetarianGroups);
    }

    public async Task<IReadOnlyCollection<RecipeDto>> GetOvoVegetarianRecipes()
    {
        return await _repository.ExcludeTertiaryGroups(_nonOvoVegetarianGroups);
    }

    public async Task<IReadOnlyCollection<RecipeDto>> GetOvoLactoVegetarianRecipes()
    {
        return await _repository.ExcludeTertiaryGroups(_nonOvoLactoVegetarianGroups);
    }

    public async Task<IReadOnlyCollection<RecipeDto>> GetLactoVegetarianRecipes()
    {
        return await _repository.ExcludeTertiaryGroups(_nonLactoVegetarianGroups);
    }
}