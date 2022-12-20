using API.Dto;
using AutoMapper;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Utils.Enum;

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

    public async Task<UserDto?> Find(string apiKey)
    {
        return await _mapper.ProjectTo<UserDto>(IncludeSubfields(_context.UserProfiles)
                .Where(e => e.ApiKey.Equals(apiKey)))
            .FirstOrDefaultAsync();
    }

    public async Task<UserDto?> Save(string username, string email, string apiKey)
    {
        var user = await _mapper.ProjectTo<UserDto>(IncludeSubfields(_context.UserProfiles)
                .Where(e => e.Username.ToLower().Equals(username) || e.Email.ToLower().Equals(email)))
            .FirstOrDefaultAsync();
        if (user != null) return null;
        var newUser = new UserProfile
        {
            Username = username,
            Email = email,
            ApiKey = apiKey,
            JoinedOn = DateTime.UtcNow.ToLocalTime()
        };
        _context.UserProfiles.Add(newUser);
        await _context.SaveChangesAsync();
        return _mapper.Map<UserProfile, UserDto>(newUser);
    }

    public async Task<UserDto?> SavePersonalData(string apiKey, UserDataDto userDataDto)
    {
        var user = await Find(apiKey);
        if (user == null) return null;
        var userData = new UserDatum
        {
            Id = user.Id,
            Name = userDataDto.Name,
            LastName = userDataDto.LastName,
            Birthdate = DateOnly.Parse(userDataDto.Birthdate),
            Gender = GenderEnum.FromReadableName(userDataDto.Gender) ?? throw new InvalidOperationException(),
            IntendedUse = IntendedUseEnum.FromReadableName(userDataDto.IntendedUse!),
            UpdateFrequency = UpdateFrequencyEnum.FromReadableName(userDataDto.UpdateFrequency!),
            Diet = DietEnum.FromReadableName(userDataDto.Diet!)
        };
        _context.UserData.Add(userData);
        await _context.SaveChangesAsync();
        return await Find(apiKey);
    }

    public async Task<UserDto?> SaveBodyMetrics(string apiKey, UserBodyMetricDto userBodyMetricDto)
    {
        var user = await Find(apiKey);
        if (user == null) return null;
        var bodyMetric = new UserBodyMetric
        {
            UserId = user.Id,
            Height = userBodyMetricDto.Height,
            Weight = userBodyMetricDto.Weight,
            BodyMassIndex = userBodyMetricDto.BodyMassIndex,
            PhysicalActivity = PhysicalActivityEnum.FromReadableName(userBodyMetricDto.PhysicalActivity) ??
                               throw new InvalidOperationException(),
            AddedOn = DateTime.UtcNow.ToLocalTime()
        };
        _context.UserBodyMetrics.Add(bodyMetric);
        await _context.SaveChangesAsync();
        user.UserBodyMetrics.Add(userBodyMetricDto);
        return await Find(apiKey);
    }

    public async Task<UserDto?> SaveMealPlan(string apiKey, MealPlanDto mealPlanDto)
    {
        var user = await Find(apiKey);
        if (user == null) return null;
        var mealPlan = new MealPlan
        {
            EnergyTarget = mealPlanDto.EnergyTarget,
            CarbohydratesTarget = mealPlanDto.CarbohydratesTarget,
            LipidsTarget = mealPlanDto.LipidsTarget,
            ProteinsTarget = mealPlanDto.ProteinsTarget
        };
        _context.MealPlans.Add(mealPlan);
        await _context.SaveChangesAsync();
        await SaveDailyMenus(mealPlan.Id, mealPlanDto.DailyMealPlans);
        user.MealPlan = mealPlanDto;
        return user;
    }

    private async Task SaveDailyMenus(int id, IEnumerable<DailyMealPlanDto> dailyMealPlans)
    {
        foreach (var mealPlan in dailyMealPlans)
        {
            var dailyMealPlan = new DailyMealPlan
            {
                MealPlanId = id,
                DayOfTheWeek = DayOfTheWeekEnum.FromReadableName(mealPlan.DayOfTheWeek) ?? DayOfTheWeekEnum.None,
                EnergyTotal = mealPlan.EnergyTotal,
                CarbohydratesTotal = mealPlan.CarbohydratesTotal,
                LipidsTotal = mealPlan.LipidsTotal,
                ProteinsTotal = mealPlan.ProteinsTotal
            };
            _context.DailyMealPlans.Add(dailyMealPlan);
            await _context.SaveChangesAsync();
            await SaveDailyMealPlans(dailyMealPlan.Id, mealPlan.DailyMenus);
        }
    }

    private async Task SaveDailyMealPlans(int id, IEnumerable<DailyMenuDto> dailyMenus)
    {
        foreach (var menu in dailyMenus)
        {
            var dailyMenu = new DailyMenu
            {
                DailyMealPlanId = id,
                MealType = MealTypeEnum.FromReadableName(menu.MealType) ?? MealTypeEnum.None,
                Satiety = SatietyEnum.FromReadableName(menu.Satiety) ?? SatietyEnum.None,
                EnergyTotal = menu.EnergyTotal,
                CarbohydratesTotal = menu.CarbohydratesTotal,
                LipidsTotal = menu.LipidsTotal,
                ProteinsTotal = menu.ProteinsTotal
            };
            _context.DailyMenus.Add(dailyMenu);
            await _context.SaveChangesAsync();
            await SaveMenuRecipes(dailyMenu.Id, menu.MenuRecipes);
        }
    }

    private async Task SaveMenuRecipes(int id, IEnumerable<MenuRecipeDto> menuRecipes)
    {
        foreach (var recipe in menuRecipes)
        {
            var menuRecipe = new MenuRecipe
            {
                RecipeId = recipe.Recipe.Id,
                DailyMenuId = id
            };
            _context.MenuRecipes.Add(menuRecipe);
            await _context.SaveChangesAsync();
        }
    }

    private static IQueryable<UserProfile> IncludeSubfields(IQueryable<UserProfile> users)
    {
        return users
            .Include(e => e.UserDatum!)
            .Include(e => e.UserBodyMetrics)
            .Include(e => e.MealPlan!)
            .ThenInclude(e => e.DailyMealPlans)
            .ThenInclude(e => e.DailyMenus)
            .ThenInclude(e => e.MenuRecipes)
            .ThenInclude(e => e.Recipe)
            .ThenInclude(e => e.RecipeNutrients)
            .ThenInclude(e => e.Nutrient)
            .ThenInclude(e => e.Subtype)
            .ThenInclude(e => e.Type);
    }
}