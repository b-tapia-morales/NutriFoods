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

    Task<PatientDto> AddConsultation(PatientDto patientDto, ConsultationDto consultationDto);

    Task<PatientDto> AddClinicalAnamnesis(PatientDto patientDto, ConsultationDto consultationDto,
        ClinicalAnamnesisDto clinicalAnamnesisDto);

    Task<PatientDto> AddNutritionalAnamnesis(PatientDto patientDto, ConsultationDto consultationDto,
        NutritionalAnamnesisDto nutritionalAnamnesisDto);

    Task<PatientDto> AddAnthropometry(PatientDto patientDto, ConsultationDto consultationDto,
        AnthropometryDto anthropometryDto);

    static async Task<Patient?> FindPatientBy(NutrifoodsDbContext context, 
        Expression<Func<Patient, bool>> predicate) =>
        await context.Patients.IncludeSubfields().Where(predicate).FirstOrDefaultAsync();

    static async Task<Consultation?> FindConsultationBy(NutrifoodsDbContext context,
        Expression<Func<Consultation, bool>> predicate) =>
        await context.Consultations.IncludeSubfields().Where(predicate).FirstOrDefaultAsync();
}