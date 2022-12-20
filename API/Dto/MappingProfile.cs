using API.Dto.Abridged;
using AutoMapper;
using Domain.Models;

namespace API.Dto;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<NutrientType, NutrientTypeDto>()
            .ReverseMap();
        CreateMap<NutrientSubtype, NutrientSubtypeDto>()
            .ReverseMap();
        CreateMap<Nutrient, NutrientDto>()
            .ForMember(dest => dest.Essentiality, opt => opt.MapFrom(src => src.Essentiality.ReadableName))
            .ReverseMap();

        CreateMap<PrimaryGroup, PrimaryGroupDto>()
            .ReverseMap();
        CreateMap<SecondaryGroup, SecondaryGroupDto>()
            .ReverseMap();
        CreateMap<TertiaryGroup, TertiaryGroupDto>()
            .ReverseMap();

        CreateMap<IngredientNutrient, IngredientNutrientDto>()
            .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit.ReadableName))
            .ReverseMap();
        CreateMap<IngredientMeasure, IngredientMeasureDto>()
            .ReverseMap();
        CreateMap<IngredientMeasure, IngredientMeasureAbridged>()
            .ReverseMap();
        CreateMap<Ingredient, IngredientDto>()
            .ForMember(dest => dest.Synonyms, opt => opt.MapFrom(src => src.IngredientSynonyms.Select(e => e.Name)))
            .ForMember(dest => dest.Measures, opt => opt.MapFrom(src => src.IngredientMeasures))
            .ForMember(dest => dest.Nutrients, opt => opt.MapFrom(src => src.IngredientNutrients))
            .ReverseMap();
        CreateMap<Ingredient, IngredientAbridged>()
            .ReverseMap();

        CreateMap<RecipeMeasure, RecipeMeasureDto>()
            .ReverseMap();
        CreateMap<RecipeQuantity, RecipeQuantityDto>()
            .ReverseMap();
        CreateMap<RecipeNutrient, RecipeNutrientDto>()
            .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit.ReadableName))
            .ReverseMap();
        CreateMap<RecipeStep, RecipeStepDto>()
            .ReverseMap();
        CreateMap<Recipe, RecipeDto>()
            .ForMember(dest => dest.Measures, opt => opt.MapFrom(src => src.RecipeMeasures))
            .ForMember(dest => dest.Quantities, opt => opt.MapFrom(src => src.RecipeQuantities))
            .ForMember(dest => dest.Steps, opt => opt.MapFrom(src => src.RecipeSteps))
            .ForMember(dest => dest.Nutrients, opt => opt.MapFrom(src => src.RecipeNutrients))
            .ForMember(dest => dest.MealTypes,
                opt => opt.MapFrom(src => src.RecipeMealTypes.Select(e => e.MealType.ReadableName)))
            .ForMember(dest => dest.DishTypes,
                opt => opt.MapFrom(src => src.RecipeDishTypes.Select(e => e.DishType.ReadableName)))
            .ForMember(dest => dest.Diets,
                opt => opt.MapFrom(src => src.RecipeDiets.Select(e => e.Diet.ReadableName)))
            .ReverseMap();


        CreateMap<MenuRecipe, MenuRecipeDto>()
            .ReverseMap();
        CreateMap<DailyMenuNutrient, DailyMenuNutrientDto>()
            .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit.ReadableName))
            .ReverseMap();
        CreateMap<DailyMenu, DailyMenuDto>()
            .ForMember(dest => dest.MealType, opt => opt.MapFrom(src => src.MealType.ReadableName))
            .ForMember(dest => dest.Satiety, opt => opt.MapFrom(src => src.Satiety.ReadableName))
            .ForMember(dest => dest.Nutrients, opt => opt.MapFrom(src => src.DailyMenuNutrients))
            .ReverseMap();
        CreateMap<DailyMealPlanNutrient, DailyMealPlanNutrientDto>()
            .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit.ReadableName))
            .ReverseMap();
        CreateMap<DailyMealPlan, DailyMealPlanDto>()
            .ForMember(dest => dest.DayOfTheWeek, opt => opt.MapFrom(src => src.DayOfTheWeek.ReadableName))
            .ForMember(dest => dest.Nutrients, opt => opt.MapFrom(src => src.DailyMealPlanNutrients))
            .ReverseMap();
        CreateMap<MealPlan, MealPlanDto>()
            .ReverseMap();

        CreateMap<UserBodyMetric, UserBodyMetricDto>()
            .ForMember(dest => dest.PhysicalActivity, opt => opt.MapFrom(src => src.PhysicalActivity.ReadableName))
            .ReverseMap();
        CreateMap<UserDatum, UserDataDto>()
            .ForMember(dest => dest.Birthdate, opt => opt.MapFrom(src => src.Birthdate.ToString()))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ReadableName))
            .ForMember(dest => dest.Diet, opt => opt.MapFrom(src => src.Diet!.ReadableName))
            .ForMember(dest => dest.IntendedUse, opt => opt.MapFrom(src => src.IntendedUse!.ReadableName))
            .ForMember(dest => dest.UpdateFrequency, opt => opt.MapFrom(src => src.UpdateFrequency!.ReadableName))
            .ReverseMap();
        CreateMap<UserProfile, UserDto>()
            .ForMember(dest => dest.UserData, opt => opt.MapFrom(src => src.UserDatum))
            .ReverseMap();
    }
}