using API.Dto;
using Domain.Enum;
using static Domain.Enum.IEnum<Domain.Enum.MealTypes,Domain.Enum.MealToken>;

namespace API.DailyMenus;

public interface IDailyMenuRepository
{
    Task<DailyMenuDto> GenerateMenu(DailyMenuDto dailyMenu, MealTypes mealType);

    Task<DailyMenuDto> GenerateMenu(DailyMenuDto dailyMenu) =>
        GenerateMenu(dailyMenu, ToValue(dailyMenu.MealType));

    async IAsyncEnumerable<DailyMenuDto> ToTasks(List<DailyMenuDto> menus, bool useFilter = true)
    {
        foreach (var menu in menus)
            yield return await GenerateMenu(menu, useFilter ? ToValue(menu.MealType) : MealTypes.None);
    }
}