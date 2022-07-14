using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using API.Dto;
using API.Utils;
using Domain.Enum;
using Domain.Models;
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
    public async Task<ActionResult<UserDto>> FindByUsername([FromQuery(Name = "username")] string username, [FromQuery(Name = "password")] string password)
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
    public async Task<ActionResult<UserDto>> FindByEmail([FromQuery(Name = "email")] string email, [FromQuery(Name = "password")] string password)
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
    public async Task<ActionResult<UserDto>> SaveUser([Required] string username, [Required] string email,
        [Required] string password, [Required] DateOnly birthDate, [Required] string gender, string name = "", string lastName = "")
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

        var user = await _repository.SaveUser(username, email, password, name, lastName, birthDate, Gender.FromName(gender));
        if (user == null)
        {
            return new UnauthorizedObjectResult(
                "Can't register user because there's a user already using the provided credentials");
        }

        return user;
    }
}