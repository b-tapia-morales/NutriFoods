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

    private static IIncludableQueryable<Recipe, IEnumerable<RecipeSection>> IncludeSubfields(IQueryable<Recipe> recipes)
    {
        return recipes.Include(e => e.RecipeSections);
    }

    public Task<List<Recipe>> GetRecipes()
    {
        return _context.Recipes.ToListAsync();
    }

    public Task<List<Recipe>> GetVegetarianRecipes()
    {
        return _context.Recipes
            .ToListAsync();
    }

    public Task<Recipe> FindRecipe(string name)
    {
        return _context.Recipes.Where(e => string.Equals(e.Name, name, StringComparison.OrdinalIgnoreCase))
            .FirstAsync();
    }

    public Task<Recipe> FindRecipe(int id)
    {
        return _context.Recipes.Where(e => e.Id == id).FirstAsync();
    }
}