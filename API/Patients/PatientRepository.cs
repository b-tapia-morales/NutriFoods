using API.Dto;
using API.Dto.Insertion;
using AutoMapper;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using static API.Patients.IPatientRepository;

namespace API.Patients;

public class PatientRepository : IPatientRepository
{
    private readonly NutrifoodsDbContext _context;
    private readonly IMapper _mapper;

    public PatientRepository(NutrifoodsDbContext context, IMapper mapper)
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

    public async Task<ConsultationDto> AddConsultation(PatientDto patientDto, ConsultationDto consultationDto)
    {
        var consultation = _mapper.Map<Consultation>(consultationDto);
        consultation.PatientId = patientDto.Id;
        await _context.Consultations.AddAsync(consultation);
        await _context.SaveChangesAsync();
        consultationDto.Id = consultation.Id;
        return consultationDto;
    }

    public async Task<ConsultationDto> AddClinicalAnamnesis(ConsultationDto consultationDto,
        ClinicalAnamnesisDto clinicalAnamnesisDto)
    {
        var consultationId = consultationDto.Id;
        var previousRecord = await _context.ClinicalAnamneses.FirstOrDefaultAsync(e => e.Id == consultationId);
        DateTime? createdOn = null;
        if (previousRecord != null)
        {
            createdOn = previousRecord.CreatedOn;
            _context.Remove(previousRecord);
            await _context.SaveChangesAsync();
        }

        var clinicalAnamnesis = _mapper.Map<ClinicalAnamnesis>(clinicalAnamnesisDto);
        clinicalAnamnesis.Id = consultationId;
        if (previousRecord != null)
            clinicalAnamnesis.CreatedOn = clinicalAnamnesisDto.CreatedOn = createdOn;
        else
            clinicalAnamnesis.LastUpdated = clinicalAnamnesisDto.LastUpdated = null;

        await _context.ClinicalAnamneses.AddAsync(clinicalAnamnesis);
        await _context.SaveChangesAsync();

        consultationDto.ClinicalAnamnesis = clinicalAnamnesisDto;
        return consultationDto;
    }

    public async Task<ConsultationDto> AddNutritionalAnamnesis(ConsultationDto consultationDto,
        NutritionalAnamnesisDto nutritionalAnamnesisDto)
    {
        var consultationId = consultationDto.Id;
        var previousRecord = await _context.NutritionalAnamneses.FirstOrDefaultAsync(e => e.Id == consultationId);
        DateTime? createdOn = null;
        if (previousRecord != null)
        {
            createdOn = previousRecord.CreatedOn;
            _context.Remove(previousRecord);
            await _context.SaveChangesAsync();
        }

        var nutritionalAnamnesis = _mapper.Map<NutritionalAnamnesis>(nutritionalAnamnesisDto);
        nutritionalAnamnesis.Id = consultationId;
        if (previousRecord != null)
            nutritionalAnamnesis.CreatedOn = nutritionalAnamnesisDto.CreatedOn = createdOn;
        else
            nutritionalAnamnesis.LastUpdated = nutritionalAnamnesisDto.LastUpdated = null;

        await _context.NutritionalAnamneses.AddAsync(nutritionalAnamnesis);
        await _context.SaveChangesAsync();

        consultationDto.NutritionalAnamnesis = nutritionalAnamnesisDto;
        return consultationDto;
    }

    public async Task<ConsultationDto> AddAnthropometry(ConsultationDto consultationDto,
        AnthropometryDto anthropometryDto)
    {
        var consultationId = consultationDto.Id;
        var previousRecord = await _context.Anthropometries.FirstOrDefaultAsync(e => e.Id == consultationId);
        DateTime? createdOn = null;
        if (previousRecord != null)
        {
            createdOn = previousRecord.CreatedOn;
            _context.Remove(previousRecord);
            await _context.SaveChangesAsync();
        }

        var anthropometry = _mapper.Map<Anthropometry>(anthropometryDto);
        anthropometry.Id = consultationId;
        if (previousRecord != null)
            anthropometry.CreatedOn = anthropometryDto.CreatedOn = createdOn;
        else
            anthropometry.LastUpdated = anthropometryDto.LastUpdated = null;

        await _context.Anthropometries.AddAsync(anthropometry);
        await _context.SaveChangesAsync();

        consultationDto.Anthropometry = anthropometryDto;
        return consultationDto;
    }

    public async Task<ConsultationDto> AddDailyPlans(ConsultationDto consultationDto,
        List<MinimalDailyPlan> minimalDailyPlans)
    {
        var consultationId = consultationDto.Id;
        var previousRecord = await _context.Consultations.FirstAsync(e => e.Id == consultationId);
        previousRecord.DailyPlans?.Clear();
        await _context.SaveChangesAsync();

        var dailyPlans = _mapper.Map<List<DailyPlan>>(minimalDailyPlans);

        previousRecord.DailyPlans ??= new List<DailyPlan>();
        foreach (var plan in dailyPlans)
            previousRecord.DailyPlans.Add(plan);
        await _context.SaveChangesAsync();

        consultationDto.DailyPlans = _mapper.Map<List<DailyPlanDto>>(dailyPlans);
        return consultationDto;
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