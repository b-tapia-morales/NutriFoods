using System.Linq.Expressions;
using API.Dto;
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

    Task<NutritionistDto> AddPatient(NutritionistDto nutritionistDto, PatientDto patientDto);

    static async Task<Nutritionist?> FindAccountBy(
        NutrifoodsDbContext context, Expression<Func<Nutritionist, bool>> predicate) =>
        await context.Nutritionists.IncludeFields().Where(predicate).FirstOrDefaultAsync();

    static async Task<Patient?> FindPatientBy(
        NutrifoodsDbContext context, Expression<Func<Patient, bool>> predicate) =>
        await context.Patients.IncludeFields().Where(predicate).FirstOrDefaultAsync();
}