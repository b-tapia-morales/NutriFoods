using API.Dto;
using AutoMapper;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Utils.Enum;
using Utils.Nutrition;

namespace API.Users;

public class UserRepository : IUserRepository
{
    private readonly NutrifoodsDbContext _context;
    private readonly IMapper _mapper;

    public UserRepository(NutrifoodsDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserDto?> Find(string apiKey) =>
        await _mapper.ProjectTo<UserDto>(IncludeSubfields(_context.UserProfiles))
            .FirstOrDefaultAsync(e => e.ApiKey.Equals(apiKey));

    public async Task<UserDto?> Save(string username, string email, string apiKey)
    {
        var user = await _mapper.ProjectTo<UserDto>(IncludeSubfields(_context.UserProfiles))
            .FirstOrDefaultAsync(e => e.Username.ToLower().Equals(username) || e.Email.ToLower().Equals(email));
        if (user != null) return null;
        var newUser = new UserProfile
        {
            Username = username,
            Email = email,
            ApiKey = apiKey,
            JoinedOn = DateTime.Now.ToLocalTime()
        };
        _context.UserProfiles.Add(newUser);
        await _context.SaveChangesAsync();
        return _mapper.Map<UserProfile, UserDto>(newUser);
    }

    public async Task<UserDto?> SaveBodyMetrics(string apiKey, int height, double weight, PhysicalActivityEnum level)
    {
        var user = await Find(apiKey);
        if (user == null) return null;
        var bodyMetric = new UserBodyMetric
        {
            UserId = user.Id,
            Height = height,
            Weight = weight,
            BodyMassIndex = BodyMassIndex.Calculate(weight, height),
            PhysicalActivity = level,
            AddedOn = DateTime.Now.ToLocalTime()
        };
        _context.UserBodyMetrics.Add(bodyMetric);
        await _context.SaveChangesAsync();
        var bodyMetricDto = _mapper.Map<UserBodyMetric, UserBodyMetricDto>(bodyMetric);
        user.BodyMetrics.Add(bodyMetricDto);
        return user;
    }

    private static IQueryable<UserProfile> IncludeSubfields(IQueryable<UserProfile> users)
    {
        return users
            .Include(e => e.MealPlan!)
            .ThenInclude(e => e.DailyMealPlans)
            .ThenInclude(e => e.DailyMenus)
            .ThenInclude(e => e.MenuRecipes)
            .ThenInclude(e => e.Recipe)
            .ThenInclude(e => e.RecipeNutrients)
            .ThenInclude(e => e.Nutrient)
            .ThenInclude(e => e.Subtype)
            .ThenInclude(e => e.Type)
            .Include(e => e.UserBodyMetrics);
    }
}