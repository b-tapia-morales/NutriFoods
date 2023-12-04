using API.Dto;

namespace API.DailyMenus;

public interface IDailyMenuRepository
{
    Task<DailyMenuDto> GenerateMenu(DailyMenuDto dailyMenu, IReadOnlyList<RecipeDto> recipes);
}