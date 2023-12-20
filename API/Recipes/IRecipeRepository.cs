using API.Dto;
using API.Dto.Insertion;
using Domain.Enum;

namespace API.Recipes;

public interface IRecipeRepository
{
    Task<List<RecipeDto>> FindAll(int pageNumber, int pageSize);

    Task<RecipeDto?> FindByNameAndAuthor(string name, string author);
    
    Task<RecipeDto?> FindByUrl(string url);

    Task<RecipeDto?> FindById(int id);

    Task<List<RecipeDto>> FindByMealType(MealTypes mealType, int pageNumber, int pageSize);

    Task<List<RecipeDto>> FindByDishType(DishTypes dishType, int pageNumber, int pageSize);

    Task<List<RecipeDto>> GetVegetarianRecipes(Diets diet, int pageNumber, int pageSize);

    Task<List<RecipeDto>> FilterByPreparationTime(int lowerBound, int upperBound, int pageNumber, int pageSize);

    Task<List<RecipeDto>> FilterByPortions(int portions, int pageNumber, int pageSize);

    Task<List<RecipeDto>> FilterByPortions(int lowerBound, int upperBound, int pageNumber, int pageSize);

    Task<List<RecipeDto>> FilterByEnergy(int lowerBound, int upperBound);

    Task<List<RecipeDto>> FilterByCarbohydrates(int lowerBound, int upperBound);

    Task<List<RecipeDto>> FilterByFattyAcids(int lowerBound, int upperBound);

    Task<List<RecipeDto>> FilterByProteins(int lowerBound, int upperBound);

    Task<List<RecipeDto>> FilterByMacronutrientDistribution(double energy, double carbohydrates, double fattyAcids,
        double proteins);

    Task<RecipeLogging> InsertRecipe(MinimalRecipe minimalRecipe);
    
    Task<List<RecipeLogging>> InsertRecipes(List<MinimalRecipe> minimalRecipe);
}