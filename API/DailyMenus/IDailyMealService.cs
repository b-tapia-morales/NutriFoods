using API.Dto;
using Utils.Enum;

namespace API.DailyMenus;

public interface IDailyMenuService
{
    Task<DailyMenuDto> GenerateDailyMenu(MealTypeEnum mealType, double energyTarget, double carbsPercent,
        double fatsPercent, double proteinsPercent);

    Task<DailyMenuDto> GenerateDailyMenu(MealTypeEnum mealType, double energyTarget);
}