using API.Dto;
using AutoMapper;
using Domain.Enum;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using static Domain.Enum.Diets;
using static Domain.Enum.Nutrients;
using static Domain.Models.NutrifoodsDbContext;

namespace API.Recipes;

public class RecipeRepository : IRecipeRepository
{
    private readonly NutrifoodsDbContext _context;
    private readonly IMapper _mapper;

    public RecipeRepository(NutrifoodsDbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<RecipeDto?> FindById(int id)
    {
        var recipe = await _context.Recipes.IncludeSubfields()
            .Where(e => e.Id == id)
            .FirstAsync();
        return _mapper.Map<RecipeDto>(recipe);
    }

    public async Task<RecipeDto?> FindByName(string name)
    {
        var recipe = await _context.Recipes.IncludeSubfields()
            .Where(e => NormalizeStr(e.Name).Equals(NormalizeStr(name)))
            .FirstAsync();
        return _mapper.Map<RecipeDto>(recipe);
    }

    public async Task<List<RecipeDto>> FindAll()
    {
        var recipes = await _context.Recipes.IncludeSubfields().ToListAsync();
        return _mapper.Map<List<RecipeDto>>(recipes);
    }

    public async Task<List<RecipeDto>> FindWithPortions()
    {
        var recipes = await _context.Recipes.IncludeSubfields()
            .Where(e => e.Portions != null && e.Portions > 0)
            .ToListAsync();
        return _mapper.Map<List<RecipeDto>>(recipes);
    }

    public async Task<List<RecipeDto>> FindByMealType(MealTypes mealType)
    {
        var recipes = await _context.Recipes.IncludeSubfields()
            .Where(e => e.Portions != null && e.Portions > 0 && e.MealTypes.Contains(mealType))
            .ToListAsync();
        return _mapper.Map<List<RecipeDto>>(recipes);
    }

    public async Task<List<RecipeDto>> FindByDishType(DishTypes dishType)
    {
        var recipes = await _context.Recipes.IncludeSubfields()
            .Where(e => e.Portions != null && e.Portions > 0 && e.DishTypes.Contains(dishType))
            .ToListAsync();
        return _mapper.Map<List<RecipeDto>>(recipes);
    }

    public async Task<List<RecipeDto>> GetVegetarianRecipes(Diets diet)
    {
        var inconsumableGroups = diet.InconsumableGroups;
        var recipes = await _context.Recipes.IncludeSubfields()
            .Where(e => e.RecipeMeasures.Select(x => x.IngredientMeasure.Ingredient)
                .All(x => !(diet == Vegan && x.IsAnimal) && !inconsumableGroups.Contains(x.FoodGroup)))
            .Where(e => e.RecipeQuantities.Select(x => x.Ingredient)
                .All(x => !(diet == Vegan && x.IsAnimal) && !inconsumableGroups.Contains(x.FoodGroup)))
            .ToListAsync();
        return _mapper.Map<List<RecipeDto>>(recipes);
    }

    public async Task<List<RecipeDto>> FilterByPreparationTime(int lowerBound, int upperBound)
    {
        var recipes = await _context.Recipes.IncludeSubfields()
            .Where(e => e.Time != null && e.Time >= lowerBound && e.Time <= upperBound)
            .ToListAsync();
        return _mapper.Map<List<RecipeDto>>(recipes);
    }

    public async Task<List<RecipeDto>> FilterByPortions(int portions)
    {
        var recipes = await _context.Recipes.IncludeSubfields()
            .Where(e => e.Portions != null && e.Portions == portions)
            .ToListAsync();
        return _mapper.Map<List<RecipeDto>>(recipes);
    }

    public async Task<List<RecipeDto>> FilterByPortions(int lowerBound, int upperBound)
    {
        var recipes = await _context.Recipes.IncludeSubfields()
            .Where(e => e.Portions != null && e.Portions >= lowerBound && e.Portions <= upperBound)
            .ToListAsync();
        return _mapper.Map<List<RecipeDto>>(recipes);
    }

    public Task<List<RecipeDto>> FilterByEnergy(int lowerBound, int upperBound) =>
        FilterByNutrientQuantity(Energy, lowerBound, upperBound);

    public Task<List<RecipeDto>> FilterByCarbohydrates(int lowerBound, int upperBound) =>
        FilterByNutrientQuantity(Carbohydrates, lowerBound, upperBound);

    public Task<List<RecipeDto>> FilterByFattyAcids(int lowerBound, int upperBound) =>
        FilterByNutrientQuantity(FattyAcids, lowerBound, upperBound);

    public Task<List<RecipeDto>> FilterByProteins(int lowerBound, int upperBound) =>
        FilterByNutrientQuantity(Proteins, lowerBound, upperBound);

    public async Task<List<RecipeDto>> FilterByMacronutrientDistribution(double energy, double carbohydrates,
        double fattyAcids, double proteins)
    {
        var recipes = await _context.Recipes.IncludeSubfields()
            .Where(e => e.NutritionalValues.Any(x => x.Nutrient == Energy && x.Quantity <= energy))
            .Where(e => e.NutritionalValues.Any(x => x.Nutrient == Carbohydrates && x.Quantity <= carbohydrates))
            .Where(e => e.NutritionalValues.Any(x => x.Nutrient == FattyAcids && x.Quantity <= fattyAcids))
            .Where(e => e.NutritionalValues.Any(x => x.Nutrient == Proteins && x.Quantity <= proteins))
            .ToListAsync();
        return _mapper.Map<List<RecipeDto>>(recipes);
    }

    private async Task<List<RecipeDto>> FilterByNutrientQuantity(Nutrients nutrient, int lowerBound, int upperBound)
    {
        var recipes = await _context.Recipes.IncludeSubfields()
            .Where(e => e.NutritionalValues.Any(x =>
                x.Nutrient == nutrient && x.Quantity >= lowerBound && x.Quantity <= upperBound))
            .ToListAsync();
        return _mapper.Map<List<RecipeDto>>(recipes);
    }
}

public static class RecipeExtensions
{
    public static IQueryable<Recipe> IncludeSubfields(this DbSet<Recipe> recipes) =>
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