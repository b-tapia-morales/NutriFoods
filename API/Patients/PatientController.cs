using API.Dto;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Utils.Enumerable;

namespace API.Patients;

[ApiController]
[Route("api/v1/patients")]
public class PatientController
{
    private readonly IValidator<ConsultationDto> _consultationValidator;
    private readonly IPatientRepository _repository;

    public PatientController(IPatientRepository repository, IValidator<ConsultationDto> consultationValidator)
    {
        _repository = repository;
        _consultationValidator = consultationValidator;
    }


    [HttpPost]
    [Route("/nutritionist/{nutritionistId:guid}/patient/{patientDto}")]
    public async Task<ActionResult<PatientDto>> Create(Guid nutritionistId, [FromBody] PatientDto patientDto)
    {
        if (await _repository.Find(patientDto.PersonalInfo!.Rut) != null)
            return new BadRequestObjectResult("");

        return await _repository.Create(patientDto, nutritionistId);
    }

    [HttpPost]
    [Route("/patient/{patientId:guid}/consultation/{consultationDto}")]
    public async Task<ActionResult<PatientDto>> Create(Guid patientId, [FromBody] ConsultationDto consultationDto)
    {
        var results = await _consultationValidator.ValidateAsync(consultationDto);
        if (!results.IsValid)
            return new BadRequestObjectResult(
                $"""
                 Could not perform query because of the following errors:
                 {results.Errors.Select(e => e.ErrorMessage).ToJoinedString(Environment.NewLine)}
                 """
            );

        var patientDto = await _repository.Find(patientId);
        if (patientDto == null)
            return new BadRequestObjectResult("");

        return await _repository.Add(patientDto, consultationDto);
    }
}