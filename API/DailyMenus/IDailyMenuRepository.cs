using API.Dto;
using Domain.Enum;

namespace API.DailyMenus;

public interface IDailyMenuRepository
{
    Task<DailyMenuDto> GenerateMenu(DailyMenuDto dailyMenu, IReadOnlyList<RecipeDto> recipes);
}