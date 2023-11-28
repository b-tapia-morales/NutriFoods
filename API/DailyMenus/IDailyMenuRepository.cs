using API.Dto;
using Domain.Enum;

namespace API.DailyMenus;

public interface IDailyMenuRepository
{
    Task<DailyMenuDto> GenerateMenuAsync(DailyMenuDto dailyMenu, MealTypes mealType, double energy, int chromosomeSize,
        double ratio);
}