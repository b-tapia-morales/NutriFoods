using API.Dto;
using AutoMapper;
using Domain.Enum;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using static Domain.Models.NutrifoodsDbContext;

namespace API.Ingredients;

public class IngredientRepository : IIngredientRepository
{
    private readonly NutrifoodsDbContext _context;
    private readonly IMapper _mapper;

    public IngredientRepository(NutrifoodsDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<IngredientDto>> FindAll() =>
        await _mapper.ProjectTo<IngredientDto>(_context.Ingredients.IncludeFields()).ToListAsync();

    public async Task<IngredientDto?> FindByName(string name) =>
        await _mapper.ProjectTo<IngredientDto>(_context.Ingredients.IncludeFields())
            .FirstAsync(e => NormalizeStr(e.Name).Equals(NormalizeStr(name)));

    public async Task<IngredientDto?> FindById(int id) =>
        await _mapper.ProjectTo<IngredientDto>(_context.Ingredients.IncludeFields())
            .FirstAsync(e => e.Id == id);

    public async Task<List<IngredientDto>> FindByFoodGroup(FoodGroups group) =>
        await _mapper.ProjectTo<IngredientDto>(_context.Ingredients.IncludeFields()
            .Where(e => e.FoodGroup == group)
        ).ToListAsync();
}

public static class IngredientExtensions
{
    public static IQueryable<Ingredient> IncludeFields(this DbSet<Ingredient> ingredients)
    {
        return ingredients
            .AsQueryable()
            .Include(e => e.IngredientMeasures)
            .Include(e => e.NutritionalValues);
    }
}