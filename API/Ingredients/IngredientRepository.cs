using API.Dto;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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

    private static IQueryable<Ingredient> FullLazyLoad(IQueryable<Ingredient> ingredients)
    {
        return ingredients
            .Include(e => e.IngredientNutrients)
            .ThenInclude(e => e.Nutrient)
            .ThenInclude(e => e.Subtype)
            .ThenInclude(e => e.Type)
            .Include(e => e.IngredientMeasures)
            .Include(e => e.TertiaryGroup)
            .ThenInclude(e => e.SecondaryGroup)
            .ThenInclude(e => e.PrimaryGroup)
            .AsNoTracking();
    }

    public async Task<IngredientDto> FindByName(string name)
    {
        return await _mapper.ProjectTo<IngredientDto>(FullLazyLoad(_context.Ingredients))
            .FirstAsync(e => e.Name.ToLower().Equals(name));
    }

    public async Task<IngredientDto> FindById(int id)
    {
        return await _mapper.ProjectTo<IngredientDto>(FullLazyLoad(_context.Ingredients))
            .FirstAsync(e => e.Id == id);
    }

    public async Task<List<IngredientDto>> FindByPrimaryGroup(string name)
    {
        return await _mapper.ProjectTo<IngredientDto>(FullLazyLoad(_context.Ingredients))
            .Where(e => e.TertiaryGroup.SecondaryGroup.PrimaryGroup.Name.ToLower().Equals(name))
            .ToListAsync();
    }

    public async Task<List<IngredientDto>> FindByPrimaryGroup(int id)
    {
        return await _mapper.ProjectTo<IngredientDto>(FullLazyLoad(_context.Ingredients))
            .Where(e => e.TertiaryGroup.SecondaryGroup.PrimaryGroup.Id == id)
            .ToListAsync();
    }

    public async Task<List<IngredientDto>> FindBySecondaryGroup(string name)
    {
        return await _mapper.ProjectTo<IngredientDto>(FullLazyLoad(_context.Ingredients))
            .Where(e => e.TertiaryGroup.SecondaryGroup.Name.ToLower().Equals(name))
            .ToListAsync();
    }

    public async Task<List<IngredientDto>> FindBySecondaryGroup(int id)
    {
        return await _mapper.ProjectTo<IngredientDto>(FullLazyLoad(_context.Ingredients))
            .Where(e => e.TertiaryGroup.SecondaryGroup.Id == id)
            .ToListAsync();
    }

    public async Task<List<IngredientDto>> FindByTertiaryGroup(string name)
    {
        return await _mapper.ProjectTo<IngredientDto>(FullLazyLoad(_context.Ingredients))
            .Where(e => e.TertiaryGroup.Name.ToLower().Equals(name))
            .ToListAsync();
    }

    public async Task<List<IngredientDto>> FindByTertiaryGroup(int id)
    {
        return await _mapper.ProjectTo<IngredientDto>(FullLazyLoad(_context.Ingredients))
            .Where(e => e.TertiaryGroup.Id == id)
            .ToListAsync();
    }

    public async Task<List<IngredientDto>> FindAll()
    {
        return await _mapper.ProjectTo<IngredientDto>(FullLazyLoad(_context.Ingredients))
            .ToListAsync();
    }
}