using API.Dto;

namespace API.Recipes;

public class RecipeService : IRecipeService
{
    private readonly List<int> _nonVegetarianGroups = new() {10, 11};
    private readonly List<int> _nonOvoVegetarianGroups = new() {10, 11, 12, 13, 18, 19, 20};
    private readonly List<int> _nonLactoVegetarianGroups = new() {10, 11, 12, 13, 14};
    private readonly List<int> _nonOvoLactoVegetarianGroups = new() {10, 11, 12, 13};
    private readonly List<int> _nonPollotarianGroups = new() {10, 11, 12, 14};
    private readonly List<int> _nonPescetarianGroups = new() {12, 13, 14};
    private readonly List<int> _nonPolloPescetarianGroups = new() {12, 14};

    private readonly IRecipeRepository _repository;

    public RecipeService(IRecipeRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<RecipeDto>> FindAll()
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

    public async Task<List<RecipeDto>> GetVegetarianRecipes()
    {
        return await _repository.ExcludeSecondaryGroups(_nonVegetarianGroups);
    }

    public async Task<List<RecipeDto>> GetOvoVegetarianRecipes()
    {
        return await _repository.ExcludeTertiaryGroups(_nonOvoVegetarianGroups);
    }

    public async Task<List<RecipeDto>> GetOvoLactoVegetarianRecipes()
    {
        return await _repository.ExcludeTertiaryGroups(_nonOvoLactoVegetarianGroups);
    }

    public async Task<List<RecipeDto>> GetLactoVegetarianRecipes()
    {
        return await _repository.ExcludeTertiaryGroups(_nonLactoVegetarianGroups);
    }

    public async Task<List<RecipeDto>> GetPollotarianRecipes()
    {
        return await _repository.ExcludeTertiaryGroups(_nonPollotarianGroups);
    }

    public async Task<List<RecipeDto>> GetPescetarianRecipes()
    {
        return await _repository.ExcludeTertiaryGroups(_nonPescetarianGroups);
    }

    public async Task<List<RecipeDto>> GetPolloPescetarianRecipes()
    {
        return await _repository.ExcludeTertiaryGroups(_nonPolloPescetarianGroups);
    }

    public async Task<List<RecipeDto>> FilterByPreparationTime(int lowerBound, int upperBound)
    {
        return await _repository.FilterByPreparationTime(lowerBound, upperBound);
    }

    public async Task<List<RecipeDto>> FilterByPortions(int portions)
    {
        return await _repository.FilterByPortions(portions);
    }

    public async Task<List<RecipeDto>> FilterByPortions(int lowerBound, int upperBound)
    {
        return await _repository.FilterByPortions(lowerBound, upperBound);
    }

    public async Task<List<RecipeDto>> FilterByEnergy(int lowerBound, int upperBound)
    {
        return await _repository.FilterByNutrientQuantity(108, lowerBound, upperBound);
    }

    public async Task<List<RecipeDto>> FilterByCarbohydrates(int lowerBound, int upperBound)
    {
        return await _repository.FilterByNutrientQuantity(1, lowerBound, upperBound);
    }

    public async Task<List<RecipeDto>> FilterByLipids(int lowerBound, int upperBound)
    {
        return await _repository.FilterByNutrientQuantity(11, lowerBound, upperBound);
    }

    public async Task<List<RecipeDto>> FilterByProteins(int lowerBound, int upperBound)
    {
        return await _repository.FilterByNutrientQuantity(109, lowerBound, upperBound);
    }
}