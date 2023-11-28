using API.Dto;
using Domain.Enum;

namespace API.Recipes;

public interface IRecipeRepository
{
    Task<List<RecipeDto>> FindAll();

    Task<List<RecipeDto>> FindWithPortions();

    Task<RecipeDto?> FindByName(string name);

    Task<RecipeDto?> FindById(int id);

    Task<List<RecipeDto>> FindByMealType(MealTypes mealType);

    Task<List<RecipeDto>> FindByDishType(DishTypes dishType);

    Task<List<RecipeDto>> GetVegetarianRecipes(Diets diet);

    Task<List<RecipeDto>> FilterByPreparationTime(int lowerBound, int upperBound);

    Task<List<RecipeDto>> FilterByPortions(int portions);

    Task<List<RecipeDto>> FilterByPortions(int lowerBound, int upperBound);

    Task<List<RecipeDto>> FilterByEnergy(int lowerBound, int upperBound);

    Task<List<RecipeDto>> FilterByCarbohydrates(int lowerBound, int upperBound);

    Task<List<RecipeDto>> FilterByFattyAcids(int lowerBound, int upperBound);

    Task<List<RecipeDto>> FilterByProteins(int lowerBound, int upperBound);

    Task<List<RecipeDto>> FilterByMacronutrientDistribution(double energy, double carbohydrates, double fattyAcids,
        double proteins);
}