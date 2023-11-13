using API.Dto;
using Domain.Enum;
using Microsoft.AspNetCore.Mvc;

namespace API.Ingredients;

[ApiController]
[Route("api/v1/ingredients")]
public class IngredientController
{
    private readonly IIngredientRepository _repository;

    public IngredientController(IIngredientRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [Route("")]
    public async Task<ActionResult<IEnumerable<IngredientDto>>> FindAll()
    {
        return await _repository.FindAll();
    }

    [HttpGet]
    [Route("name/{name}")]
    public async Task<ActionResult<IngredientDto>> FindByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return new BadRequestObjectResult("Parameter can't be an empty or whitespace string");

        var ingredient = await _repository.FindByName(name.ToLower());
        return ingredient == null ? new NotFoundResult() : ingredient;
    }

    [HttpGet]
    [Route("id/{id:int}")]
    public async Task<ActionResult<IngredientDto>> FindById(int id)
    {
        if (id < 0)
            return new BadRequestObjectResult($"Parameter can't be a negative integer (Value provided was: {id})");

        var ingredient = await _repository.FindById(id);
        return ingredient == null ? new NotFoundResult() : ingredient;
    }

    [HttpGet]
    [Route("primaryGroup/{foodGroup}")]
    public async Task<ActionResult<IEnumerable<IngredientDto>>> FindByFoodGroup(FoodGroupToken foodGroup)
    {
        var value = IEnum<FoodGroups, FoodGroupToken>.FromToken(foodGroup);
        return await (value == FoodGroups.None ? _repository.FindAll() : _repository.FindByFoodGroup(value));
    }
}