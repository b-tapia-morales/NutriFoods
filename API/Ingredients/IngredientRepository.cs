using API.Dto;
using AutoMapper;
using Domain.Enum;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

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
        await _mapper.ProjectTo<IngredientDto>(IncludeFields(_context.Ingredients))
            .ToListAsync();

    public async Task<IngredientDto?> FindByName(string name) =>
        await _mapper.ProjectTo<IngredientDto>(IncludeFields(_context.Ingredients))
            .FirstAsync(e => e.Name.ToLower().Equals(name));

    public async Task<IngredientDto?> FindById(int id) =>
        await _mapper.ProjectTo<IngredientDto>(IncludeFields(_context.Ingredients))
            .FirstAsync(e => e.Id == id);

    public async Task<List<IngredientDto>> FindByFoodGroup(FoodGroups group) =>
        await _mapper.ProjectTo<IngredientDto>(IncludeFields(_context.Ingredients)
            .Where(e => e.FoodGroup == group)
        ).ToListAsync();

    private static IQueryable<Ingredient> IncludeFields(IQueryable<Ingredient> ingredients)
    {
        return ingredients
            .Include(e => e.IngredientMeasures)
            .Include(e => e.IngredientNutrients)
            .AsNoTracking();
    }
}