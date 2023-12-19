using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.RegularExpressions;
using API.Dto;
using API.Validations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Utils;
using Utils.Enumerable;
using Utils.Rut;

namespace API.Nutritionists;

[ApiController]
[Route("api/v1/nutritionists")]
public partial class NutritionistController
{
    private readonly IValidator<NutritionistDto> _nutritionistValidator;
    private readonly IValidator<PatientDto> _patientValidator;
    private readonly INutritionistRepository _repository;

    public NutritionistController(IValidator<NutritionistDto> nutritionistValidator,
        IValidator<PatientDto> patientValidator, INutritionistRepository repository)
    {
        _repository = repository;
        _nutritionistValidator = nutritionistValidator;
        _patientValidator = patientValidator;
    }

    [HttpPost]
    [Route("/sign-up")]
    public async Task<ActionResult<NutritionistDto>> SignUp([FromBody] NutritionistDto dto)
    {
        var results = await _nutritionistValidator.ValidateAsync(dto);
        if (!results.IsValid)
            return new BadRequestObjectResult(
                $"""
                 Could not perform query because of the following errors:
                 {results.Errors.Select(e => e.ErrorMessage).ToJoinedString(Environment.NewLine)}
                 """
            );

        if (await _repository.IsEmailTaken(dto.Email))
            return new ConflictObjectResult($"The email “{dto.Email}” is already in use");

        if (await _repository.IsUsernameTaken(dto.Username))
            return new ConflictObjectResult($"The username “{dto.Username}” is already in use");

        return await _repository.SaveAccount(dto);
    }

    [HttpGet]
    [Route("/login")]
    public async Task<ActionResult<NutritionistDto>> Login(
        [FromQuery, Required] string email, [FromQuery, Required] string password)
    {
        if (!new EmailAddressAttribute().IsValid(email))
            return new BadRequestObjectResult($"Provided argument “{email}” does not correspond to a valid email.");

        if (!PasswordValidator().IsMatch(password))
            return new BadRequestObjectResult(MessageExtensions.IsNotAMatch("password", password,
                RegexUtils.PasswordRule));

        var dto = await _repository.FindAccount(email);
        if (dto == null)
            return new NotFoundObjectResult("Could not find an account with the given email");

        if (!PasswordEncryption.Verify(password, dto.Password))
            return new UnauthorizedObjectResult("The given password does not match with the account's password");

        return dto;
    }

    [HttpPost]
    [Route("/nutritionist/{nutritionistId:guid}/patient/")]
    public async Task<ActionResult<PatientDto>> AddPatient(Guid nutritionistId, [FromBody] PatientDto patientDto)
    {
        var nutritionistDto = await _repository.FindAccount(nutritionistId);
        if (nutritionistDto == null)
            return new NotFoundObjectResult("Could not find an account with the given id");

        var results = await _patientValidator.ValidateAsync(patientDto);
        if (!results.IsValid)
            return new BadRequestObjectResult(
                $"""
                 Could not perform query because of the following errors:
                 {results.Errors.Select(e => e.ErrorMessage).ToJoinedString(Environment.NewLine)}
                 """
            );

        var rut = RutUtils.FormatRut(patientDto.PersonalInfo!.Rut);
        if (await _repository.FindPatient(rut) != null)
            return new ConflictObjectResult($"The rut “{rut}” is already registered by another user");

        patientDto.PersonalInfo!.Rut = rut;
        return await _repository.AddPatient(nutritionistDto, patientDto);
    }

    [GeneratedRegex(RegexUtils.Password)]
    private static partial Regex PasswordValidator();
}