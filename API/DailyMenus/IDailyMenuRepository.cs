using API.Dto;

namespace API.DailyMenus;

public interface IDailyMenuRepository
{
    DailyMenuDto GenerateMenu(DailyMenuDto dailyMenu);
    
    Task<DailyMenuDto> GenerateMenuAsync(DailyMenuDto dailyMenu);
}