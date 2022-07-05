using Domain.Models;

namespace API.Users;

public interface IUserRepository
{
    Task<UserProfile> Find(string apiKey);

    Task<UserProfile> FindByUsername(string username, string password);

    Task<UserProfile> FindByEmail(string email, string password);
}