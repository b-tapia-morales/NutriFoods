using API.Dto;

namespace API.DailyMenus;

public interface IDailyMenuRepository
{
    Task<DailyMenuDto> GenerateMenuAsync(DailyMenuDto dailyMenu);
    
    Task<DailyMenuDto> GenerateMenuAsync(DailyMenuDto dailyMenu, IReadOnlyList<RecipeDto> recipes);
}