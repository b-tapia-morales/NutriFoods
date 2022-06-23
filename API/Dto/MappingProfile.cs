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
            .ForMember(e => e.Essentiality, 
                opt => opt.MapFrom(src => src.Essentiality.NameDisplay));
        CreateMap<PrimaryGroup, PrimaryGroupDto>();
        CreateMap<SecondaryGroup, SecondaryGroupDto>();
        CreateMap<TertiaryGroup, TertiaryGroupDto>();
    }
}