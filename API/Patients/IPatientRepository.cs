using System.Linq.Expressions;
using API.Dto;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Patients;

public interface IPatientRepository
{
    Task<PatientDto?> Find(Guid id);

    Task<PatientDto?> Find(string rut);

    Task<PatientDto> Create(PatientDto patientDto, Guid nutritionistId);

    Task<PatientDto> Add(PatientDto patientDto, ConsultationDto consultationDto);

    static async Task<Patient?> FindBy(Expression<Func<Patient, bool>> predicate)
    {
        await using var context = new NutrifoodsDbContext();
        return await context.Patients.IncludeSubfields().Where(predicate).FirstAsync();
    }
}