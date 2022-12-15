using API.Dto;
using Utils.Enum;

namespace API.Recipes;

public interface IRecipeRepository
{
    Task<List<RecipeDto>> FindAll();

    Task<List<RecipeDto>> FindWithPortions();

    Task<List<RecipeDto>> FindExcludeById(IList<int> ids);

    Task<RecipeDto> FindByName(string name);

    Task<RecipeDto> FindById(int id);

    Task<List<RecipeDto>> FindByMealType(MealType mealType);

    Task<List<RecipeDto>> FindByDishType(DishType dishType);

    Task<List<RecipeDto>> GetVegetarianRecipes();

    Task<List<RecipeDto>> GetOvoVegetarianRecipes();

    Task<List<RecipeDto>> GetOvoLactoVegetarianRecipes();

    Task<List<RecipeDto>> GetLactoVegetarianRecipes();

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

    Task<List<RecipeDto>> FilterByMacronutrientDistribution(double energyLimit, double carbohydratesLimit, double lipidsLimit,
        double proteinsLimit);
}