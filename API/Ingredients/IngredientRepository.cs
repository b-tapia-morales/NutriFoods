using System.Linq.Expressions;
using API.Dto;
using AutoMapper;
using Domain.Enum;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Utils.Enumerable;
using Utils.String;
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
            .FindAllBy(e => e.NutritionalValues.Any(x => x.Nutrient == nutrient))
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
        _mapper.Map<List<IngredientDto>>(await _context.Ingredients
            .FindAllBy(e => e.FoodGroup == group)
            .Paginate(pageNumber, pageSize)
            .ToListAsync()
        );

    public async Task<IngredientDto> InsertSynonyms(IngredientDto dto, SynonymInsertion insertion)
    {
        var ingredient = await _context.Ingredients.FirstAsync(e => e.Id == dto.Id);
        var synonyms = new HashSet<string>(ingredient.Synonyms.Select(e => e.Standardize()));
        var stateChanged = false;
        foreach (var synonym in insertion.Synonyms)
        {
            var normalized = synonym.Standardize();
            if (synonyms.Contains(normalized))
                continue;
            ingredient.Synonyms.Add(synonym);
            synonyms.Add(normalized);
            stateChanged = true;
        }

        if (!stateChanged) return dto;

        await _context.SaveChangesAsync();
        dto.Synonyms.Copy(ingredient.Synonyms);
        return dto;
    }

    public async IAsyncEnumerable<IngredientDto> InsertSynonyms(List<SynonymInsertion> insertions)
    {
        foreach (var insertion in insertions)
        {
            var ingredient = await FindByName(insertion.Ingredient);
            if (ingredient == null)
                continue;
            yield return await InsertSynonyms(ingredient, insertion);
        }
    }

    public async IAsyncEnumerable<IngredientDto> InsertMeasures(List<MeasureInsertion> insertions)
    {
        foreach (var insertion in insertions)
        {
            var ingredient = await FindByName(insertion.Ingredient);
            if (ingredient == null)
                continue;
            yield return await InsertMeasures(ingredient, insertion);
        }
    }

    public async Task<IngredientDto> InsertMeasures(IngredientDto dto, MeasureInsertion insertion)
    {
        var ingredient = await _context.Ingredients.Include(e => e.IngredientMeasures).FirstAsync(e => e.Id == dto.Id);
        var measures = new HashSet<string>(ingredient.IngredientMeasures.Select(e => e.Name.Standardize()));
        var stateChanged = false;
        foreach (var measure in insertion.Measures)
        {
            var normalized = measure.Name.Standardize();
            if (measures.Contains(normalized))
                continue;
            ingredient.IngredientMeasures.Add(new IngredientMeasure { Name = measure.Name, Grams = measure.Grams });
            measures.Add(normalized);
            stateChanged = true;
        }

        if (!stateChanged) return dto;

        await _context.SaveChangesAsync();
        dto.Measures.Copy(_mapper.Map<List<IngredientMeasureDto>>(ingredient.IngredientMeasures));
        return dto;
    }
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