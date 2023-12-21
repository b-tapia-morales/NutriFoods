using System.Linq.Expressions;
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

    public Task<List<IngredientDto>> FindAll(int pageNumber, int pageSize) =>
        _mapper.ProjectTo<IngredientDto>(_context.Ingredients
            .FindAllBy(e => e.Id > ((pageNumber - 1) * pageSize) && e.Id <= (pageNumber * pageSize))
        ).ToListAsync();

    public Task<IngredientDto?> FindByName(string name) =>
        _mapper.ProjectTo<IngredientDto>(_context.Ingredients.IncludeSubfields())
            .FirstOrDefaultAsync(e => NormalizeStr(e.Name).Equals(NormalizeStr(name)));

    public Task<IngredientDto?> FindById(int id) =>
        _mapper.ProjectTo<IngredientDto>(_context.Ingredients.IncludeSubfields())
            .FirstOrDefaultAsync(e => e.Id == id);

    public Task<List<IngredientDto>> FindByFoodGroup(FoodGroups group, int pageNumber, int pageSize) =>
        _mapper.ProjectTo<IngredientDto>(
            _context.Ingredients.IncludeSubfields()
                .Where(e => e.FoodGroup == group)
                .Paginate(pageNumber, pageSize)
        ).ToListAsync();
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

    public static Task<Ingredient?> FindFirstBy(this DbSet<Ingredient> recipes,
        Expression<Func<Ingredient, bool>> predicate) =>
        recipes.IncludeSubfields().FirstOrDefaultAsync(predicate);

    public static IQueryable<Ingredient> FindAllBy(this DbSet<Ingredient> recipes,
        Expression<Func<Ingredient, bool>> predicate) =>
        recipes.IncludeSubfields().Where(predicate);

    public static IQueryable<Ingredient> FindAllBy(this IQueryable<Ingredient> recipes,
        Expression<Func<Ingredient, bool>> predicate) =>
        recipes.Where(predicate);

    public static IQueryable<Ingredient> Paginate(this IQueryable<Ingredient> recipes,
        int pageNumber, int pageSize) =>
        recipes.Skip((pageNumber - 1) * pageSize).Take(pageSize);
}