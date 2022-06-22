using AutoMapper;

namespace API.Ingredients;

public class IngredientService: IIngredientService
{
    private readonly IIngredientRepository _repository;
    private readonly IMapper _mapper;

    public IngredientService(IIngredientRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
}