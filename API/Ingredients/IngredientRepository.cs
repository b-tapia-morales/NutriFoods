using API.Dto;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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

    public Task<List<IngredientDto>> FindAll() =>
        _mapper.ProjectTo<IngredientDto>(_context.Ingredients.IncludeSubfields())
            .ToListAsync();

    public Task<IngredientDto?> FindByName(string name) =>
        _mapper.ProjectTo<IngredientDto>(_context.Ingredients.IncludeSubfields())
            .FirstOrDefaultAsync(e => NormalizeStr(e.Name).Equals(NormalizeStr(name)));

    public Task<IngredientDto?> FindById(int id) =>
        _mapper.ProjectTo<IngredientDto>(_context.Ingredients.IncludeSubfields())
            .FirstOrDefaultAsync(e => e.Id == id);

    public Task<List<IngredientDto>> FindByFoodGroup(FoodGroups group) =>
        _mapper.ProjectTo<IngredientDto>(_context.Ingredients.IncludeSubfields().Where(e => e.FoodGroup == group))
            .ToListAsync();
}

public static class IngredientExtensions
{
    public static IQueryable<Ingredient> IncludeSubfields(this DbSet<Ingredient> ingredients)
    {
        return ingredients
            .AsQueryable()
            .Include(e => e.IngredientMeasures)
            .Include(e => e.NutritionalValues);
    }
}