using System.ComponentModel.DataAnnotations;
using API.Dto;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
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
        if (user == null)
        {
            return new BadRequestObjectResult(
                "Can't register user because there's a user already using the provided credentials");
        }

        return user;
    }

    [HttpPut]
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

        var user = await _repository.SaveBodyMetrics(apiKey, height, weight,
            PhysicalActivityEnum.FromToken(physicalActivity));
        if (user == null)
        {
            return new NotFoundObjectResult("There's no user with the specified key");
        }

        return user;
    }
}