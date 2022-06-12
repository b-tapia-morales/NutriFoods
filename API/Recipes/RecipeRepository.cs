using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace API.Recipes;

public class RecipeRepository : IRecipeRepository
{
    private readonly NutrifoodsDbContext _context;

    public RecipeRepository(NutrifoodsDbContext context)
    {
        _context = context;
    }

    private static IIncludableQueryable<Recipe, PrimaryGroup> IncludeSubfields(IQueryable<Recipe> recipes)
    {
        return recipes
            .Include(e => e.RecipeSteps)
            .Include(e => e.RecipeNutrients)
            .ThenInclude(e => e.Nutrient)
            .ThenInclude(e => e.Subtype)
            .ThenInclude(e => e.Type)
            .Include(e => e.RecipeMeasures)
            .ThenInclude(e => e.IngredientMeasure)
            .ThenInclude(e => e.Ingredient)
            .ThenInclude(e => e.TertiaryGroup)
            .ThenInclude(e => e.SecondaryGroup)
            .ThenInclude(e => e.PrimaryGroup)
            .Include(e => e.RecipeQuantities)
            .ThenInclude(e => e.Ingredient)
            .ThenInclude(e => e.TertiaryGroup)
            .ThenInclude(e => e.SecondaryGroup)
            .ThenInclude(e => e.PrimaryGroup);
    }

    public async Task<List<Recipe>> GetRecipes()
    {
        return await IncludeSubfields(_context.Recipes).ToListAsync();
    }

    public async Task<Recipe> FindRecipe(string name)
    {
        return await IncludeSubfields(_context.Recipes)
            .Where(e => e.Name.Equals(name))
            .FirstAsync();
    }

    public async Task<Recipe> FindRecipe(int id)
    {
        return await _context.Recipes.Where(e => e.Id == id).FirstAsync();
    }

    public async Task<List<Recipe>> ExcludeSecondaryGroups(IEnumerable<int> ids)
    {
        var findMeasures = IncludeSubfields(_context.Recipes)
            .Where(e => !e.RecipeMeasures.Any(m =>
                ids.Contains(m.IngredientMeasure.Ingredient.TertiaryGroup.SecondaryGroup.Id)))
            .ToListAsync();
        var findQuantities = IncludeSubfields(_context.Recipes).Where(e => !e.RecipeQuantities.Any(m =>
                ids.Contains(m.Ingredient.TertiaryGroup.SecondaryGroup.Id)))
            .ToListAsync();
        await Task.WhenAll(findMeasures, findQuantities);
        return findMeasures.Result.Concat(findQuantities.Result).ToList();
    }

    public async Task<List<Recipe>> ExcludeTertiaryGroups(IEnumerable<int> ids)
    {
        var findMeasures = IncludeSubfields(_context.Recipes)
            .Where(e => !e.RecipeMeasures.Any(m => ids.Contains(m.IngredientMeasure.Ingredient.TertiaryGroup.Id)))
            .ToListAsync();
        var findQuantities = IncludeSubfields(_context.Recipes)
            .Where(e => !e.RecipeQuantities.Any(m => ids.Contains(m.Ingredient.TertiaryGroup.Id)))
            .ToListAsync();
        await Task.WhenAll(findMeasures, findQuantities);
        return findMeasures.Result.Concat(findQuantities.Result).ToList();
    }
}