using API.Dto;
using AutoMapper;

namespace API.Nutritionists;

public class NutritionistRepository: INutritionistRepository
{
    private readonly IMapper _mapper;

    public NutritionistRepository(IMapper mapper) => _mapper = mapper;
    
    public async Task<NutritionistDto?> Find(string email, string password)
    {
        throw new NotImplementedException();
    }
}