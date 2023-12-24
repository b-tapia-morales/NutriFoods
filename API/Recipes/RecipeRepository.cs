// ReSharper disable UnusedAutoPropertyAccessor.Global

using System.Linq.Expressions;
using API.ApplicationData;
using API.Dto;
using API.Dto.Insertion;
using AutoMapper;
using Domain.Enum;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Utils.Enumerable;
using static Domain.Enum.Diets;
using static Domain.Enum.Nutrients;
using static Domain.Models.NutrifoodsDbContext;
using static NutrientRetrieval.NutrientCalculation.NutrientCalculation;

namespace API.Recipes;

public class RecipeRepository : IRecipeRepository
{
    private readonly IApplicationData _appData;
    private readonly NutrifoodsDbContext _context;
    private readonly IMapper _mapper;

    public RecipeRepository(NutrifoodsDbContext context, IMapper mapper, IApplicationData appData)
    {
        _appData = appData;
        _mapper = mapper;
        _context = context;
    }

    public async Task<RecipeDto?> FindById(int id) =>
        _mapper.Map<RecipeDto>(await _context.Recipes.FindFirstBy(e => e.Id == id));

    public async Task<RecipeDto?> FindByNameAndAuthor(string name, string author) =>
        _mapper.Map<RecipeDto>(await _context.Recipes
            .FindFirstBy(e => NormalizeStr(e.Name).Equals(NormalizeStr(name)) &&
                              NormalizeStr(e.Author).Equals(NormalizeStr(author)))
        );

    public async Task<RecipeDto?> FindByUrl(string url) =>
        _mapper.Map<RecipeDto>(await _context.Recipes.FindFirstBy(e => e.Url.ToLower().Equals(url.ToLower())));

    public async Task<List<RecipeDto>> FindAll(int pageNumber, int pageSize) =>
        _mapper.Map<List<RecipeDto>>(await _context.Recipes
            .FindAllBy(e => e.Id > ((pageNumber - 1) * pageSize) && e.Id <= (pageNumber * pageSize))
            .ToListAsync()
        );

    public async Task<List<RecipeDto>> FindOrderedBy(Nutrients nutrient, int pageNumber, int pageSize,
        bool descending) =>
        _mapper.Map<List<RecipeDto>>(await _context.Recipes
            .FindAllBy(e => e.NutritionalValues.Any(x => x.Nutrient == nutrient))
            .SortedBy(e => e.NutritionalValues.First(x => x.Nutrient == nutrient).Quantity, !descending)
            .Paginate(pageNumber, pageSize)
            .ToListAsync()
        );

    public async Task<List<RecipeDto>> FindByMealType(MealTypes mealType, int pageNumber, int pageSize)
    {
        return _mapper.Map<List<RecipeDto>>(await _context.Recipes
            .FindAllBy(e => e.MealTypes.Contains(mealType))
            .Paginate(pageNumber, pageSize)
            .ToListAsync()
        );
    }

    public async Task<List<RecipeDto>> FindByDishType(DishTypes dishType, int pageNumber, int pageSize) =>
        _mapper.Map<List<RecipeDto>>(await _context.Recipes
            .FindAllBy(e => e.DishTypes.Contains(dishType))
            .Paginate(pageNumber, pageSize)
            .ToListAsync()
        );

    public async Task<List<RecipeDto>> GetVegetarianRecipes(Diets diet, int pageNumber, int pageSize) =>
        _mapper.Map<List<RecipeDto>>(await _context.Recipes
            .FindAllBy(e => e.RecipeMeasures.Select(x => x.IngredientMeasure.Ingredient)
                .All(x => !(diet == Vegan && x.IsAnimal) && !diet.InconsumableGroups.Contains(x.FoodGroup)))
            .FindAllBy(e => e.RecipeQuantities.Select(x => x.Ingredient)
                .All(x => !(diet == Vegan && x.IsAnimal) && !diet.InconsumableGroups.Contains(x.FoodGroup)))
            .Paginate(pageNumber, pageSize)
            .ToListAsync()
        );

    public async Task<List<RecipeDto>> FindByPreparationTime(int lowerBound, int upperBound, int pageNumber,
        int pageSize) =>
        _mapper.Map<List<RecipeDto>>(await _context.Recipes
            .FindAllBy(e => e.Time != null && e.Time >= lowerBound && e.Time <= upperBound)
            .Paginate(pageNumber, pageSize)
            .ToListAsync()
        );

    public async Task<List<RecipeDto>> FindByPortions(int portions, int pageNumber, int pageSize) =>
        _mapper.Map<List<RecipeDto>>(await _context.Recipes
            .FindAllBy(e => e.Portions != null && e.Portions == portions)
            .Paginate(pageNumber, pageSize)
            .ToListAsync()
        );

    public async Task<List<RecipeDto>> FindByPortions(int lowerBound, int upperBound, int pageNumber, int pageSize) =>
        _mapper.Map<List<RecipeDto>>(await _context.Recipes
            .FindAllBy(e => e.Portions != null && e.Portions >= lowerBound && e.Portions <= upperBound)
            .Paginate(pageNumber, pageSize)
            .ToListAsync()
        );

    public Task<List<RecipeDto>> FindByEnergy(int lowerBound, int upperBound) =>
        FilterByNutrientQuantity(Energy, lowerBound, upperBound);

    public Task<List<RecipeDto>> FindByCarbohydrates(int lowerBound, int upperBound) =>
        FilterByNutrientQuantity(Carbohydrates, lowerBound, upperBound);

    public Task<List<RecipeDto>> FindByFattyAcids(int lowerBound, int upperBound) =>
        FilterByNutrientQuantity(FattyAcids, lowerBound, upperBound);

    public Task<List<RecipeDto>> FindByProteins(int lowerBound, int upperBound) =>
        FilterByNutrientQuantity(Proteins, lowerBound, upperBound);

    public async Task<RecipeLogging> InsertRecipe(MinimalRecipe recipe)
    {
        var logging = recipe.ProcessRecipe(_appData.IngredientDict, _appData.MeasureDict);
        if (!logging.IsSuccessful)
            return logging;

        var recipeId = await InsertIngredient(recipe);
        await InsertNutritionalValues(recipeId);

        return logging;
    }

    public async Task<List<RecipeLogging>> InsertRecipes(List<MinimalRecipe> minimalRecipes)
    {
        var tuples = minimalRecipes
            .Where(e => !Exists(e))
            .Select((e, i) => (Index: i, Log: e.ProcessRecipe(_appData.IngredientDict, _appData.MeasureDict)))
            .ToList();
        var filteredRecipes = tuples
            .Where(t => t.Log.IsSuccessful)
            .Select(t => minimalRecipes[t.Index])
            .ToList();
        await foreach (var recipeId in InsertIngredients(filteredRecipes))
            await InsertNutritionalValues(recipeId);

        return tuples.Select(e => e.Log).ToList();
    }

    private async Task<List<RecipeDto>> FilterByNutrientQuantity(Nutrients nutrient, int lowerBound, int upperBound)
    {
        var recipes = await _context.Recipes
            .FindAllBy(e => e.NutritionalValues.Any(x =>
                x.Nutrient == nutrient && x.Quantity >= lowerBound && x.Quantity <= upperBound))
            .ToListAsync();
        return _mapper.Map<List<RecipeDto>>(recipes);
    }

    private async Task<int> InsertIngredient(MinimalRecipe minimalRecipe)
    {
        var recipe = _mapper.Map<Recipe>(minimalRecipe);
        recipe.RecipeQuantities = [..minimalRecipe.ToQuantities(_appData.IngredientDict)];
        recipe.RecipeMeasures = [..minimalRecipe.ToMeasures(_appData.MeasureDict)];
        await _context.AddAsync(recipe);
        await _context.SaveChangesAsync();
        return recipe.Id;
    }

    private async IAsyncEnumerable<int> InsertIngredients(IEnumerable<MinimalRecipe> minimalRecipes)
    {
        foreach (var recipe in minimalRecipes)
            yield return await InsertIngredient(recipe);
    }

    private async Task InsertNutritionalValues(int recipeId)
    {
        var recipe = await _context.Recipes.IncludeSubfields().FirstAsync(e => e.Id == recipeId);
        recipe.NutritionalValues = [..ToNutritionalValues(recipe)];
        await _context.SaveChangesAsync();
    }

    private bool Exists(MinimalRecipe minimalRecipe) =>
        _context.Recipes.FirstOrDefault(e =>
            (NormalizeStr(e.Name).Equals(NormalizeStr(minimalRecipe.Name)) &&
             NormalizeStr(e.Author).Equals(NormalizeStr(minimalRecipe.Author))) ||
            e.Url.ToLower().Equals(minimalRecipe.Url.ToLower())) is not null;
}

public class RecipeLogging
{
    public string Name { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string Url { get; set; } = null!;
    public bool IsSuccessful { get; set; }
    public List<MeasureLogging> MeasureLogs { get; set; } = null!;
    public List<QuantityLogging> QuantityLogs { get; set; } = null!;
}

public class MeasureLogging
{
    public string MeasureName { get; set; } = null!;
    public string IngredientName { get; set; } = null!;
    public bool Exists { get; set; }
}

public class QuantityLogging
{
    public string IngredientName { get; set; } = null!;
    public bool Exists { get; set; }
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
            .ThenInclude(e => e.NutritionalValues)
            .Include(e => e.RecipeQuantities)
            .ThenInclude(e => e.Ingredient)
            .ThenInclude(e => e.NutritionalValues);

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