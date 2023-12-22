using System.Linq.Expressions;
using API.Dto;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Enum;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Utils.Enumerable;
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

    public async Task<List<IngredientDto>> FindAll(int pageNumber, int pageSize) =>
        _mapper.Map<List<IngredientDto>>(await _context.Ingredients
            .FindAllBy(e => e.Id > ((pageNumber - 1) * pageSize) && e.Id <= (pageNumber * pageSize))
            .ToListAsync()
        );

    public async Task<List<IngredientDto>> FindOrderedBy(Nutrients nutrient, int pageNumber, int pageSize,
        bool descending) =>
        _mapper.Map<List<IngredientDto>>(await _context.Ingredients
            .FindAllBy(e => e.NutritionalValues.Count > 0 && e.NutritionalValues.Any(x => x.Nutrient == nutrient))
            .SortedBy(e => e.NutritionalValues.First(x => x.Nutrient == nutrient).Quantity, !descending)
            .Paginate(pageNumber, pageSize)
            .ToListAsync()
        );

    public async Task<IngredientDto?> FindByName(string name) =>
        _mapper.Map<IngredientDto>(await _context.Ingredients.IncludeSubfields()
            .FirstOrDefaultAsync(e => NormalizeStr(e.Name).Equals(NormalizeStr(name)))
        );

    public async Task<IngredientDto?> FindById(int id) =>
        _mapper.Map<IngredientDto>(await _context.Ingredients.IncludeSubfields()
            .FirstOrDefaultAsync(e => e.Id == id)
        );

    public async Task<List<IngredientDto>> FindByFoodGroup(FoodGroups group, int pageNumber, int pageSize) =>
        _mapper.Map<List<IngredientDto>>(await _context.Ingredients.IncludeSubfields()
            .Where(e => e.FoodGroup == group)
            .Paginate(pageNumber, pageSize)
            .ToListAsync()
        );
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

    public static Task<Ingredient?> FindFirstBy(this DbSet<Ingredient> ingredients,
        Expression<Func<Ingredient, bool>> predicate) =>
        ingredients.IncludeSubfields().FirstOrDefaultAsync(predicate);

    public static IQueryable<Ingredient> FindAllBy(this DbSet<Ingredient> ingredients,
        Expression<Func<Ingredient, bool>> predicate) =>
        ingredients.IncludeSubfields().Where(predicate);

    public static IQueryable<Ingredient> FindAllBy(this IQueryable<Ingredient> ingredients,
        Expression<Func<Ingredient, bool>> predicate) =>
        ingredients.Where(predicate);

    public static IQueryable<Ingredient> Paginate(this IQueryable<Ingredient> ingredients,
        int pageNumber, int pageSize) =>
        ingredients.Skip((pageNumber - 1) * pageSize).Take(pageSize);
}