using API.Dto.Abridged;
using AutoMapper;
using Domain.Models;

namespace API.Dto;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<NutrientType, NutrientTypeDto>();
        CreateMap<NutrientSubtype, NutrientSubtypeDto>();
        CreateMap<Nutrient, NutrientDto>()
            .ForMember(dest => dest.Essentiality, opt => opt.MapFrom(src => src.EssentialityEnum.ReadableName));

        CreateMap<PrimaryGroup, PrimaryGroupDto>();
        CreateMap<SecondaryGroup, SecondaryGroupDto>();
        CreateMap<TertiaryGroup, TertiaryGroupDto>();

        CreateMap<IngredientNutrient, IngredientNutrientDto>()
            .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.UnitEnum.ReadableName));
        CreateMap<IngredientMeasure, IngredientMeasureDto>();
        CreateMap<IngredientMeasure, IngredientMeasureAbridged>();
        CreateMap<Ingredient, IngredientDto>()
            .ForMember(dest => dest.Synonyms, opt => opt.MapFrom(src => src.IngredientSynonyms.Select(e => e.Name)))
            .ForMember(dest => dest.Measures, opt => opt.MapFrom(src => src.IngredientMeasures))
            .ForMember(dest => dest.Nutrients, opt => opt.MapFrom(src => src.IngredientNutrients));
        CreateMap<Ingredient, IngredientAbridged>();

        CreateMap<RecipeMeasure, RecipeMeasureDto>();
        CreateMap<RecipeQuantity, RecipeQuantityDto>();
        CreateMap<RecipeNutrient, RecipeNutrientDto>()
            .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.UnitEnum.ReadableName));
        CreateMap<RecipeStep, RecipeStepDto>();
        CreateMap<Recipe, RecipeDto>()
            .ForMember(dest => dest.Measures, opt => opt.MapFrom(src => src.RecipeMeasures))
            .ForMember(dest => dest.Quantities, opt => opt.MapFrom(src => src.RecipeQuantities))
            .ForMember(dest => dest.Steps, opt => opt.MapFrom(src => src.RecipeSteps))
            .ForMember(dest => dest.Nutrients, opt => opt.MapFrom(src => src.RecipeNutrients))
            .ForMember(dest => dest.MealTypes,
                opt => opt.MapFrom(src => src.RecipeMealTypes.Select(e => e.MealTypeEnum.ReadableName)))
            .ForMember(dest => dest.DishTypes,
                opt => opt.MapFrom(src => src.RecipeDishTypes.Select(e => e.DishTypeEnum.ReadableName)));

        CreateMap<MenuRecipe, MenuRecipeDto>();
        CreateMap<DailyMenu, DailyMenuDto>()
            .ForMember(dest => dest.MealType, opt => opt.MapFrom(src => src.MealTypeEnum.ReadableName))
            .ForMember(dest => dest.Satiety, opt => opt.MapFrom(src => src.SatietyEnum.ReadableName));
        CreateMap<DailyMealPlan, DailyMealPlanDto>()
            .ForMember(dest => dest.DayOfTheWeek, opt => opt.MapFrom(src => src.DayOfWeekEnum.ReadableName));
        CreateMap<MealPlan, MealPlanDto>();
        
        CreateMap<UserBodyMetric, UserBodyMetricDto>()
            .ForMember(dest => dest.PhysicalActivity,
                opt => opt.MapFrom(src => src.PhysicalActivityEnum.ReadableName));
        CreateMap<UserProfile, UserDto>()
            .ForMember(dest => dest.Birthdate, opt => opt.MapFrom(src => src.Birthdate.ToString()))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ReadableName))
            .ForMember(dest => dest.Diet, opt => opt.MapFrom(src => src.Diet!.ReadableName))
            .ForMember(dest => dest.UpdateFrequency, opt => opt.MapFrom(src => src.UpdateFrequency!.ReadableName))
            .ForMember(dest => dest.BodyMetrics, opt => opt.MapFrom(src => src.UserBodyMetrics));
    }
}