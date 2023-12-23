// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable ClassNeverInstantiated.Global

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

    [HttpPost]
    [Route("synonyms")]
    public async Task<ActionResult<IngredientDto>> InsertSynonyms([FromBody] SynonymInsertion insertion)
    {
        var ingredient = await _repository.FindByName(insertion.Ingredient);
        if (ingredient == null)
            return new NotFoundResult();
        return await _repository.InsertSynonyms(ingredient, insertion);
    }

    [HttpPost]
    [Route("measures")]
    public async Task<ActionResult<IngredientDto>> InsertMeasures([FromBody] MeasureInsertion insertion)
    {
        var ingredient = await _repository.FindByName(insertion.Ingredient);
        if (ingredient == null)
            return new NotFoundResult();
        return await _repository.InsertMeasures(ingredient, insertion);
    }
}

public class SynonymInsertion
{
    public string Ingredient { get; set; } = null!;
    public IList<string> Synonyms { get; set; } = null!;
}

public class MeasureInsertion
{
    public string Ingredient { get; set; } = null!;
    public IList<Measure> Measures { get; set; } = null!;
}

public class Measure
{
    public string Name { get; set; } = null!;
    public double Grams { get; set; }
}