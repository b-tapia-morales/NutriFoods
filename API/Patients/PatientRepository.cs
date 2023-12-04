using API.Dto;
using AutoMapper;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using static API.Patients.IPatientRepository;

namespace API.Patients;

public class PatientRepository : IPatientRepository
{
    private readonly IMapper _mapper;

    public PatientRepository(IMapper mapper) => _mapper = mapper;

    public async Task<PatientDto?> Find(Guid id) =>
        _mapper.Map<PatientDto>(await FindBy(e => e.Id == id));

    public async Task<PatientDto?> Find(string rut) =>
        _mapper.Map<PatientDto>(await FindBy(e => e.PersonalInfo!.Rut.Equals(rut)));

    public async Task<PatientDto> Create(PatientDto patientDto, Guid nutritionistId)
    {
        await using var context = new NutrifoodsDbContext();
        var patient = new Patient { NutritionistId = nutritionistId };
        await context.Patients.AddAsync(patient);
        await context.SaveChangesAsync();
        patient.PersonalInfo = _mapper.Map<PersonalInfo>(patientDto.PersonalInfo);
        patient.ContactInfo = _mapper.Map<ContactInfo>(patientDto.ContactInfo);
        patient.Address = _mapper.Map<Address>(patientDto.Address);
        await context.SaveChangesAsync();
        patientDto.Id = patient.Id;
        return patientDto;
    }

    public async Task<PatientDto> Add(PatientDto patientDto, ConsultationDto consultationDto)
    {
        await using var context = new NutrifoodsDbContext();
        var consultation = _mapper.Map<Consultation>(consultationDto);
        consultation.PatientId = patientDto.Id;
        context.Consultations.Add(consultation);
        await context.SaveChangesAsync();
        consultationDto.Id = consultation.Id;
        patientDto.Consultations.Add(consultationDto);
        return patientDto;
    }
}

public static class PatientExtensions
{
    public static IQueryable<Patient> IncludeSubfields(this DbSet<Patient> dbSet) =>
        dbSet.AsQueryable()
            // Patient info
            .Include(e => e.PersonalInfo)
            .Include(e => e.ContactInfo)
            .Include(e => e.Address)
            .Include(e => e.Consultations)
            // Consultations
            .Include(e => e.Consultations)
            // Clinical anamnesis
            .Include(e => e.Consultations).ThenInclude(e => e.ClinicalAnamnesis)
            // Clinical anamnesis - fields
            .Include(e => e.Consultations).ThenInclude(e => e.ClinicalAnamnesis!.Diseases)
            .Include(e => e.Consultations).ThenInclude(e => e.ClinicalAnamnesis!.ClinicalSigns)
            .Include(e => e.Consultations).ThenInclude(e => e.ClinicalAnamnesis!.Ingestibles)
            // Nutritional anamnesis
            .Include(e => e.Consultations).ThenInclude(e => e.NutritionalAnamnesis)
            // Nutritional anamnesis - fields
            .Include(e => e.Consultations).ThenInclude(e => e.NutritionalAnamnesis!.EatingSymptoms)
            .Include(e => e.Consultations).ThenInclude(e => e.NutritionalAnamnesis!.HarmfulHabits)
            .Include(e => e.Consultations).ThenInclude(e => e.NutritionalAnamnesis!.FoodConsumptions)
            .Include(e => e.Consultations).ThenInclude(e => e.NutritionalAnamnesis!.AdverseFoodReactions)
            // Anthropometry
            .Include(e => e.Consultations).ThenInclude(e => e.Anthropometry);
}