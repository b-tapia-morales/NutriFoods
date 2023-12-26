using System.Collections.Concurrent;
using API.Dto;
using Domain.Enum;
using Utils.Parallel;
using static Domain.Enum.IEnum<Domain.Enum.MealTypes, Domain.Enum.MealToken>;

namespace API.DailyMenus;

public interface IDailyMenuRepository
{
    Task<DailyMenuDto> GenerateMenu(DailyMenuDto dailyMenu, MealTypes mealType);

    Task<DailyMenuDto> GenerateMenu(DailyMenuDto dailyMenu) =>
        GenerateMenu(dailyMenu, ToValue(dailyMenu.MealType));

    async IAsyncEnumerable<Task<DailyMenuDto>> ToTasks(List<DailyMenuDto> menus, bool useFilter = true)
    {
        foreach (var menu in menus)
            yield return GenerateMenu(menu, useFilter ? ToValue(menu.MealType) : MealTypes.None);
    }

    async Task<IEnumerable<DailyMenuDto>> Parallelize(List<DailyMenuDto> menus, bool useFilter = true)
    {
        var queue = new ConcurrentQueue<DailyMenuDto>();
        var tasks = ToTasks(menus, useFilter);
        await tasks.AsyncParallelForEach(async e =>
        {
            var menu = await e;
            queue.Enqueue(menu);
        });
        return queue;
    }
}