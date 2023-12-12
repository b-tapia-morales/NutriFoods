using System.Linq.Expressions;
using API.Dto;
using API.Nutritionists;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Patients;

public interface IPatientRepository
{
    Task<PatientDto?> FindPatient(Guid id);

    Task<PatientDto?> FindPatient(string rut);

    Task<ConsultationDto?> FindConsultation(Guid id);

    Task<ConsultationDto> AddConsultation(PatientDto patientDto, ConsultationDto consultationDto);

    Task<ConsultationDto> AddClinicalAnamnesis(ConsultationDto consultationDto,
        ClinicalAnamnesisDto clinicalAnamnesisDto);

    Task<ConsultationDto> AddNutritionalAnamnesis(ConsultationDto consultationDto,
        NutritionalAnamnesisDto nutritionalAnamnesisDto);

    Task<ConsultationDto> AddAnthropometry(ConsultationDto consultationDto,
        AnthropometryDto anthropometryDto);

    static Task<Patient?> FindPatientBy(NutrifoodsDbContext context,
        Expression<Func<Patient, bool>> predicate) =>
        context.Patients.IncludeSubfields().Where(predicate).FirstOrDefaultAsync();

    static Task<Consultation?> FindConsultationBy(NutrifoodsDbContext context,
        Expression<Func<Consultation, bool>> predicate) =>
        context.Consultations.IncludeSubfields().Where(predicate).FirstOrDefaultAsync();
}