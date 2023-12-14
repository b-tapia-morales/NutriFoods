using API.Dto;

namespace API.DailyMenus;

public interface IDailyMenuRepository
{
    Task<DailyMenuDto> GenerateMenu(DailyMenuDto dailyMenu, IReadOnlyList<RecipeDto> recipes);

    async IAsyncEnumerable<DailyMenuDto> ToTasks(List<DailyMenuDto> menus, IReadOnlyList<RecipeDto> recipes)
    {
        foreach (var menu in menus)
            yield return await GenerateMenu(menu, recipes);
    }
}