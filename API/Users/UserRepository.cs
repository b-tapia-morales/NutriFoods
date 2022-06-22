using Application.Utils;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Users;

public class UserRepository : IUserRepository
{
    private readonly NutrifoodsDbContext _context;

    public UserRepository(NutrifoodsDbContext context)
    {
        _context = context;
    }

    public Task<UserProfile> Find(string apiKey)
    {
        return _context
            .UserProfiles
            .FirstAsync(e => e.ApiKey.Equals(apiKey));
    }

    public async Task<UserProfile> FindByUsername(string username, string password)
    {
        var user = await _context.UserProfiles.FirstAsync(e => e.Username.ToLower().Equals(username));
        if (PasswordEncryption.Verify(password, user.Password))
        {
            throw new ArgumentException("No user with the specified password has been found");
        }

        return user;
    }

    public async Task<UserProfile> FindByEmail(string email, string password)
    {
        var user = await _context.UserProfiles.FirstAsync(e => e.Email.ToLower().Equals(email));
        if (PasswordEncryption.Verify(password, user.Password))
        {
            throw new ArgumentException("No user with the specified password has been found");
        }

        return user;
    }
}