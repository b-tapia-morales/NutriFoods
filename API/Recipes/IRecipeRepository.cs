using API.Dto;
using API.Dto.Insertion;
using Domain.Enum;

namespace API.Recipes;

public interface IRecipeRepository
{
    Task<RecipeDto?> FindByNameAndAuthor(string name, string author);
    Task<RecipeDto?> FindByUrl(string url);
    Task<RecipeDto?> FindById(int id);
    Task<List<RecipeDto>> FindAll(int pageNumber, int pageSize);
    Task<List<RecipeDto>> FindOrderedBy(Nutrients nutrient, int pageNumber, int pageSize, bool descending);
    Task<List<RecipeDto>> FindByMealType(MealTypes mealType, int pageNumber, int pageSize);
    Task<List<RecipeDto>> FindByDishType(DishTypes dishType, int pageNumber, int pageSize);
    Task<List<RecipeDto>> GetVegetarianRecipes(Diets diet, int pageNumber, int pageSize);
    Task<List<RecipeDto>> FindByPreparationTime(int lowerBound, int upperBound, int pageNumber, int pageSize);
    Task<List<RecipeDto>> FindByPortions(int portions, int pageNumber, int pageSize);
    Task<List<RecipeDto>> FindByPortions(int lowerBound, int upperBound, int pageNumber, int pageSize);
    Task<List<RecipeDto>> FindByEnergy(int lowerBound, int upperBound);
    Task<List<RecipeDto>> FindByCarbohydrates(int lowerBound, int upperBound);
    Task<List<RecipeDto>> FindByFattyAcids(int lowerBound, int upperBound);
    Task<List<RecipeDto>> FindByProteins(int lowerBound, int upperBound);
    Task<RecipeLogging> InsertRecipe(MinimalRecipe recipe);
    Task<List<RecipeLogging>> InsertRecipes(List<MinimalRecipe> minimalRecipes);
}