using System.Globalization;
using API.Dto.Abridged;
using API.Genetic;
using AutoMapper;
using Domain.Models;

namespace API.Dto;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Diet, DietDto>();
        CreateMap<DishType, DishTypeDto>();
        CreateMap<MealType, MealTypeDto>();
        CreateMap<NutrientType, NutrientTypeDto>();
        CreateMap<NutrientSubtype, NutrientSubtypeDto>();
        CreateMap<Nutrient, NutrientDto>()
            .ForMember(dest => dest.Essentiality, opt => opt.MapFrom(src => src.Essentiality.ReadableName));
        CreateMap<PrimaryGroup, PrimaryGroupDto>();
        CreateMap<SecondaryGroup, SecondaryGroupDto>();
        CreateMap<TertiaryGroup, TertiaryGroupDto>();
        CreateMap<IngredientNutrient, IngredientNutrientDto>()
            .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit.ReadableName));
        CreateMap<IngredientMeasure, IngredientMeasureDto>();
        CreateMap<IngredientMeasure, IngredientMeasureAbridged>();
        CreateMap<Ingredient, IngredientDto>();
        CreateMap<Ingredient, IngredientAbridged>();
        CreateMap<RecipeMeasure, RecipeMeasureDto>();
        CreateMap<RecipeQuantity, RecipeQuantityDto>();
        CreateMap<RecipeNutrient, RecipeNutrientDto>()
            .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit.ReadableName));
        CreateMap<RecipeStep, RecipeStepDto>();
        CreateMap<Recipe, RecipeDto>();
        CreateMap<MealMenuRecipe, MealMenuRecipeDto>();
        CreateMap<MealMenu, MealMenuDto>()
            .ForMember(dest => dest.MenuRecipes, opt => opt.MapFrom(src => src.MealMenuRecipes))
            .ForMember(dest => dest.Satiety, opt => opt.MapFrom(src => src.Satiety.ReadableName));
        CreateMap<MealPlan, MealPlanDto>();
        CreateMap<UserBodyMetric, UserBodyMetricDto>()
            .ForMember(dest => dest.PhysicalActivityLevel,
                opt => opt.MapFrom(src => src.PhysicalActivityLevel.ReadableName));
        CreateMap<UserProfile, UserDto>()
            .ForMember(dest => dest.Birthdate, opt => opt.MapFrom(src => src.Birthdate.ToString()))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ReadableName))
            .ForMember(dest => dest.UpdateFrequency, opt => opt.MapFrom(src => src.UpdateFrequency!.ReadableName))
            .ForMember(dest => dest.BodyMetrics, opt => opt.MapFrom(src => src.UserBodyMetrics));
    }
}