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
    [Route("/{patientId:guid}/consultation/")]
    public async Task<ActionResult<PatientDto>> Create(Guid patientId,
        [FromBody] ConsultationDto consultationDto)
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

        return await _repository.AddConsultation(patientDto, consultationDto);
    }

    [HttpPost]
    [Route("/{patientId:guid}/consultation/{consultationId:guid}/clinical-anamnesis/")]
    public async Task<ActionResult<PatientDto>> Create(Guid patientId, Guid consultationId,
        [FromBody] ClinicalAnamnesisDto clinicalAnamnesisDto)
    {
        var patientDto = await _repository.Find(patientId);
        if (patientDto == null)
            return new BadRequestObjectResult("");

        var consultationDto = patientDto.Consultations.FirstOrDefault(e => e.Id == consultationId);
        if (consultationDto == null)
            return new BadRequestObjectResult("");

        return await _repository.AddClinicalAnamnesis(patientDto, consultationDto, clinicalAnamnesisDto);
    }
}