using System.ComponentModel.DataAnnotations;
using API.Dto;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Utils.Date;
using Utils.Enum;
using Utils.Nutrition;

namespace API.Users;

[ApiController]
[Route("api/v1/users")]
public class UserController
{
    private readonly IUserRepository _repository;
    private readonly IValidator<UserDto> _userValidator;
    private readonly IValidator<UserDataDto> _userDataValidator;
    private readonly IValidator<UserBodyMetricDto> _userBodyMetricValidator;

    public UserController(IUserRepository repository, IValidator<UserDto> userValidator,
        IValidator<UserDataDto> userDataValidator, IValidator<UserBodyMetricDto> userBodyMetricValidator)
    {
        _repository = repository;
        _userValidator = userValidator;
        _userDataValidator = userDataValidator;
        _userBodyMetricValidator = userBodyMetricValidator;
    }

    [HttpGet]
    [Route("api-key")]
    public async Task<ActionResult<UserDto>> Find(string apiKey)
    {
        var user = await _repository.Find(apiKey);
        if (user == null) return new NotFoundObjectResult("There's no user with the specified key");
        var personalData = user.UserData;
        var bodyMetrics = user.UserBodyMetrics.MaxBy(e => e.AddedOn);
        user.TotalMetabolicRate = personalData == null || bodyMetrics == null
            ? null
            : TotalMetabolicRate.Calculate(GenderEnum.FromReadableName(personalData.Gender)!, bodyMetrics.Weight,
                bodyMetrics.Height, DateOnlyUtils.Difference(DateOnly.Parse(personalData.Birthdate), Interval.Years),
                PhysicalActivityEnum.FromReadableName(bodyMetrics.PhysicalActivity)!);
        return user;
    }

    [HttpPut]
    [Route("save-user")]
    public async Task<ActionResult<UserDto>> SaveUser([Required] string username, [Required] string email,
        [Required] string apiKey)
    {
        var user = new UserDto
        {
            Username = username,
            Email = email,
            ApiKey = apiKey
        };
        var validationResult = await _userValidator.ValidateAsync(user);
        if (!validationResult.IsValid)
        {
            return new BadRequestObjectResult(Results.ValidationProblem(validationResult.ToDictionary()));
        }

        user = await _repository.Save(username, email, apiKey);

        return user == null
            ? new BadRequestObjectResult(
                "Can't register user because there's a user already using the provided credentials")
            : user;
    }

    [HttpPut]
    [Route("save-data")]
    public async Task<ActionResult<UserDto>> SavePersonalData([Required] string apiKey, string birthdate, Gender gender,
        string? name = "", string? lastName = "", Diet diet = Diet.None, IntendedUse intendedUse = IntendedUse.None,
        UpdateFrequency updateFrequency = UpdateFrequency.None)
    {
        var userData = new UserDataDto
        {
            Name = name ?? string.Empty,
            LastName = lastName ?? string.Empty,
            Birthdate = birthdate,
            Gender = GenderEnum.FromToken(gender).ReadableName,
            Diet = DietEnum.FromToken(diet).ReadableName,
            IntendedUse = IntendedUseEnum.FromToken(intendedUse).ReadableName,
            UpdateFrequency = UpdateFrequencyEnum.FromToken(updateFrequency).ReadableName
        };
        var validationResult = await _userDataValidator.ValidateAsync(userData);
        if (!validationResult.IsValid)
        {
            return new BadRequestObjectResult(Results.ValidationProblem(validationResult.ToDictionary()));
        }

        var user = await _repository.SavePersonalData(apiKey, userData);

        return user == null ? new NotFoundObjectResult("There's no user with the specified key") : user;
    }

    [HttpPost]
    [Route("save-metrics")]
    public async Task<ActionResult<UserDto>> SaveMetrics([Required] string apiKey, [Required] int height,
        [Required] double weight, [Required] PhysicalActivity physicalActivity)
    {
        var bodyMetricDto = new UserBodyMetricDto
        {
            Height = height,
            Weight = weight,
            BodyMassIndex = BodyMassIndex.Calculate(weight, height),
            PhysicalActivity = PhysicalActivityEnum.FromToken(physicalActivity).ReadableName
        };
        var validationResult = await _userBodyMetricValidator.ValidateAsync(bodyMetricDto);
        if (!validationResult.IsValid)
        {
            return new BadRequestObjectResult(Results.ValidationProblem(validationResult.ToDictionary()));
        }

        var user = await _repository.SaveBodyMetrics(apiKey, bodyMetricDto);

        return user == null ? new NotFoundObjectResult("There's no user with the specified key") : user;
    }
}