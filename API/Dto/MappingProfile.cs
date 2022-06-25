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
        CreateMap<Ingredient, IngredientDto>();
        CreateMap<RecipeMeasure, RecipeMeasureDto>();
        CreateMap<RecipeQuantity, RecipeQuantityDto>();
        CreateMap<RecipeNutrient, RecipeNutrientDto>()
            .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit.NameDisplay));
        CreateMap<Recipe, RecipeDto>();
    }
}