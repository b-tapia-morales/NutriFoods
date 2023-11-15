using API.Dto;
using AutoMapper;
using Domain.Enum;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using static Domain.Enum.Diets;
using static Domain.Enum.Nutrients;

namespace API.Recipes;

public class RecipeRepository(IMapper mapper) : IRecipeRepository
{
    public async Task<List<RecipeDto>> FindAll()
    {
        await using var context = new NutrifoodsDbContext();
        return await mapper.ProjectTo<RecipeDto>(IncludeSubfields(context.Recipes)).ToListAsync();
    }

    public async Task<List<RecipeDto>> FindWithPortions()
    {
        await using var context = new NutrifoodsDbContext();
        return await mapper.ProjectTo<RecipeDto>(
            IncludeSubfields(context.Recipes)
                .Where(e => e.Portions != null && e.Portions > 0)
        ).ToListAsync();
    }

    public async Task<List<RecipeDto>> FindByMealType(MealTypes mealType)
    {
        await using var context = new NutrifoodsDbContext();
        return await mapper.ProjectTo<RecipeDto>(
            IncludeSubfields(context.Recipes)
                .Where(e => e.Portions != null && e.Portions > 0 && e.MealTypes.Contains(mealType))
        ).ToListAsync();
    }

    public async Task<List<RecipeDto>> FindByDishType(DishTypes dishType)
    {
        await using var context = new NutrifoodsDbContext();
        return await mapper.ProjectTo<RecipeDto>(
            IncludeSubfields(context.Recipes)
                .Where(e => e.Portions != null && e.Portions > 0 && e.DishTypes.Contains(dishType))
        ).ToListAsync();
    }

    public async Task<RecipeDto?> FindByName(string name)
    {
        await using var context = new NutrifoodsDbContext();
        return await mapper.ProjectTo<RecipeDto>(
            IncludeSubfields(context.Recipes)
                .Where(e => e.Name.Equals(name))
        ).FirstAsync();
    }

    public async Task<RecipeDto?> FindById(int id)
    {
        await using var context = new NutrifoodsDbContext();
        return await mapper.ProjectTo<RecipeDto>(
            IncludeSubfields(context.Recipes)
                .Where(e => e.Id == id)
        ).FirstAsync();
    }

    public async Task<List<RecipeDto>> GetVegetarianRecipes(Diets diet)
    {
        var inconsumableGroups = diet.InconsumableGroups;
        await using var context = new NutrifoodsDbContext();
        return await mapper.ProjectTo<RecipeDto>(
            IncludeSubfields(context.Recipes)
                .Where(e => e.RecipeMeasures.Select(x => x.IngredientMeasure.Ingredient)
                    .All(x => !(diet == Vegan && x.IsAnimal) && !inconsumableGroups.Contains(x.FoodGroup)))
                .Where(e => e.RecipeQuantities.Select(x => x.Ingredient)
                    .All(x => !(diet == Vegan && x.IsAnimal) && !inconsumableGroups.Contains(x.FoodGroup)))
        ).ToListAsync();
    }

    public async Task<List<RecipeDto>> FilterByPreparationTime(int lowerBound, int upperBound)
    {
        await using var context = new NutrifoodsDbContext();
        return await mapper.ProjectTo<RecipeDto>(
            IncludeSubfields(context.Recipes)
                .Where(e => e.Time != null && e.Time >= lowerBound &&
                            e.Time <= upperBound)
        ).ToListAsync();
    }

    public async Task<List<RecipeDto>> FilterByPortions(int portions)
    {
        await using var context = new NutrifoodsDbContext();
        return await mapper.ProjectTo<RecipeDto>(
            IncludeSubfields(context.Recipes)
                .Where(e => e.Portions != null && e.Portions == portions)
        ).ToListAsync();
    }

    public async Task<List<RecipeDto>> FilterByPortions(int lowerBound, int upperBound)
    {
        await using var context = new NutrifoodsDbContext();
        return await mapper.ProjectTo<RecipeDto>(
            IncludeSubfields(context.Recipes)
                .Where(e => e.Portions != null && e.Portions >= lowerBound && e.Portions <= upperBound)
        ).ToListAsync();
    }

    public async Task<List<RecipeDto>> FilterByEnergy(int lowerBound, int upperBound) =>
        await FilterByNutrientQuantity(Energy, lowerBound, upperBound);

    public async Task<List<RecipeDto>> FilterByCarbohydrates(int lowerBound, int upperBound) =>
        await FilterByNutrientQuantity(Carbohydrates, lowerBound, upperBound);

    public async Task<List<RecipeDto>> FilterByFattyAcids(int lowerBound, int upperBound) =>
        await FilterByNutrientQuantity(FattyAcids, lowerBound, upperBound);

    public async Task<List<RecipeDto>> FilterByProteins(int lowerBound, int upperBound) =>
        await FilterByNutrientQuantity(Proteins, lowerBound, upperBound);

    public async Task<List<RecipeDto>> FilterByMacronutrientDistribution(double energy, double carbohydrates,
        double fattyAcids, double proteins)
    {
        await using var context = new NutrifoodsDbContext();
        return await mapper.ProjectTo<RecipeDto>(
            IncludeSubfields(context.Recipes)
                .Where(e => e.RecipeNutrients.Any(x => x.Nutrient == Energy && x.Quantity <= energy))
                .Where(e => e.RecipeNutrients.Any(x => x.Nutrient == Carbohydrates && x.Quantity <= carbohydrates))
                .Where(e => e.RecipeNutrients.Any(x => x.Nutrient == FattyAcids && x.Quantity <= fattyAcids))
                .Where(e => e.RecipeNutrients.Any(x => x.Nutrient == Proteins && x.Quantity <= proteins))
        ).ToListAsync();
    }

    private async Task<List<RecipeDto>> FilterByNutrientQuantity(Nutrients nutrient, int lowerBound, int upperBound)
    {
        await using var context = new NutrifoodsDbContext();
        return await mapper.ProjectTo<RecipeDto>(
            IncludeSubfields(context.Recipes)
                .Where(e => e.RecipeNutrients.Any(x =>
                    x.Nutrient == nutrient && x.Quantity >= lowerBound && x.Quantity <= upperBound))
        ).ToListAsync();
    }

    private static IQueryable<Recipe> IncludeSubfields(IQueryable<Recipe> recipes) =>
        recipes
            .Include(e => e.RecipeSteps)
            .Include(e => e.RecipeNutrients)
            .Include(e => e.RecipeMeasures)
            .ThenInclude(e => e.IngredientMeasure)
            .ThenInclude(e => e.Ingredient)
            .Include(e => e.RecipeQuantities)
            .ThenInclude(e => e.Ingredient)
            .AsNoTracking();
}