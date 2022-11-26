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
    private readonly IValidator<UserBodyMetricDto> _userBodyMetricValidator;

    public UserController(IUserRepository repository, IValidator<UserDto> userValidator,
        IValidator<UserBodyMetricDto> userBodyMetricValidator)
    {
        _repository = repository;
        _userValidator = userValidator;
        _userBodyMetricValidator = userBodyMetricValidator;
    }

    [HttpGet]
    [Route("api-key")]
    public async Task<ActionResult<UserDto>> Find(string apiKey)
    {
        var user = await _repository.Find(apiKey);
        if (user == null)
        {
            return new NotFoundObjectResult("There's no user with the specified key");
        }

        return user;
    }

    [HttpGet]
    [Route("find-by-username")]
    public async Task<ActionResult<UserDto>> FindByUsername([Required] string username, [Required] string password)
    {
        var user = new UserDto
        {
            Username = username,
            Password = password
        };
        var validationResult = await _userValidator.ValidateAsync(user);
        if (!validationResult.IsValid)
        {
            return new BadRequestObjectResult(Results.ValidationProblem(validationResult.ToDictionary(), null, null,
                400));
        }

        user = await _repository.FindByUsername(username, password);
        if (user == null)
        {
            return new NotFoundObjectResult("There's no user with the specified username and password provided.");
        }

        return user;
    }

    [HttpGet]
    [Route("find-by-email")]
    public async Task<ActionResult<UserDto>> FindByEmail([Required] string email, [Required] string password)
    {
        var user = new UserDto
        {
            Email = email,
            Password = password
        };
        var validationResult = await _userValidator.ValidateAsync(user);
        if (!validationResult.IsValid)
        {
            return new BadRequestObjectResult(Results.ValidationProblem(validationResult.ToDictionary()));
        }

        user = await _repository.FindByEmail(email, password);
        if (user == null)
        {
            return new NotFoundObjectResult("There's no user with the specified key");
        }

        return user;
    }

    [HttpPut]
    [Route("save-user")]
    public async Task<ActionResult<UserDto>> SaveUser([Required] string username, [Required] string email,
        [Required] string password, [Required] string birthDate, [Required] GenderToken gender, string? name = "",
        string? lastName = "")
    {
        var user = new UserDto
        {
            Username = username,
            Email = email,
            Password = password,
            Birthdate = birthDate,
            Gender = Gender.FromToken(gender).ReadableName,
            Name = string.IsNullOrWhiteSpace(name) ? null : name,
            LastName = string.IsNullOrWhiteSpace(lastName) ? null : lastName,
        };
        var validationResult = await _userValidator.ValidateAsync(user);
        if (!validationResult.IsValid)
        {
            return new BadRequestObjectResult(Results.ValidationProblem(validationResult.ToDictionary()));
        }

        var dateValue = DateOnly.ParseExact(birthDate, DateOnlyUtils.AllowedFormats, null);
        user =
            await _repository.SaveUser(username, email, password, name, lastName, dateValue, Gender.FromToken(gender));
        if (user == null)
        {
            return new BadRequestObjectResult(
                "Can't register user because there's a user already using the provided credentials");
        }

        return user;
    }

    [HttpPut]
    [Route("save-metrics")]
    public async Task<ActionResult<UserDto>> SaveUser([Required] string apiKey, [Required] int height,
        [Required] double weight, [Required] PhysicalActivityToken physicalActivity)
    {
        var bodyMetricDto = new UserBodyMetricDto
        {
            Height = height,
            Weight = weight,
            BodyMassIndex = BodyMassIndex.Calculate(weight, height),
            PhysicalActivity = PhysicalActivity.FromToken(physicalActivity).ReadableName
        };
        var validationResult = await _userBodyMetricValidator.ValidateAsync(bodyMetricDto);
        if (!validationResult.IsValid)
        {
            return new BadRequestObjectResult(Results.ValidationProblem(validationResult.ToDictionary()));
        }

        var user = await _repository.SaveBodyMetrics(apiKey, height, weight,
            PhysicalActivity.FromToken(physicalActivity));
        if (user == null)
        {
            return new NotFoundObjectResult("There's no user with the specified key");
        }

        return user;
    }
}