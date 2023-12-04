using System.Linq.Expressions;
using API.Dto;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Nutritionists;

public interface INutritionistRepository
{
    Task<NutritionistDto?> Find(string email, string password);
    
    static async Task<Nutritionist?> FindBy(Expression<Func<Nutritionist, bool>> predicate)
    {
        await using var context = new NutrifoodsDbContext();
        return await context.Nutritionists.Where(predicate).FirstAsync();
    }
}