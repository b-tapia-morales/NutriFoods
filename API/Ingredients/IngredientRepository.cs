using API.Dto;
using AutoMapper;
using Domain.Enum;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Ingredients;

public class IngredientRepository(NutrifoodsDbContext context, IMapper mapper) : IIngredientRepository
{
    public async Task<List<IngredientDto>> FindAll() =>
        await mapper.ProjectTo<IngredientDto>(IncludeFields(context.Ingredients))
            .ToListAsync();

    public async Task<IngredientDto?> FindByName(string name) =>
        await mapper.ProjectTo<IngredientDto>(IncludeFields(context.Ingredients))
            .FirstAsync(e => e.Name.ToLower().Equals(name));

    public async Task<IngredientDto?> FindById(int id) =>
        await mapper.ProjectTo<IngredientDto>(IncludeFields(context.Ingredients))
            .FirstAsync(e => e.Id == id);

    public async Task<List<IngredientDto>> FindByFoodGroup(FoodGroups group) =>
        await mapper.ProjectTo<IngredientDto>(IncludeFields(context.Ingredients)
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