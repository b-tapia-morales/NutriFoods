using API.Dto;
using Utils.Enum;

namespace API.Users;

public interface IUserRepository
{
    Task<UserDto?> Find(string apiKey);

    public Task<UserDto?> Save(string username, string email, string apiKey);

    public Task<UserDto?> SaveBodyMetrics(string apiKey, int height, double weight, PhysicalActivityEnum level);
}