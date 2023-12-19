using API.Dto;
using AutoMapper;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Utils;
using static API.Nutritionists.INutritionistRepository;

namespace API.Nutritionists;

public class NutritionistRepository : INutritionistRepository
{
    private readonly NutrifoodsDbContext _context;
    private readonly IMapper _mapper;

    public NutritionistRepository(NutrifoodsDbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<bool> IsEmailTaken(string email) =>
        await _context.Nutritionists.IncludeSubfields()
            .FirstOrDefaultAsync(e => e.Email.ToLower().Equals(email.ToLower())) != null;

    public async Task<bool> IsUsernameTaken(string accountName) =>
        await _context.Nutritionists.IncludeSubfields()
            .FirstOrDefaultAsync(e => e.Username.ToLower().Equals(accountName.ToLower())) != null;

    public async Task<NutritionistDto?> FindAccount(string email) =>
        _mapper.Map<NutritionistDto>(
            await FindNutritionistBy(_context, e => e.Email.ToLower().Equals(email.ToLower())));

    public async Task<NutritionistDto?> FindAccount(Guid id) =>
        _mapper.Map<NutritionistDto>(await FindNutritionistBy(_context, e => e.Id == id));

    public async Task<NutritionistDto> SaveAccount(NutritionistDto dto)
    {
        var nutritionist = _mapper.Map<Nutritionist>(dto);
        nutritionist.Password = PasswordEncryption.Hash(nutritionist.Password);
        await _context.Nutritionists.AddAsync(nutritionist);
        await _context.SaveChangesAsync();
        dto.Id = nutritionist.Id;
        dto.Password = nutritionist.Password;
        dto.JoinedOn = nutritionist.JoinedOn;
        return dto;
    }

    public async Task<PatientDto?> FindPatient(string rut) =>
        _mapper.Map<PatientDto>(await FindPatientBy(_context,
            e => e.PersonalInfo != null && e.PersonalInfo.Rut == rut));

    public async Task<PatientDto> AddPatient(NutritionistDto nutritionistDto, PatientDto patientDto)
    {
        var patient = new Patient { NutritionistId = nutritionistDto.Id };
        await _context.Patients.AddAsync(patient);
        await _context.SaveChangesAsync();
        var id = patient.Id;
        patient.PersonalInfo = _mapper.Map<PersonalInfo>(patientDto.PersonalInfo);
        patient.ContactInfo = _mapper.Map<ContactInfo>(patientDto.ContactInfo);
        patient.Address = _mapper.Map<Address>(patientDto.Address);
        await _context.SaveChangesAsync();
        patientDto.Id = id;
        patientDto.JoinedOn = patient.JoinedOn;
        return patientDto;
    }
}

public static class NutritionistExtensions
{
    public static IQueryable<Nutritionist> IncludeSubfields(this DbSet<Nutritionist> nutritionists)
    {
        return nutritionists
            .AsQueryable()
            .Include(e => e.Patients).ThenInclude(e => e.PersonalInfo)
            .Include(e => e.Patients).ThenInclude(e => e.ContactInfo)
            .Include(e => e.Patients).ThenInclude(e => e.Address)
            .Include(e => e.Patients).ThenInclude(e => e.Consultations).ThenInclude(e => e.ClinicalAnamnesis)
            .Include(e => e.Patients).ThenInclude(e => e.Consultations)
            .ThenInclude(e => e.ClinicalAnamnesis).ThenInclude(e => e!.Diseases)
            .Include(e => e.Patients).ThenInclude(e => e.Consultations)
            .ThenInclude(e => e.ClinicalAnamnesis).ThenInclude(e => e!.ClinicalSigns)
            .Include(e => e.Patients).ThenInclude(e => e.Consultations)
            .ThenInclude(e => e.ClinicalAnamnesis).ThenInclude(e => e!.Ingestibles)
            .Include(e => e.Patients).ThenInclude(e => e.Consultations).ThenInclude(e => e.NutritionalAnamnesis)
            .Include(e => e.Patients).ThenInclude(e => e.Consultations)
            .ThenInclude(e => e.NutritionalAnamnesis).ThenInclude(e => e!.HarmfulHabits)
            .Include(e => e.Patients).ThenInclude(e => e.Consultations)
            .ThenInclude(e => e.NutritionalAnamnesis).ThenInclude(e => e!.EatingSymptoms)
            .Include(e => e.Patients).ThenInclude(e => e.Consultations)
            .ThenInclude(e => e.NutritionalAnamnesis).ThenInclude(e => e!.AdverseFoodReactions)
            .Include(e => e.Patients).ThenInclude(e => e.Consultations)
            .ThenInclude(e => e.NutritionalAnamnesis).ThenInclude(e => e!.FoodConsumptions)
            .Include(e => e.Patients).ThenInclude(e => e.Consultations).ThenInclude(e => e.Anthropometry);
    }

    
}