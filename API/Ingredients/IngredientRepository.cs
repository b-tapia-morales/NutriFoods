using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace API.Ingredients;

public class IngredientRepository : IIngredientRepository
{
    private readonly NutrifoodsDbContext _context;

    public IngredientRepository(NutrifoodsDbContext context)
    {
        _context = context;
    }

    private static IIncludableQueryable<Ingredient, PrimaryGroup> LoadLazily(IQueryable<Ingredient> ingredients)
    {
        return ingredients.Include(e => e.IngredientMeasures)
            .Include(e => e.TertiaryGroup)
            .ThenInclude(e => e.SecondaryGroup)
            .ThenInclude(e => e.PrimaryGroup);
    }

    public Task<Ingredient> FindByName(string name)
    {
        return LoadLazily(_context.Ingredients)
            .FirstAsync(e => e.Name.ToLower().Equals(name));
    }

    public Task<Ingredient> FindById(int id)
    {
        return LoadLazily(_context.Ingredients)
            .FirstAsync(e => e.Id == id);
    }

    public Task<List<Ingredient>> FindByPrimaryGroup(string name)
    {
        return LoadLazily(_context.Ingredients)
            .Where(e => e.TertiaryGroup.SecondaryGroup.PrimaryGroup.Name.ToLower().Equals(name))
            .ToListAsync();
    }

    public Task<List<Ingredient>> FindByPrimaryGroup(int id)
    {
        return LoadLazily(_context.Ingredients)
            .Where(e => e.TertiaryGroup.SecondaryGroup.PrimaryGroup.Id == id)
            .ToListAsync();
    }

    public Task<List<Ingredient>> FindBySecondaryGroup(string name)
    {
        return LoadLazily(_context.Ingredients)
            .Where(e => e.TertiaryGroup.SecondaryGroup.Name.ToLower().Equals(name))
            .ToListAsync();
    }

    public Task<List<Ingredient>> FindBySecondaryGroup(int id)
    {
        return LoadLazily(_context.Ingredients)
            .Where(e => e.TertiaryGroup.SecondaryGroup.Id == id)
            .ToListAsync();
    }

    public Task<List<Ingredient>> FindByTertiaryGroup(string name)
    {
        return LoadLazily(_context.Ingredients)
            .Where(e => e.TertiaryGroup.Name.ToLower().Equals(name))
            .ToListAsync();
    }

    public Task<List<Ingredient>> FindByTertiaryGroup(int id)
    {
        return LoadLazily(_context.Ingredients)
            .Where(e => e.TertiaryGroup.Id == id)
            .ToListAsync();
    }

    public Task<List<Ingredient>> FindAll()
    {
        return LoadLazily(_context.Ingredients)
            .ToListAsync();
    }
}