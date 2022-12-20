using API.Dto;

namespace API.Users;

public interface IUserRepository
{
    Task<UserDto?> Find(string apiKey);

    public Task<UserDto?> Save(string username, string email, string apiKey);

    public Task<UserDto?> SavePersonalData(string apiKey, UserDataDto userDataDto);

    public Task<UserDto?> SaveBodyMetrics(string apiKey, UserBodyMetricDto userBodyMetricDto);

    public Task<UserDto?> SaveMealPlan(string apiKey, MealPlanDto mealPlanDto);
}