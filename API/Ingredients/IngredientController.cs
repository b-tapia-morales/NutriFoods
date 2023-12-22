using System.ComponentModel.DataAnnotations;
using API.Dto;
using Domain.Enum;
using Microsoft.AspNetCore.Mvc;

namespace API.Ingredients;

[ApiController]
[Route("api/v1/ingredients")]
public class IngredientController
{
    private const int DefaultPageSize = 20;

    private readonly IIngredientRepository _repository;

    public IngredientController(IIngredientRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [Route("")]
    public async Task<ActionResult<IEnumerable<IngredientDto>>> FindAll(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = DefaultPageSize)
    {
        return await _repository.FindAll(pageNumber, pageSize);
    }

    [HttpGet]
    [Route("order-by")]
    public async Task<ActionResult<IEnumerable<IngredientDto>>> FindOrderedBy(
        [FromQuery, Required] NutrientToken nutrient,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = DefaultPageSize,
        [FromQuery] bool descending = true) =>
        await _repository.FindOrderedBy(IEnum<Nutrients, NutrientToken>.ToValue(nutrient), pageNumber, pageSize,
            descending);

    [HttpGet]
    [Route("name/{name}")]
    public async Task<ActionResult<IngredientDto>> FindByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return new BadRequestObjectResult("Parameter can't be an empty or whitespace string");

        var ingredient = await _repository.FindByName(name);
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
    public async Task<ActionResult<IEnumerable<IngredientDto>>> FindByFoodGroup(
        FoodGroupToken foodGroup,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = DefaultPageSize)
    {
        var value = IEnum<FoodGroups, FoodGroupToken>.ToValue(foodGroup);
        return await (value == FoodGroups.None
            ? _repository.FindAll(pageNumber, pageSize)
            : _repository.FindByFoodGroup(value, pageNumber, pageSize));
    }
}