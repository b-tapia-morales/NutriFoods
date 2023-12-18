using System.Linq.Expressions;
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
        var recipe = await _context.Recipes.FindFirstBy(e => e.Id == id);
        return _mapper.Map<RecipeDto>(recipe);
    }

    public async Task<RecipeDto?> FindByNameAndAuthor(string name, string author)
    {
        var recipe = await _context.Recipes.FindFirstBy(e => NormalizeStr(e.Name).Equals(NormalizeStr(name)) &&
                                                             NormalizeStr(e.Author).Equals(NormalizeStr(author)));
        return _mapper.Map<RecipeDto>(recipe);
    }

    public async Task<List<RecipeDto>> FindAll(int pageNumber, int pageSize)
    {
        var recipes = await _context.Recipes
            .FindAllBy(e => e.Id > ((pageNumber - 1) * pageSize) && e.Id <= (pageNumber * pageSize))
            .ToListAsync();
        return _mapper.Map<List<RecipeDto>>(recipes);
    }

    public async Task<List<RecipeDto>> FindByMealType(MealTypes mealType, int pageNumber, int pageSize)
    {
        var recipes = await _context.Recipes
            .FindAllBy(e => e.MealTypes.Contains(mealType))
            .Paginate(pageNumber, pageSize)
            .ToListAsync();
        return _mapper.Map<List<RecipeDto>>(recipes);
    }

    public async Task<List<RecipeDto>> FindByDishType(DishTypes dishType, int pageNumber, int pageSize)
    {
        var recipes = await _context.Recipes
            .FindAllBy(e => e.DishTypes.Contains(dishType))
            .Paginate(pageNumber, pageSize)
            .ToListAsync();
        return _mapper.Map<List<RecipeDto>>(recipes);
    }

    public async Task<List<RecipeDto>> GetVegetarianRecipes(Diets diet, int pageNumber, int pageSize)
    {
        var inconsumableGroups = diet.InconsumableGroups;
        var recipes = await _context.Recipes
            .FindAllBy(e => e.RecipeMeasures.Select(x => x.IngredientMeasure.Ingredient)
                .All(x => !(diet == Vegan && x.IsAnimal) && !inconsumableGroups.Contains(x.FoodGroup)))
            .FindAllBy(e => e.RecipeQuantities.Select(x => x.Ingredient)
                .All(x => !(diet == Vegan && x.IsAnimal) && !inconsumableGroups.Contains(x.FoodGroup)))
            .Paginate(pageNumber, pageSize)
            .ToListAsync();
        return _mapper.Map<List<RecipeDto>>(recipes);
    }

    public async Task<List<RecipeDto>> FilterByPreparationTime(int lowerBound, int upperBound, int pageNumber,
        int pageSize)
    {
        var recipes = await _context.Recipes
            .FindAllBy(e => e.Time != null && e.Time >= lowerBound && e.Time <= upperBound)
            .Paginate(pageNumber, pageSize)
            .ToListAsync();
        return _mapper.Map<List<RecipeDto>>(recipes);
    }

    public async Task<List<RecipeDto>> FilterByPortions(int portions, int pageNumber, int pageSize)
    {
        var recipes = await _context.Recipes
            .FindAllBy(e => e.Portions != null && e.Portions == portions)
            .Paginate(pageNumber, pageSize)
            .ToListAsync();
        return _mapper.Map<List<RecipeDto>>(recipes);
    }

    public async Task<List<RecipeDto>> FilterByPortions(int lowerBound, int upperBound, int pageNumber, int pageSize)
    {
        var recipes = await _context.Recipes
            .FindAllBy(e => e.Portions != null && e.Portions >= lowerBound && e.Portions <= upperBound)
            .Paginate(pageNumber, pageSize)
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
        var recipes = await _context.Recipes
            .FindAllBy(e => e.NutritionalValues.Any(x => x.Nutrient == Energy && x.Quantity <= energy))
            .FindAllBy(e => e.NutritionalValues.Any(x => x.Nutrient == Carbohydrates && x.Quantity <= carbohydrates))
            .FindAllBy(e => e.NutritionalValues.Any(x => x.Nutrient == FattyAcids && x.Quantity <= fattyAcids))
            .FindAllBy(e => e.NutritionalValues.Any(x => x.Nutrient == Proteins && x.Quantity <= proteins))
            .ToListAsync();
        return _mapper.Map<List<RecipeDto>>(recipes);
    }

    private async Task<List<RecipeDto>> FilterByNutrientQuantity(Nutrients nutrient, int lowerBound, int upperBound)
    {
        var recipes = await _context.Recipes
            .FindAllBy(e => e.NutritionalValues.Any(x =>
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

    public static Task<Recipe?> FindFirstBy(this DbSet<Recipe> recipes, Expression<Func<Recipe, bool>> predicate) =>
        recipes.IncludeSubfields().FirstOrDefaultAsync(predicate);

    public static IQueryable<Recipe> FindAllBy(this DbSet<Recipe> recipes, Expression<Func<Recipe, bool>> predicate) =>
        recipes.IncludeSubfields().Where(predicate);

    public static IQueryable<Recipe> FindAllBy(this IQueryable<Recipe> recipes,
        Expression<Func<Recipe, bool>> predicate) =>
        recipes.Where(predicate);

    public static IQueryable<Recipe> Paginate(this IQueryable<Recipe> recipes,
        int pageNumber, int pageSize) =>
        recipes.Skip((pageNumber - 1) * pageSize).Take(pageSize);
}