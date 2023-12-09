using System.Linq.Expressions;
using API.Dto;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Nutritionists;

public interface INutritionistRepository
{
    Task<bool> IsEmailTaken(string email);

    Task<bool> IsUsernameTaken(string accountName);

    Task<NutritionistDto?> FindAccount(string email);
    
    Task<NutritionistDto?> FindAccount(Guid id);

    Task<NutritionistDto> SaveAccount(NutritionistDto dto);

    static async Task<Nutritionist?> FindBy(
        NutrifoodsDbContext context, Expression<Func<Nutritionist, bool>> predicate) =>
        await context.Nutritionists.IncludeFields().Where(predicate).FirstAsync();
}