using API.Dto;
using AutoMapper;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Recipes;

public class RecipeRepository : IRecipeRepository
{
    private readonly NutrifoodsDbContext _context;
    private readonly IMapper _mapper;

    public RecipeRepository(NutrifoodsDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RecipeDto>> FindAll()
    {
        return await _mapper.ProjectTo<RecipeDto>(IncludeSubfields(_context.Recipes)).ToListAsync();
    }

    public async Task<RecipeDto> FindByName(string name)
    {
        return await _mapper.ProjectTo<RecipeDto>(IncludeSubfields(_context.Recipes))
            .Where(e => e.Name.Equals(name))
            .FirstAsync();
    }

    public async Task<RecipeDto> FindById(int id)
    {
        return await _mapper.ProjectTo<RecipeDto>(_context.Recipes).Where(e => e.Id == id).FirstAsync();
    }

    public async Task<IEnumerable<RecipeDto>> ExcludeSecondaryGroups(IEnumerable<int> ids)
    {
        var findMeasures = _mapper.ProjectTo<RecipeDto>(IncludeSubfields(_context.Recipes)
                .Where(e => !e.RecipeMeasures.Any(m =>
                    ids.Contains(m.IngredientMeasure.Ingredient.TertiaryGroup.SecondaryGroup.Id))))
            .ToListAsync();
        var findQuantities = _mapper.ProjectTo<RecipeDto>(IncludeSubfields(_context.Recipes)
                .Where(e => !e.RecipeQuantities.Any(m =>
                    ids.Contains(m.Ingredient.TertiaryGroup.SecondaryGroup.Id))))
            .ToListAsync();
        await Task.WhenAll(findMeasures, findQuantities);
        var measures = await findMeasures;
        var quantities = await findQuantities;
        return measures.Concat(quantities).ToList();
    }

    public async Task<IEnumerable<RecipeDto>> ExcludeTertiaryGroups(IEnumerable<int> ids)
    {
        var findMeasures = _mapper.ProjectTo<RecipeDto>(IncludeSubfields(_context.Recipes)
                .Where(e => !e.RecipeMeasures.Any(m =>
                    ids.Contains(m.IngredientMeasure.Ingredient.TertiaryGroup.Id))))
            .ToListAsync();
        var findQuantities = _mapper.ProjectTo<RecipeDto>(IncludeSubfields(_context.Recipes)
                .Where(e => !e.RecipeQuantities.Any(m =>
                    ids.Contains(m.Ingredient.TertiaryGroup.Id))))
            .ToListAsync();
        await Task.WhenAll(findMeasures, findQuantities);
        var measures = await findMeasures;
        var quantities = await findQuantities;
        return measures.Concat(quantities).ToList();
    }

    private static IQueryable<Recipe> IncludeSubfields(IQueryable<Recipe> recipes)
    {
        return recipes
            .Include(e => e.RecipeSteps)
            .Include(e => e.RecipeNutrients)
            .ThenInclude(e => e.Nutrient)
            .ThenInclude(e => e.Subtype)
            .ThenInclude(e => e.Type)
            .Include(e => e.RecipeMeasures)
            .ThenInclude(e => e.IngredientMeasure)
            .ThenInclude(e => e.Ingredient)
            .ThenInclude(e => e.TertiaryGroup)
            .ThenInclude(e => e.SecondaryGroup)
            .ThenInclude(e => e.PrimaryGroup)
            .Include(e => e.RecipeQuantities)
            .ThenInclude(e => e.Ingredient)
            .ThenInclude(e => e.TertiaryGroup)
            .ThenInclude(e => e.SecondaryGroup)
            .ThenInclude(e => e.PrimaryGroup)
            .AsNoTracking();
    }
}