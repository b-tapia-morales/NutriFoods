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

    public async Task<List<RecipeDto>> FindAll()
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

    public async Task<List<RecipeDto>> ExcludeSecondaryGroups(IEnumerable<int> ids)
    {
        return await _mapper.ProjectTo<RecipeDto>(IncludeSubfields(_context.Recipes)
                .Where(e => !e.RecipeMeasures.Any(m =>
                    ids.Contains(m.IngredientMeasure.Ingredient.TertiaryGroup.SecondaryGroup.Id)))
                .Where(e => !e.RecipeQuantities.Any(m =>
                    ids.Contains(m.Ingredient.TertiaryGroup.SecondaryGroup.Id))))
            .ToListAsync();
    }

    public async Task<List<RecipeDto>> ExcludeTertiaryGroups(IEnumerable<int> ids)
    {
        return await _mapper.ProjectTo<RecipeDto>(IncludeSubfields(_context.Recipes)
                .Where(e => !e.RecipeMeasures.Any(m =>
                    ids.Contains(m.IngredientMeasure.Ingredient.TertiaryGroup.Id)))
                .Where(e => !e.RecipeQuantities.Any(m =>
                    ids.Contains(m.Ingredient.TertiaryGroup.Id))))
            .ToListAsync();
    }

    public async Task<List<RecipeDto>> FilterByPreparationTime(int lowerBound, int upperBound)
    {
        return await _mapper.ProjectTo<RecipeDto>(IncludeSubfields(_context.Recipes)
                .Where(e => e.PreparationTime != null && e.PreparationTime >= lowerBound &&
                            e.PreparationTime <= upperBound))
            .ToListAsync();
    }

    public async Task<List<RecipeDto>> FilterByPortions(int portions)
    {
        return await _mapper.ProjectTo<RecipeDto>(IncludeSubfields(_context.Recipes)
                .Where(e => e.Portions != null && e.Portions == portions))
            .ToListAsync();
    }

    public async Task<List<RecipeDto>> FilterByPortions(int lowerBound, int upperBound)
    {
        return await _mapper.ProjectTo<RecipeDto>(IncludeSubfields(_context.Recipes)
                .Where(e => e.Portions != null && e.Portions >= lowerBound && e.Portions <= upperBound))
            .ToListAsync();
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