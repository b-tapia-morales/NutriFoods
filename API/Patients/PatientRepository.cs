using API.Dto;
using AutoMapper;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using static API.Patients.IPatientRepository;

namespace API.Patients;

public class PatientRepository : IPatientRepository
{
    private readonly NutrifoodsDbContext _context;
    private readonly IMapper _mapper;

    public PatientRepository(IMapper mapper, NutrifoodsDbContext context)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PatientDto?> FindPatient(Guid id) =>
        _mapper.Map<PatientDto>(await FindPatientBy(_context, e => e.Id == id));

    public async Task<PatientDto?> FindPatient(string rut) =>
        _mapper.Map<PatientDto>(await FindPatientBy(_context, e => e.PersonalInfo!.Rut.Equals(rut)));

    public async Task<ConsultationDto?> FindConsultation(Guid id) =>
        _mapper.Map<ConsultationDto>(await FindConsultationBy(_context, e => e.Id == id));

    public async Task<PatientDto> AddConsultation(PatientDto patientDto, ConsultationDto consultationDto)
    {
        await using var context = new NutrifoodsDbContext();
        var consultation = _mapper.Map<Consultation>(consultationDto);
        consultation.PatientId = patientDto.Id;
        await context.Consultations.AddAsync(consultation);
        await context.SaveChangesAsync();
        consultationDto.Id = consultation.Id;
        patientDto.Consultations.Add(consultationDto);
        return patientDto;
    }

    public async Task<PatientDto> AddClinicalAnamnesis(PatientDto patientDto, ConsultationDto consultationDto,
        ClinicalAnamnesisDto clinicalAnamnesisDto)
    {
        var consultationId = consultationDto.Id;
        var previousRecord = await _context.ClinicalAnamneses.FirstOrDefaultAsync(e => e.Id == consultationId);
        if (previousRecord != null)
        {
            _context.Remove(previousRecord);
            await _context.SaveChangesAsync();
        }

        var clinicalAnamnesis = _mapper.Map<ClinicalAnamnesis>(clinicalAnamnesisDto);
        clinicalAnamnesis.Id = consultationDto.Id;

        await _context.ClinicalAnamneses.AddAsync(clinicalAnamnesis);
        await _context.SaveChangesAsync();
        consultationDto.ClinicalAnamnesis = clinicalAnamnesisDto;
        return patientDto;
    }

    public async Task<PatientDto> AddNutritionalAnamnesis(PatientDto patientDto, ConsultationDto consultationDto,
        NutritionalAnamnesisDto nutritionalAnamnesisDto)
    {
        var anamnesis = _mapper.Map<NutritionalAnamnesis>(nutritionalAnamnesisDto);
        anamnesis.Id = consultationDto.Id;
        _context.NutritionalAnamneses.Update(anamnesis);
        await _context.SaveChangesAsync();
        consultationDto.NutritionalAnamnesis = nutritionalAnamnesisDto;
        return patientDto;
    }

    public async Task<PatientDto> AddAnthropometry(PatientDto patientDto, ConsultationDto consultationDto,
        AnthropometryDto anthropometryDto)
    {
        var anthropometry = _mapper.Map<Anthropometry>(anthropometryDto);
        anthropometry.Id = consultationDto.Id;
        _context.Anthropometries.Update(anthropometry);
        await _context.SaveChangesAsync();
        consultationDto.Anthropometry = anthropometryDto;
        return patientDto;
    }
}

public static class PatientExtensions
{
    public static IQueryable<Patient> IncludeSubfields(this DbSet<Patient> patients)
    {
        return patients
            .AsQueryable()
            .Include(e => e.PersonalInfo)
            .Include(e => e.ContactInfo)
            .Include(e => e.Address)
            // Clinical anamnesis
            .Include(e => e.Consultations)
            // Clinical anamnesis - fields
            .Include(e => e.Consultations).ThenInclude(e => e.ClinicalAnamnesis)
            .Include(e => e.Consultations).ThenInclude(e => e.ClinicalAnamnesis!.Diseases)
            .Include(e => e.Consultations).ThenInclude(e => e.ClinicalAnamnesis!.ClinicalSigns)
            .Include(e => e.Consultations).ThenInclude(e => e.ClinicalAnamnesis!.Ingestibles)
            // Nutritional anamnesis
            .Include(e => e.Consultations).ThenInclude(e => e.NutritionalAnamnesis)
            // Nutritional anamnesis - fields
            .Include(e => e.Consultations).ThenInclude(e => e.NutritionalAnamnesis!.HarmfulHabits)
            .Include(e => e.Consultations).ThenInclude(e => e.NutritionalAnamnesis!.EatingSymptoms)
            .Include(e => e.Consultations).ThenInclude(e => e.NutritionalAnamnesis!.AdverseFoodReactions)
            .Include(e => e.Consultations).ThenInclude(e => e.NutritionalAnamnesis!.FoodConsumptions)
            // Anthropometry
            .Include(e => e.Consultations).ThenInclude(e => e.Anthropometry);
    }

    public static IQueryable<Consultation> IncludeSubfields(this DbSet<Consultation> dbSet) =>
        dbSet.AsQueryable()
            // Clinical anamnesis
            .Include(e => e.ClinicalAnamnesis)
            // Clinical anamnesis - fields
            .Include(e => e.ClinicalAnamnesis!.Diseases)
            .Include(e => e.ClinicalAnamnesis!.ClinicalSigns)
            .Include(e => e.ClinicalAnamnesis!.Ingestibles)
            // Nutritional anamnesis
            .Include(e => e.NutritionalAnamnesis)
            // Nutritional anamnesis - fields
            .Include(e => e.NutritionalAnamnesis!.EatingSymptoms)
            .Include(e => e.NutritionalAnamnesis!.HarmfulHabits)
            .Include(e => e.NutritionalAnamnesis!.FoodConsumptions)
            .Include(e => e.NutritionalAnamnesis!.AdverseFoodReactions)
            // Anthropometry
            .Include(e => e.Anthropometry);
}