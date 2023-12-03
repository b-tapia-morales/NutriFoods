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
    public async Task<RecipeDto?> FindById(int id)
    {
        await using var context = new NutrifoodsDbContext();
        var recipe = await context.Recipes.AsQueryable().IncludeSubfields()
            .Where(e => e.Id == id)
            .FirstAsync();
        return mapper.Map<RecipeDto>(recipe);
    }

    public async Task<RecipeDto?> FindByName(string name)
    {
        await using var context = new NutrifoodsDbContext();
        var recipe = await context.Recipes.IncludeSubfields()
            .Where(e => NutrifoodsDbContext.NormalizeStr(e.Name).Equals(NutrifoodsDbContext.NormalizeStr(name)))
            .FirstAsync();
        return mapper.Map<RecipeDto>(recipe);
    }

    public async Task<List<RecipeDto>> FindAll()
    {
        await using var context = new NutrifoodsDbContext();
        var recipes = await context.Recipes.AsQueryable().IncludeSubfields()
            .ToListAsync();
        return mapper.Map<List<RecipeDto>>(recipes);
    }

    public async Task<List<RecipeDto>> FindWithPortions()
    {
        await using var context = new NutrifoodsDbContext();
        var recipes = await context.Recipes.AsQueryable().IncludeSubfields()
            .Where(e => e.Portions != null && e.Portions > 0)
            .ToListAsync();
        return mapper.Map<List<RecipeDto>>(recipes);
    }

    public async Task<List<RecipeDto>> FindByMealType(MealTypes mealType)
    {
        await using var context = new NutrifoodsDbContext();
        var recipes = await context.Recipes.AsQueryable().IncludeSubfields()
            .Where(e => e.Portions != null && e.Portions > 0 && e.MealTypes.Contains(mealType))
            .ToListAsync();
        return mapper.Map<List<RecipeDto>>(recipes);
    }

    public async Task<List<RecipeDto>> FindByDishType(DishTypes dishType)
    {
        await using var context = new NutrifoodsDbContext();
        var recipes = await context.Recipes.AsQueryable().IncludeSubfields()
            .Where(e => e.Portions != null && e.Portions > 0 && e.DishTypes.Contains(dishType))
            .ToListAsync();
        return mapper.Map<List<RecipeDto>>(recipes);
    }

    public async Task<List<RecipeDto>> GetVegetarianRecipes(Diets diet)
    {
        var inconsumableGroups = diet.InconsumableGroups;
        await using var context = new NutrifoodsDbContext();
        var recipes = await context.Recipes.AsQueryable().IncludeSubfields()
            .Where(e => e.RecipeMeasures.Select(x => x.IngredientMeasure.Ingredient)
                .All(x => !(diet == Vegan && x.IsAnimal) && !inconsumableGroups.Contains(x.FoodGroup)))
            .Where(e => e.RecipeQuantities.Select(x => x.Ingredient)
                .All(x => !(diet == Vegan && x.IsAnimal) && !inconsumableGroups.Contains(x.FoodGroup)))
            .ToListAsync();
        return mapper.Map<List<RecipeDto>>(recipes);
    }

    public async Task<List<RecipeDto>> FilterByPreparationTime(int lowerBound, int upperBound)
    {
        await using var context = new NutrifoodsDbContext();
        var recipes = await context.Recipes.AsQueryable().IncludeSubfields()
            .Where(e => e.Time != null && e.Time >= lowerBound && e.Time <= upperBound)
            .ToListAsync();
        return mapper.Map<List<RecipeDto>>(recipes);
    }

    public async Task<List<RecipeDto>> FilterByPortions(int portions)
    {
        await using var context = new NutrifoodsDbContext();
        var recipes = await context.Recipes.AsQueryable().IncludeSubfields()
            .Where(e => e.Portions != null && e.Portions == portions)
            .ToListAsync();
        return mapper.Map<List<RecipeDto>>(recipes);
    }

    public async Task<List<RecipeDto>> FilterByPortions(int lowerBound, int upperBound)
    {
        await using var context = new NutrifoodsDbContext();
        var recipes = await context.Recipes.AsQueryable().IncludeSubfields()
            .Where(e => e.Portions != null && e.Portions >= lowerBound && e.Portions <= upperBound)
            .ToListAsync();
        return mapper.Map<List<RecipeDto>>(recipes);
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
        var recipes = await context.Recipes.AsQueryable().IncludeSubfields()
            .Where(e => e.NutritionalValues.Any(x => x.Nutrient == Energy && x.Quantity <= energy))
            .Where(e => e.NutritionalValues.Any(x => x.Nutrient == Carbohydrates && x.Quantity <= carbohydrates))
            .Where(e => e.NutritionalValues.Any(x => x.Nutrient == FattyAcids && x.Quantity <= fattyAcids))
            .Where(e => e.NutritionalValues.Any(x => x.Nutrient == Proteins && x.Quantity <= proteins))
            .ToListAsync();
        return mapper.Map<List<RecipeDto>>(recipes);
    }

    private async Task<List<RecipeDto>> FilterByNutrientQuantity(Nutrients nutrient, int lowerBound, int upperBound)
    {
        await using var context = new NutrifoodsDbContext();
        var recipes = await context.Recipes.AsQueryable().IncludeSubfields()
            .Where(e => e.NutritionalValues.Any(x =>
                x.Nutrient == nutrient && x.Quantity >= lowerBound && x.Quantity <= upperBound))
            .ToListAsync();
        return mapper.Map<List<RecipeDto>>(recipes);
    }
}

public static class RecipeExtensions
{
    public static IQueryable<Recipe> IncludeSubfields(this IQueryable<Recipe> recipes) =>
        recipes
            .AsQueryable()
            .Include(e => e.NutritionalValues)
            .Include(e => e.RecipeSteps)
            .Include(e => e.RecipeMeasures)
            .ThenInclude(e => e.IngredientMeasure)
            .ThenInclude(e => e.Ingredient)
            .Include(e => e.RecipeQuantities)
            .ThenInclude(e => e.Ingredient);
}