using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using API.Dto;
using API.Utils;
using Domain.Enum;
using Microsoft.AspNetCore.Mvc;

namespace API.Users;

[ApiController]
[Route("api/v1/users")]
public class UserController
{
    private readonly IUserRepository _repository;

    public UserController(IUserRepository repository)
    {
        _repository = repository;
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
    public async Task<ActionResult<UserDto>> FindByUsername(string username, string password)
    {
        if (!Regex.IsMatch(username, RegexUtils.Username))
        {
            return new BadRequestObjectResult(
                $"Provided argument {username} does not match required string validation rules.\n{RegexUtils.UsernameRule}");
        }

        if (!Regex.IsMatch(password, RegexUtils.Password))
        {
            return new BadRequestObjectResult(
                $"Provided argument {password} does not match required string validation rules.\n{RegexUtils.PasswordRule}");
        }

        var user = await _repository.FindByUsername(username, password);
        if (user == null)
        {
            return new NotFoundObjectResult("There's no user with the specified username and password provided.");
        }

        return user;
    }

    [HttpGet]
    [Route("find-by-email")]
    public async Task<ActionResult<UserDto>> FindByEmail([FromQuery(Name = "email")] string email,
        [FromQuery(Name = "password")] string password)
    {
        if (!new EmailAddressAttribute().IsValid(email))
        {
            return new BadRequestObjectResult(
                $"Provided argument {email} does not correspond to a valid email.");
        }

        if (!Regex.IsMatch(password, RegexUtils.Password))
        {
            return new BadRequestObjectResult(
                $"Provided argument {password} does not match required string validation rules.\n{RegexUtils.PasswordRule}");
        }

        var user = await _repository.FindByEmail(email, password);
        if (user == null)
        {
            return new NotFoundObjectResult("There's no user with the specified key");
        }

        return user;
    }

    [HttpPut]
    [Route("save-user")]
    public async Task<ActionResult<UserDto>> SaveUser([Required] string username, [Required] string email,
        [Required] string password, [Required] string birthDate, [Required] string gender, string name = "",
        string lastName = "")
    {
        if (!Regex.IsMatch(username, RegexUtils.Username))
        {
            return new BadRequestObjectResult(
                $"Provided argument {username} does not match required string validation rules.\n{RegexUtils.UsernameRule}");
        }

        if (!new EmailAddressAttribute().IsValid(email))
        {
            return new BadRequestObjectResult(
                $"Provided argument {email} does not correspond to a valid email.");
        }

        if (!Regex.IsMatch(password, RegexUtils.Password))
        {
            return new BadRequestObjectResult(
                $"Provided argument {password} does not match required string validation rules.\n{RegexUtils.PasswordRule}");
        }

        if (!DateOnly.TryParse(birthDate, out var date))
        {
            return new BadRequestObjectResult(
                $"Provided argument {birthDate} does not correspond to a valid date.");
        }

        if (!Gender.TryFromName(gender, out var value))
        {
            return new BadRequestObjectResult(
                $"Provided argument {value} does not correspond to a valid gender.");
        }

        var user = await _repository.SaveUser(username, email, password, name, lastName, date, value);
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

        if (!PhysicalActivity.TryFromName(physicalActivity, out var value))
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