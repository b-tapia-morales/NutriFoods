using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Ingredients;

public class IngredientRepository : IIngredientRepository
{
    private readonly NutrifoodsDbContext _context;

    public IngredientRepository(NutrifoodsDbContext context)
    {
        _context = context;
    }

    public Task<Ingredient> FindByName(string name)
    {
        return _context.Ingredients
            .Include(e => e.TertiaryGroup)
            .Include(e => e.Measures)
            .FirstAsync(e => e.Name.ToLower().Equals(name));
    }

    public Task<Ingredient> FindById(int id)
    {
        return _context.Ingredients
            .Include(e => e.Measures)
            .Include(e => e.TertiaryGroup)
            .FirstAsync(e => e.Id == id);
    }

    public Task<List<Ingredient>> FindByPrimaryGroup(string name)
    {
        return _context
            .Ingredients
            .Where(e => e.TertiaryGroup.SecondaryGroup.PrimaryGroup.Name.ToLower().Equals(name))
            .Include(e => e.Measures)
            .Include(e => e.TertiaryGroup)
            .ToListAsync();
    }

    public Task<List<Ingredient>> FindByPrimaryGroup(int id)
    {
        return _context
            .Ingredients
            .Where(e => e.TertiaryGroup.SecondaryGroup.PrimaryGroup.Id == id)
            .Include(e => e.TertiaryGroup)
            .Include(e => e.Measures)
            .ToListAsync();
    }

    public Task<List<Ingredient>> FindBySecondaryGroup(string name)
    {
        return _context
            .Ingredients
            .Where(e => e.TertiaryGroup.SecondaryGroup.Name.ToLower().Equals(name))
            .Include(e => e.TertiaryGroup)
            .Include(e => e.Measures)
            .ToListAsync();
    }

    public Task<List<Ingredient>> FindBySecondaryGroup(int id)
    {
        return _context
            .Ingredients
            .Where(e => e.TertiaryGroup.SecondaryGroup.Id == id)
            .Include(e => e.TertiaryGroup)
            .Include(e => e.Measures)
            .ToListAsync();
    }

    public Task<List<Ingredient>> FindByTertiaryGroup(string name)
    {
        return _context
            .Ingredients
            .Where(e => e.TertiaryGroup.Name.ToLower().Equals(name))
            .Include(e => e.TertiaryGroup)
            .Include(e => e.Measures)
            .ToListAsync();
    }

    public Task<List<Ingredient>> FindByTertiaryGroup(int id)
    {
        return _context
            .Ingredients
            .Where(e => e.TertiaryGroup.Id == id)
            .Include(e => e.TertiaryGroup)
            .Include(e => e.Measures)
            .ToListAsync();
    }

    public Task<List<Ingredient>> FindAll()
    {
        return _context.Ingredients
            .Include(e => e.TertiaryGroup)
            .Include(e => e.Measures)
            .ToListAsync();
    }
}