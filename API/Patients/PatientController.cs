using System.Globalization;
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

    [HttpGet]
    [Route("/{patientId:guid}")]
    public async Task<ActionResult<PatientDto>> FindPatient(Guid patientId)
    {
        var patientDto = await _repository.FindPatient(patientId);
        if (patientDto == null)
            return new BadRequestObjectResult("");

        return patientDto;
    }

    [HttpGet]
    [Route("/{patientId:guid}/consultations/")]
    public async Task<ActionResult<IEnumerable<ConsultationDto>>> FindConsultations(Guid patientId)
    {
        var patientDto = await _repository.FindPatient(patientId);
        if (patientDto == null)
            return new NotFoundObjectResult($"There's no registered patient with the Id {patientId}");

        return patientDto.Consultations
            .OrderByDescending(e => DateOnly.Parse(e.RegisteredOn!, CultureInfo.InvariantCulture)).ToList();
    }

    [HttpGet]
    [Route("/{patientId:guid}/consultations/latest")]
    public async Task<ActionResult<ConsultationDto>> FindLatestConsultation(Guid patientId)
    {
        var patientDto = await _repository.FindPatient(patientId);
        if (patientDto == null)
            return new NotFoundObjectResult($"There's no registered patient with the Id {patientId}");

        if (patientDto.Consultations.Count == 0)
            return new NotFoundObjectResult($"The patient with the Id {patientId} has no registered consultations");

        return patientDto.Consultations.MaxBy(e => DateOnly.Parse(e.RegisteredOn!, CultureInfo.InvariantCulture))!;
    }

    [HttpPost]
    [Route("/{patientId:guid}/consultations/")]
    public async Task<ActionResult<ConsultationDto>> CreateConsultation(Guid patientId,
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

        var patientDto = await _repository.FindPatient(patientId);
        if (patientDto == null)
            return new BadRequestObjectResult("");

        return await _repository.AddConsultation(patientDto, consultationDto);
    }

    [HttpPut]
    [Route("/{patientId:guid}/consultations/{consultationId:guid}/clinical-anamnesis/")]
    public async Task<ActionResult<ConsultationDto>> AddClinicalAnamnesis(Guid patientId, Guid consultationId,
        [FromBody] ClinicalAnamnesisDto clinicalAnamnesisDto)
    {
        var patientDto = await _repository.FindPatient(patientId);
        if (patientDto == null)
            return new NotFoundObjectResult($"There's no registered patient with the Id {patientId}");

        var consultationDto = await _repository.FindConsultation(consultationId);
        if (consultationDto == null)
            return new NotFoundObjectResult($"There's no registered consultation with the Id {consultationId}");

        return await _repository.AddClinicalAnamnesis(consultationDto, clinicalAnamnesisDto);
    }

    [HttpPut]
    [Route("/{patientId:guid}/consultations/{consultationId:guid}/nutritional-anamnesis/")]
    public async Task<ActionResult<ConsultationDto>> AddNutritionalAnamnesis(Guid patientId, Guid consultationId,
        [FromBody] NutritionalAnamnesisDto nutritionalAnamnesisDto)
    {
        var patientDto = await _repository.FindPatient(patientId);
        if (patientDto == null)
            return new NotFoundObjectResult($"There's no registered patient with the Id {patientId}");

        var consultationDto = await _repository.FindConsultation(consultationId);
        if (consultationDto == null)
            return new NotFoundObjectResult($"There's no registered consultation with the Id {consultationId}");

        return await _repository.AddNutritionalAnamnesis(consultationDto, nutritionalAnamnesisDto);
    }

    [HttpPut]
    [Route("/{patientId:guid}/consultations/{consultationId:guid}/anthropometry/")]
    public async Task<ActionResult<ConsultationDto>> AddNutritionalAnamnesis(Guid patientId, Guid consultationId,
        [FromBody] AnthropometryDto anthropometryDto)
    {
        var patientDto = await _repository.FindPatient(patientId);
        if (patientDto == null)
            return new NotFoundObjectResult($"There's no registered patient with the Id {patientId}");

        var consultationDto = await _repository.FindConsultation(consultationId);
        if (consultationDto == null)
            return new NotFoundObjectResult($"There's no registered consultation with the Id {consultationId}");

        return await _repository.AddAnthropometry(consultationDto, anthropometryDto);
    }
}