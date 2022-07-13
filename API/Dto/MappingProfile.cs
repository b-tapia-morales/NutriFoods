using API.Dto.Abridged;
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
            .ForMember(dest => dest.Essentiality, opt => opt.MapFrom(src => src.Essentiality.NameDisplay));
        CreateMap<PrimaryGroup, PrimaryGroupDto>();
        CreateMap<SecondaryGroup, SecondaryGroupDto>();
        CreateMap<TertiaryGroup, TertiaryGroupDto>();
        CreateMap<IngredientNutrient, IngredientNutrientDto>()
            .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit.NameDisplay));
        CreateMap<IngredientMeasure, IngredientMeasureDto>();
        CreateMap<IngredientMeasure, IngredientMeasureAbridged>();
        CreateMap<Ingredient, IngredientDto>();
        CreateMap<Ingredient, IngredientAbridged>();
        CreateMap<RecipeMeasure, RecipeMeasureDto>();
        CreateMap<RecipeQuantity, RecipeQuantityDto>();
        CreateMap<RecipeNutrient, RecipeNutrientDto>()
            .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit.NameDisplay));
        CreateMap<RecipeStep, RecipeStepDto>();
        CreateMap<Recipe, RecipeDto>();
        CreateMap<MealMenuRecipe, MealMenuRecipeDto>();
        CreateMap<MealMenu, MealMenuDto>()
            .ForMember(dest => dest.Recipes, opt => opt.MapFrom(src => src.MealMenuRecipes))
            .ForMember(dest => dest.Satiety, opt => opt.MapFrom(src => src.Satiety.NameDisplay));
        CreateMap<MealPlan, MealPlanDto>();
    }
}