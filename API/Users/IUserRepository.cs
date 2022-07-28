using API.Dto;
using Utils.Enum;

namespace API.Users;

public interface IUserRepository
{
    Task<UserDto?> Find(string apiKey);

    Task<UserDto?> FindByUsername(string username, string password);

    Task<UserDto?> FindByEmail(string email, string password);

    public Task<UserDto?> SaveUser(string username, string email, string password, string? name, string? lastName,
        DateOnly birthDate, Gender gender);

    public Task<UserDto?> SaveBodyMetrics(string apiKey, int height, double weight, PhysicalActivity level,
        double? muscleMassPercentage);
}