using System.Collections.Immutable;
using System.Globalization;
using API.Dto.Abridged;
using API.Dto.Insertion;
using AutoMapper;
using Domain.Enum;
using Domain.Models;
using Utils.Enumerable;

namespace API.Dto;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Nutritional Value
        CreateMap<NutritionalValue, NutritionalValueDto>()
            .ForMember(dest => dest.Nutrient, opt => opt.MapFrom(src => src.Nutrient.ReadableName))
            .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit.ReadableName))
            .ReverseMap()
            .ForMember(dest => dest.Nutrient,
                opt => opt.MapFrom(src => IEnum<Nutrients, NutrientToken>.ToValue(src.Nutrient)))
            .ForMember(dest => dest.Unit,
                opt => opt.MapFrom(src => IEnum<Units, UnitToken>.ToValue(src.Unit)));

        // Ingredient
        CreateMap<IngredientMeasure, IngredientMeasureDto>()
            .ReverseMap();

        CreateMap<Ingredient, IngredientDto>()
            .ForMember(dest => dest.FoodGroup, opt => opt.MapFrom(src => src.FoodGroup.ReadableName))
            .ForMember(dest => dest.Measures, opt => opt.MapFrom(src => src.IngredientMeasures))
            .ForMember(dest => dest.Nutrients, opt => opt.MapFrom(src => src.NutritionalValues))
            .ReverseMap()
            .ForMember(dest => dest.FoodGroup,
                opt => opt.MapFrom(src => IEnum<FoodGroups, FoodGroupToken>.ToValue(src.FoodGroup)))
            .ForMember(dest => dest.IngredientMeasures, opt => opt.MapFrom(src => src.Measures))
            .ForMember(dest => dest.NutritionalValues, opt => opt.MapFrom(src => src.Nutrients));

        // Ingredient Abridged
        CreateMap<Ingredient, IngredientAbridged>()
            .ForMember(dest => dest.FoodGroup, opt => opt.MapFrom(src => src.FoodGroup.ReadableName))
            .ReverseMap()
            .ForMember(dest => dest.FoodGroup,
                opt => opt.MapFrom(src => IEnum<FoodGroups, FoodGroupToken>.ToValue(src.FoodGroup)));

        CreateMap<IngredientMeasure, IngredientMeasureAbridged>()
            .ReverseMap();

        // Recipes
        CreateMap<RecipeMeasure, RecipeMeasureDto>()
            .ReverseMap();

        CreateMap<RecipeQuantity, RecipeQuantityDto>()
            .ReverseMap();

        CreateMap<RecipeStep, RecipeStepDto>()
            .ReverseMap();

        CreateMap<Recipe, RecipeDto>()
            .ForMember(dest => dest.Measures, opt => opt.MapFrom(src => src.RecipeMeasures))
            .ForMember(dest => dest.Quantities, opt => opt.MapFrom(src => src.RecipeQuantities))
            .ForMember(dest => dest.Steps, opt => opt.MapFrom(src => src.RecipeSteps))
            .ForMember(dest => dest.Nutrients, opt => opt.MapFrom(src => src.NutritionalValues))
            .ForMember(dest => dest.MealTypes,
                opt => opt.MapFrom(src => src.MealTypes.Select(e => e.ReadableName).ToList()))
            .ForMember(dest => dest.DishTypes,
                opt => opt.MapFrom(src => src.DishTypes.Select(e => e.ReadableName).ToList()))
            .ForMember(dest => dest.NutrientDict, opt =>
                opt.MapFrom(src => src.NutritionalValues.ToSortedDictionary(e => e.Nutrient.ReadableName, e => e,
                    IEnum<Nutrients, NutrientToken>.ReadableNameComparer)))
            .ForMember(dest => dest.Difficulty,
                opt => opt.MapFrom(src => src.Difficulty == null ? string.Empty : src.Difficulty.ReadableName))
            .ReverseMap()
            .ForMember(dest => dest.RecipeMeasures, opt => opt.MapFrom(src => src.Measures))
            .ForMember(dest => dest.RecipeQuantities, opt => opt.MapFrom(src => src.Quantities))
            .ForMember(dest => dest.RecipeSteps, opt => opt.MapFrom(src => src.Steps))
            .ForMember(dest => dest.NutritionalValues, opt => opt.MapFrom(src => src.Nutrients))
            .ForMember(dest => dest.MealTypes,
                opt => opt.MapFrom(src => src.MealTypes.Select(IEnum<MealTypes, MealToken>.ToValue).ToList()))
            .ForMember(dest => dest.DishTypes,
                opt => opt.MapFrom(src => src.MealTypes.Select(IEnum<DishTypes, DishToken>.ToValue).ToList()))
            .ForMember(dest => dest.Difficulty,
                opt => opt.MapFrom(src =>
                    IEnum<Difficulties, DifficultyToken>.ToValue(src.Difficulty ?? string.Empty)));

        // Nutritional Target
        CreateMap<NutritionalTarget, NutritionalTargetDto>()
            .ForMember(dest => dest.Nutrient, opt => opt.MapFrom(src => src.Nutrient.ReadableName))
            .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit.ReadableName))
            .ForMember(dest => dest.ThresholdType, opt => opt.MapFrom(src => src.ThresholdType.ReadableName))
            .ReverseMap()
            .ForMember(dest => dest.Nutrient,
                opt => opt.MapFrom(src => IEnum<Nutrients, NutrientToken>.ToValue(src.Nutrient)))
            .ForMember(dest => dest.Unit,
                opt => opt.MapFrom(src => IEnum<Units, UnitToken>.ToValue(src.Unit)))
            .ForMember(dest => dest.ThresholdType,
                opt => opt.MapFrom(src => IEnum<ThresholdTypes, ThresholdToken>.ToValue(src.ThresholdType)));

        // Meal Plan
        CreateMap<MenuRecipe, MenuRecipeDto>()
            .ForMember(dest => dest.Recipe, opt => opt.MapFrom(src => src.Recipe))
            .ReverseMap()
            .ForMember(dest => dest.Recipe, opt => opt.MapFrom(src => src.Recipe));

        CreateMap<DailyMenu, DailyMenuDto>()
            .ForMember(dest => dest.MealType, opt => opt.MapFrom(src => src.MealType.ReadableName))
            .ForMember(dest => dest.Nutrients, opt => opt.MapFrom(src => src.NutritionalValues))
            .ForMember(dest => dest.Targets, opt => opt.MapFrom(src => src.NutritionalTargets))
            .ForMember(dest => dest.Recipes, opt => opt.MapFrom(src => src.MenuRecipes))
            .ReverseMap()
            .ForMember(dest => dest.MealType,
                opt => opt.MapFrom(src => IEnum<MealTypes, MealToken>.ToValue(src.MealType)))
            .ForMember(dest => dest.NutritionalValues, opt => opt.MapFrom(src => src.Nutrients))
            .ForMember(dest => dest.NutritionalTargets, opt => opt.MapFrom(src => src.Targets))
            .ForMember(dest => dest.MenuRecipes, opt => opt.MapFrom(src => src.Recipes));

        CreateMap<DailyPlan, DailyPlanDto>()
            .ForMember(dest => dest.Days, opt => opt.MapFrom(src => src.Days.Select(e => e.ReadableName).ToList()))
            .ForMember(dest => dest.PhysicalActivityLevel,
                opt => opt.MapFrom(src => src.PhysicalActivityLevel.ReadableName))
            .ForMember(dest => dest.Nutrients, opt => opt.MapFrom(src => src.NutritionalValues))
            .ForMember(dest => dest.Targets, opt => opt.MapFrom(src => src.NutritionalTargets))
            .ForMember(dest => dest.Menus, opt => opt.MapFrom(src => src.DailyMenus))
            .ReverseMap()
            .ForMember(dest => dest.Days,
                opt => opt.MapFrom(src => src.Days.Select(IEnum<Days, DayToken>.ToValue).ToList()))
            .ForMember(dest => dest.PhysicalActivityLevel,
                opt => opt.MapFrom(src =>
                    IEnum<PhysicalActivities, PhysicalActivityToken>.ToValue(src.PhysicalActivityLevel)))
            .ForMember(dest => dest.NutritionalValues, opt => opt.MapFrom(src => src.Nutrients))
            .ForMember(dest => dest.NutritionalTargets, opt => opt.MapFrom(src => src.Targets))
            .ForMember(dest => dest.DailyMenus, opt => opt.MapFrom(src => src.Menus));

        // Clinical Anamnesis
        CreateMap<Ingestible, IngestibleDto>()
            .ForMember(e => e.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(e => e.Type, opt => opt.MapFrom(src => src.Type.ReadableName))
            .ForMember(e => e.Adherence, opt => opt.MapFrom(src => src.Adherence.ReadableName))
            .ForMember(e => e.Unit, opt => opt.MapFrom(src => src.Unit == null ? null : src.Unit.ReadableName))
            .ReverseMap()
            .ForMember(e => e.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Type,
                opt => opt.MapFrom(src => IEnum<IngestibleTypes, IngestibleToken>.ToValue(src.Type)))
            .ForMember(dest => dest.Adherence,
                opt => opt.MapFrom(src => IEnum<Frequencies, FrequencyToken>.ToValue(src.Adherence)))
            .ForMember(e => e.Unit,
                opt => opt.MapFrom(src => IEnum<Units, UnitToken>.ToValue(src.Unit ?? string.Empty)));

        CreateMap<Disease, DiseaseDto>()
            .ForMember(e => e.InheritanceTypes,
                opt => opt.MapFrom(src => src.InheritanceTypes.Select(e => e.ReadableName).ToList()))
            .ReverseMap()
            .ForMember(dest => dest.InheritanceTypes,
                opt => opt.MapFrom(
                    src => src.InheritanceTypes.Select(IEnum<InheritanceTypes, InheritanceToken>.ToValue)));

        CreateMap<ClinicalSign, ClinicalSignDto>()
            .ReverseMap();

        CreateMap<ClinicalAnamnesis, ClinicalAnamnesisDto>()
            .ReverseMap();

        // Nutritional Anamnesis
        CreateMap<HarmfulHabit, HarmfulHabitDto>()
            .ReverseMap();

        CreateMap<EatingSymptom, EatingSymptomDto>()
            .ReverseMap();

        CreateMap<AdverseFoodReaction, AdverseFoodReactionDto>()
            .ForMember(dest => dest.FoodGroup, opt => opt.MapFrom(src => src.FoodGroup.ReadableName))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ReadableName))
            .ReverseMap()
            .ForMember(dest => dest.FoodGroup,
                opt => opt.MapFrom(src => IEnum<FoodGroups, FoodGroupToken>.ToValue(src.FoodGroup)))
            .ForMember(dest => dest.Type,
                opt => opt.MapFrom(src => IEnum<FoodReactions, FoodReactionToken>.ToValue(src.Type)));

        CreateMap<FoodConsumption, FoodConsumptionDto>()
            .ForMember(dest => dest.FoodGroup, opt => opt.MapFrom(src => src.FoodGroup.ReadableName))
            .ForMember(dest => dest.Frequency, opt => opt.MapFrom(src => src.Frequency.ReadableName))
            .ReverseMap()
            .ForMember(dest => dest.FoodGroup,
                opt => opt.MapFrom(src => IEnum<FoodGroups, FoodGroupToken>.ToValue(src.FoodGroup)))
            .ForMember(dest => dest.Frequency,
                opt => opt.MapFrom(src => IEnum<Frequencies, FrequencyToken>.ToValue(src.Frequency)));

        CreateMap<NutritionalAnamnesis, NutritionalAnamnesisDto>()
            .ReverseMap();

        // Anthropometry
        CreateMap<Anthropometry, AnthropometryDto>()
            .ReverseMap();

        // Consultation
        CreateMap<Consultation, ConsultationDto>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ReadableName))
            .ForMember(dest => dest.Purpose, opt => opt.MapFrom(src => src.Purpose.ReadableName))
            .ForMember(dest => dest.RegisteredOn,
                opt => opt.MapFrom(src =>
                    src.RegisteredOn.HasValue ? src.RegisteredOn.Value.ToString("yyyy-MM-dd") : null))
            .ReverseMap()
            .ForMember(dest => dest.PatientId, opt => opt.Ignore())
            .ForMember(dest => dest.Type,
                opt => opt.MapFrom(src => IEnum<ConsultationTypes, ConsultationTypeToken>.ToValue(src.Type)))
            .ForMember(dest => dest.Purpose,
                opt => opt.MapFrom(src => IEnum<ConsultationPurposes, ConsultationPurposeToken>.ToValue(src.Purpose)))
            .ForMember(dest => dest.RegisteredOn, opt => opt.Ignore());

        // Patient
        CreateMap<PersonalInfo, PersonalInfoDto>()
            .ForMember(dest => dest.BiologicalSex, opt => opt.MapFrom(src => src.BiologicalSex.ReadableName))
            .ForMember(dest => dest.Birthdate, opt => opt.MapFrom(src => src.Birthdate.ToString("yyyy-MM-dd")))
            .ReverseMap()
            .ForMember(dest => dest.BiologicalSex,
                opt => opt.MapFrom(src => IEnum<BiologicalSexes, BiologicalSexToken>.ToValue(src.BiologicalSex)))
            .ForMember(dest => dest.Birthdate,
                opt => opt.MapFrom(
                    src => DateOnly.ParseExact(src.Birthdate, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                        DateTimeStyles.None)));

        CreateMap<ContactInfo, ContactInfoDto>()
            .ReverseMap();

        CreateMap<Address, AddressDto>()
            .ForMember(dest => dest.Province, opt => opt.MapFrom(src => src.Province.ReadableName))
            .ReverseMap()
            .ForMember(dest => dest.Province,
                opt => opt.MapFrom(src => IEnum<Provinces, ProvinceToken>.ToValue(src.Province)));

        CreateMap<Patient, PatientDto>()
            .ReverseMap();

        // Nutritionist
        CreateMap<Nutritionist, NutritionistDto>()
            .ReverseMap();

        CreateMap<MinimalDailyMenu, DailyMenu>()
            .ForMember(dest => dest.MealType,
                opt => opt.MapFrom(src => IEnum<MealTypes, MealToken>.ToValue(src.MealType)))
            .ForMember(dest => dest.NutritionalValues, opt => opt.MapFrom(src => src.Nutrients))
            .ForMember(dest => dest.NutritionalTargets, opt => opt.MapFrom(src => src.Targets))
            .ForMember(dest => dest.MenuRecipes, opt => opt.MapFrom(src => src.Recipes.Select(e => new MenuRecipe
            {
                RecipeId = e.RecipeId,
                Portions = e.Portions,
            })));

        CreateMap<MinimalDailyPlan, DailyPlan>()
            .ForMember(dest => dest.Days,
                opt => opt.MapFrom(src => src.Days.Select(IEnum<Days, DayToken>.ToValue).ToList()))
            .ForMember(dest => dest.PhysicalActivityLevel,
                opt => opt.MapFrom(src =>
                    IEnum<PhysicalActivities, PhysicalActivityToken>.ToValue(src.PhysicalActivityLevel)))
            .ForMember(dest => dest.NutritionalValues, opt => opt.MapFrom(src => src.Nutrients))
            .ForMember(dest => dest.NutritionalTargets, opt => opt.MapFrom(src => src.Targets))
            .ForMember(dest => dest.DailyMenus, opt => opt.MapFrom(src => src.Menus));

        CreateMap<MinimalRecipe, Recipe>()
            .ForMember(dest => dest.Difficulty,
                opt => opt.MapFrom(src => IEnum<Difficulties, DifficultyToken>.ToValue(src.Difficulty ?? string.Empty)))
            .ForMember(dest => dest.MealTypes,
                opt => opt.MapFrom(src => src.MealTypes.Select(IEnum<MealTypes, MealToken>.ToValue)))
            .ForMember(dest => dest.DishTypes,
                opt => opt.MapFrom(src => src.DishTypes.Select(IEnum<DishTypes, DishToken>.ToValue)))
            .ForMember(dest => dest.RecipeSteps, opt => opt.MapFrom(src => src.Steps.Select((e, i) => new RecipeStep
            {
                Number = i + 1,
                Description = e
            })));
    }
}