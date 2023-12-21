using API.Dto;
using Domain.Enum;
using Microsoft.AspNetCore.Mvc;

namespace API.Ingredients;

[ApiController]
[Route("api/v1/ingredients")]
public class IngredientController(IIngredientRepository repository)
{
    private const int DefaultPageSize = 20;

    [HttpGet]
    [Route("")]
    public async Task<ActionResult<IEnumerable<IngredientDto>>> FindAll(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = DefaultPageSize)
    {
        return await repository.FindAll(pageNumber, pageSize);
    }

    [HttpGet]
    [Route("name/{name}")]
    public async Task<ActionResult<IngredientDto>> FindByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return new BadRequestObjectResult("Parameter can't be an empty or whitespace string");

        var ingredient = await repository.FindByName(name);
        return ingredient == null ? new NotFoundResult() : ingredient;
    }

    [HttpGet]
    [Route("id/{id:int}")]
    public async Task<ActionResult<IngredientDto>> FindById(int id)
    {
        if (id < 0)
            return new BadRequestObjectResult($"Parameter can't be a negative integer (Value provided was: {id})");

        var ingredient = await repository.FindById(id);
        return ingredient == null ? new NotFoundResult() : ingredient;
    }

    [HttpGet]
    [Route("primaryGroup/{foodGroup}")]
    public async Task<ActionResult<IEnumerable<IngredientDto>>> FindByFoodGroup(
        FoodGroupToken foodGroup,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = DefaultPageSize)
    {
        var value = IEnum<FoodGroups, FoodGroupToken>.ToValue(foodGroup);
        return await (value == FoodGroups.None
            ? repository.FindAll(pageNumber, pageSize)
            : repository.FindByFoodGroup(value, pageNumber, pageSize));
    }
}