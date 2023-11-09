using API.Dto.Abridged;
using AutoMapper;
using Domain.Enum;
using Domain.Models;
using EatingSymptom = Domain.Models.EatingSymptom;
using HarmfulHabit = Domain.Models.HarmfulHabit;

namespace API.Dto;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Ingredient
        CreateMap<IngredientMeasure, IngredientMeasureDto>()
            .ReverseMap();
        CreateMap<IngredientNutrient, IngredientNutrientDto>()
            .ForMember(dest => dest.Nutrient, opt => opt.MapFrom(src => src.Nutrient.ReadableName))
            .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit.ReadableName))
            .ReverseMap();
        CreateMap<Ingredient, IngredientDto>()
            .ForMember(dest => dest.Synonyms,
                opt => opt.MapFrom(src => (src.Synonyms ?? Array.Empty<string>()).ToList()))
            .ForMember(dest => dest.FoodGroup, opt => opt.MapFrom(src => src.FoodGroup.ReadableName))
            .ForMember(dest => dest.Measures, opt => opt.MapFrom(src => src.IngredientMeasures))
            .ForMember(dest => dest.Nutrients, opt => opt.MapFrom(src => src.IngredientNutrients))
            .ReverseMap();

        // Ingredient Abridged
        CreateMap<Ingredient, IngredientAbridged>()
            .ForMember(dest => dest.Synonyms,
                opt => opt.MapFrom(src => (src.Synonyms ?? Array.Empty<string>()).ToList()))
            .ForMember(dest => dest.FoodGroup, opt => opt.MapFrom(src => src.FoodGroup.ReadableName))
            .ReverseMap();
        CreateMap<IngredientMeasure, IngredientMeasureAbridged>()
            .ReverseMap();

        CreateMap<RecipeMeasure, RecipeMeasureDto>()
            .ReverseMap();
        CreateMap<RecipeQuantity, RecipeQuantityDto>()
            .ReverseMap();
        CreateMap<RecipeStep, RecipeStepDto>()
            .ReverseMap();
        CreateMap<RecipeNutrient, RecipeNutrientDto>()
            .ForMember(dest => dest.Nutrient, opt => opt.MapFrom(src => src.Nutrient.ReadableName))
            .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit.ReadableName))
            .ReverseMap();
        CreateMap<Recipe, RecipeDto>()
            .ForMember(dest => dest.Measures, opt => opt.MapFrom(src => src.RecipeMeasures))
            .ForMember(dest => dest.Quantities, opt => opt.MapFrom(src => src.RecipeQuantities))
            .ForMember(dest => dest.Steps, opt => opt.MapFrom(src => src.RecipeSteps))
            .ForMember(dest => dest.Nutrients, opt => opt.MapFrom(src => src.RecipeNutrients))
            .ForMember(dest => dest.MealTypes, opt => opt
                .MapFrom(src =>
                    (src.MealTypes ?? Array.Empty<MealType>()).Select(e => e.ReadableName).ToList()))
            .ForMember(dest => dest.DishTypes,
                opt => opt.MapFrom(src =>
                    (src.DishTypes ?? Array.Empty<DishType>()).Select(e => e.ReadableName).ToList()))
            .ReverseMap();

        // Recipe Abridged
        CreateMap<Recipe, RecipeAbridged>()
            .ForMember(dest => dest.Measures, opt => opt.MapFrom(src => src.RecipeMeasures))
            .ForMember(dest => dest.Quantities, opt => opt.MapFrom(src => src.RecipeQuantities))
            .ForMember(dest => dest.Steps, opt => opt.MapFrom(src => src.RecipeSteps))
            .ForMember(dest => dest.MealTypes, opt => opt
                .MapFrom(src =>
                    (src.MealTypes ?? Array.Empty<MealType>()).Select(e => e.ReadableName).ToList()))
            .ForMember(dest => dest.DishTypes,
                opt => opt.MapFrom(src =>
                    (src.DishTypes ?? Array.Empty<DishType>()).Select(e => e.ReadableName).ToList()))
            .ReverseMap();

        // Meal Plan
        CreateMap<MenuRecipe, MenuRecipeDto>()
            .ReverseMap();
        CreateMap<DailyMenuNutrient, DailyMenuNutrientDto>()
            .ForMember(dest => dest.Nutrient, opt => opt.MapFrom(src => src.Nutrient.ReadableName))
            .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit.ReadableName))
            .ReverseMap();
        CreateMap<DailyMenu, DailyMenuDto>()
            .ForMember(dest => dest.MealType, opt => opt.MapFrom(src => src.MealType.ReadableName))
            .ForMember(dest => dest.Nutrients, opt => opt.MapFrom(src => src.DailyMenuNutrients))
            .ForMember(dest => dest.Recipes, opt => opt.MapFrom(src => src.MenuRecipes))
            .ReverseMap();
        CreateMap<DailyPlanNutrient, DailyPlanNutrientDto>()
            .ForMember(dest => dest.Nutrient, opt => opt.MapFrom(src => src.Nutrient.ReadableName))
            .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit.ReadableName))
            .ReverseMap();
        CreateMap<DailyPlanTarget, DailyPlanTargetDto>()
            .ForMember(dest => dest.Nutrient, opt => opt.MapFrom(src => src.Nutrient.ReadableName))
            .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit.ReadableName))
            .ForMember(dest => dest.ThresholdType, opt => opt.MapFrom(src => src.ThresholdType.ReadableName))
            .ReverseMap();
        CreateMap<DailyPlan, DailyPlanDto>()
            .ForMember(dest => dest.Day, opt => opt.MapFrom(src => src.Day.ReadableName))
            .ForMember(dest => dest.PhysicalActivityLevel,
                opt => opt.MapFrom(src => src.PhysicalActivityLevel.ReadableName))
            .ForMember(dest => dest.Nutrients, opt => opt.MapFrom(src => src.DailyPlanNutrients))
            .ForMember(dest => dest.Targets, opt => opt.MapFrom(src => src.DailyPlanTargets))
            .ForMember(dest => dest.Menus, opt => opt.MapFrom(src => src.DailyMenus))
            .ReverseMap();
        CreateMap<MealPlan, MealPlanDto>()
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn ?? GetPacificStandardTime()))
            .ForMember(dest => dest.Plans, opt => opt.MapFrom(src => src.DailyPlans))
            .ReverseMap();

        // Clinical Anamnesis
        CreateMap<Medication, MedicationDto>()
            .ForMember(e => e.Type, opt => opt.MapFrom(src => src.Type.ReadableName))
            .ForMember(e => e.AdministrationTimes, opt => opt.MapFrom(src => src.AdministrationTimes.ToList()))
            .ForMember(e => e.Adherence, opt => opt.MapFrom(src => src.Adherence.ReadableName))
            .ReverseMap();
        CreateMap<Disease, DiseaseDto>()
            .ForMember(e => e.InheritanceType, opt => opt.MapFrom(src => src.InheritanceType.ReadableName))
            .ReverseMap();
        CreateMap<ClinicalSign, ClinicalSignDto>()
            .ReverseMap();
        CreateMap<ClinicalAnamnesis, ClinicalAnamnesisDto>()
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn ?? GetPacificStandardTime()))
            .ForMember(dest => dest.LastUpdated, opt => opt.MapFrom(src => src.LastUpdated ?? GetPacificStandardTime()))
            .ReverseMap();

        // Nutritional Anamnesis
        CreateMap<HarmfulHabit, HarmfulHabitDto>()
            .ReverseMap();
        CreateMap<EatingSymptom, EatingSymptomDto>()
            .ReverseMap();
        CreateMap<AdverseFoodReaction, AdverseFoodReactionDto>()
            .ForMember(dest => dest.FoodGroup, opt => opt.MapFrom(src => src.FoodGroup.ReadableName))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ReadableName))
            .ReverseMap();
        CreateMap<FoodConsumption, FoodConsumptionDto>()
            .ForMember(dest => dest.FoodGroup, opt => opt.MapFrom(src => src.FoodGroup.ReadableName))
            .ForMember(dest => dest.Frequency, opt => opt.MapFrom(src => src.Frequency.ReadableName))
            .ReverseMap();
        CreateMap<NutritionalAnamnesis, NutritionalAnamnesisDto>()
            .ForMember(dest => dest.CreatedOn,
                opt => opt.MapFrom(src => src.CreatedOn ?? GetPacificStandardTime()))
            .ForMember(dest => dest.LastUpdated,
                opt => opt.MapFrom(src => src.LastUpdated ?? GetPacificStandardTime()))
            .ReverseMap();

        // Anthropometry
        CreateMap<Anthropometry, AnthropometryDto>()
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn ?? GetPacificStandardTime()))
            .ForMember(dest => dest.LastUpdated, opt => opt.MapFrom(src => src.LastUpdated ?? GetPacificStandardTime()))
            .ReverseMap();

        // Consultation
        CreateMap<Consultation, ConsultationDto>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ReadableName))
            .ForMember(dest => dest.Purpose, opt => opt.MapFrom(src => src.Purpose.ReadableName))
            .ForMember(dest => dest.RegisteredOn,
                opt => opt.MapFrom(src => (src.RegisteredOn ?? GetPacificStandardDate()).ToString("YYYY/mm/dd")))
            .ReverseMap();
        
        // Patient
        CreateMap<PersonalInfo, PersonalInfoDto>()
            .ForMember(dest => dest.BiologicalSex, opt => opt.MapFrom(src => src.BiologicalSex.ReadableName))
            .ForMember(dest => dest.Birthdate, opt => opt.MapFrom(src => src.Birthdate.ToString("YYYY/mm/dd")))
            .ReverseMap();
        CreateMap<ContactInfo, ContactInfoDto>()
            .ReverseMap();
        CreateMap<Address, AddressDto>()
            .ForMember(dest => dest.Province, opt => opt.MapFrom(src => src.Province.ReadableName))
            .ReverseMap();
        CreateMap<Patient, PatientDto>()
            .ReverseMap();
        
        // Nutritionist
        CreateMap<Nutritionist, NutritionistDto>()
            .ReverseMap();
    }

    private static DateTime GetPacificStandardTime() =>
        TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time"));

    private static DateOnly GetPacificStandardDate() =>
        DateOnly.FromDateTime(GetPacificStandardTime());
}