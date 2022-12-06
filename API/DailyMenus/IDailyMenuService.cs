using API.Dto;
using Utils.Enum;

namespace API.DailyMenus;

public interface IDailyMenuService
{
    Task<DailyMenuDto> GenerateDailyMenu(double energyTarget, double carbsPercent, double fatsPercent,
        double proteinsPercent, MealType mealType = MealType.None, Satiety satiety = Satiety.None);

    Task<DailyMenuDto> GenerateDailyMenu(double energyTarget, MealType mealType = MealType.None,
        Satiety satiety = Satiety.None);
}