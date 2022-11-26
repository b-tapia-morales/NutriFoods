using System.ComponentModel.DataAnnotations;
using API.Dto;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Utils.Date;
using Utils.Enum;

namespace API.Users;

[ApiController]
[Route("api/v1/users")]
public class UserController
{
    private readonly IUserRepository _repository;
    private readonly IValidator<UserDto> _validator;

    public UserController(IUserRepository repository, IValidator<UserDto> validator)
    {
        _repository = repository;
        _validator = validator;
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
        var validationResult = await _validator.ValidateAsync(user);
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
        var validationResult = await _validator.ValidateAsync(user);
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
        [Required] string password, [Required] string birthDate, [Required] string gender, string? name = "",
        string? lastName = "")
    {
        var user = new UserDto
        {
            Username = username,
            Email = email,
            Password = password,
            Birthdate = birthDate,
            Gender = gender,
            Name = string.IsNullOrWhiteSpace(name) ? null : name,
            LastName = string.IsNullOrWhiteSpace(lastName) ? null : lastName,
        };
        var validationResult = await _validator.ValidateAsync(user);
        if (!validationResult.IsValid)
        {
            return new BadRequestObjectResult(Results.ValidationProblem(validationResult.ToDictionary()));
        }

        var dateValue = DateOnly.ParseExact(birthDate, DateOnlyUtils.AllowedFormats, null);
        var genderValue = Gender.ReadOnlyDictionary[gender];
        user = await _repository.SaveUser(username, email, password, name, lastName, dateValue, genderValue);
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
        [Required] double weight, [Required] string physicalActivity, double muscleMassPercentage = 0)
    {
        if (height is < 150 or > 200)
        {
            return new BadRequestObjectResult(
                "Minimum and maximum values allowed for height are 150 and 200 [cm] respectively)");
        }

        if (weight is < 50 or > 150)
        {
            return new BadRequestObjectResult(
                "Minimum and maximum values allowed for weight are 50 and 150 [kg] respectively)");
        }

        if (muscleMassPercentage is < 0 or > 1)
        {
            return new BadRequestObjectResult(
                "Minimum and maximum values allowed for muscle mass percentage are 0% and 100% respectively)");
        }

        if (!PhysicalActivity.TryFromName(physicalActivity, true, out var value))
        {
            return new BadRequestObjectResult(
                $"Provided argument {value} does not correspond to a valid gender.");
        }

        var user = await _repository.SaveBodyMetrics(apiKey, height, weight, value, muscleMassPercentage);
        if (user == null)
        {
            return new NotFoundObjectResult("There's no user with the specified key");
        }

        return user;
    }
}