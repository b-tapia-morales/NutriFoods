using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Recipes;

public class RecipeRepository : IRecipeRepository
{
    private readonly NutrifoodsDbContext _context;

    public RecipeRepository(NutrifoodsDbContext context)
    {
        _context = context;
    }

    public Task<List<Recipe>> GetRecipes()
    {
        return _context.Recipes.ToListAsync();
    }

    public Task<List<Recipe>> GetVegetarianRecipes()
    {
        return _context.Recipes
            .Where(r => r.RecipeSection.RecipeQuantities.Any(e => !e.Ingredient.IsAnimal) ||
                        r.RecipeSection.RecipeMeasures.Any(e => !e.IngredientMeasure.Ingredient.IsAnimal))
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