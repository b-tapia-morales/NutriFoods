using System.Linq.Expressions;
using API.Dto;
using API.Patients;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Nutritionists;

public interface INutritionistRepository
{
    Task<bool> IsEmailTaken(string email);

    Task<bool> IsUsernameTaken(string accountName);

    Task<NutritionistDto?> FindAccount(string email);

    Task<NutritionistDto?> FindAccount(Guid id);

    Task<NutritionistDto> SaveAccount(NutritionistDto dto);

    Task<PatientDto?> FindPatient(string rut);

    Task<PatientDto> AddPatient(NutritionistDto nutritionistDto, PatientDto patientDto);

    static Task<Nutritionist?> FindNutritionistBy(
        NutrifoodsDbContext context, Expression<Func<Nutritionist, bool>> predicate) =>
        context.Nutritionists.IncludeSubfields().Where(predicate).FirstOrDefaultAsync();

    static Task<Patient?> FindPatientBy(
        NutrifoodsDbContext context, Expression<Func<Patient, bool>> predicate) =>
        context.Patients.IncludeSubfields().Where(predicate).FirstOrDefaultAsync();
}