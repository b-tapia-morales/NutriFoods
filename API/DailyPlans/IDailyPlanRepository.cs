using API.Dto;

namespace API.DailyPlans;

public interface IDailyPlanRepository
{
    Task<DailyMenuDto> GenerateMenus(DailyMenuDto dailyMenu, IReadOnlyList<RecipeDto> recipes);
}