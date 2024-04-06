// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using System.ComponentModel.DataAnnotations;
using API.Dto;
using API.Dto.Insertion;
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
    [Route("name/{name:length(2)}")]
    public async Task<ActionResult<IngredientDto>> FindByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return new BadRequestObjectResult("Parameter can't be an empty or whitespace string");

        var ingredient = await _repository.FindByName(name);
        return ingredient == null ? new NotFoundResult() : ingredient;
    }

    [HttpGet]
    [Route("id/{id:int:min(1)}")]
    public async Task<ActionResult<IngredientDto>> FindById(int id)
    {
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

    [HttpPut]
    [Route("synonyms")]
    public async Task<ActionResult<IngredientDto>> InsertSynonyms([FromBody] SynonymInsertion insertion)
    {
        var ingredient = await _repository.FindByName(insertion.Ingredient);
        if (ingredient == null)
            return new NotFoundObjectResult($"No ingredient with the given name “{insertion.Ingredient}” was found");
        return await _repository.InsertSynonyms(ingredient, insertion);
    }

    [HttpPut]
    [Route("synonyms/batch-update")]
    public async IAsyncEnumerable<IngredientDto> InsertMeasures([FromBody] List<SynonymInsertion> insertions)
    {
        await foreach (var insertion in _repository.InsertSynonyms(insertions))
            yield return insertion;
    }

    [HttpPut]
    [Route("measures")]
    public async Task<ActionResult<IngredientDto>> InsertMeasures([FromBody] MeasureInsertion insertion)
    {
        var ingredient = await _repository.FindByName(insertion.Ingredient);
        if (ingredient == null)
            return new NotFoundObjectResult($"No ingredient with the given name “{insertion.Ingredient}” was found");
        return await _repository.InsertMeasures(ingredient, insertion);
    }

    [HttpPut]
    [Route("measures/batch-update")]
    public async IAsyncEnumerable<IngredientDto> InsertMeasures([FromBody] List<MeasureInsertion> insertions)
    {
        await foreach (var insertion in _repository.InsertMeasures(insertions))
            yield return insertion;
    }

    [HttpPost]
    [Route("")]
    public async Task<ActionResult<IngredientDto>> InsertIngredient(
        [FromBody] MinimalIngredient insertion)
    {
        if (await _repository.FindByName(insertion.Name) is not null)
            return new ConflictObjectResult($"An ingredient with the same name (“{insertion.Name}”) already exists");
        return await _repository.InsertIngredient(insertion);
    }

    [HttpPost]
    [Route("batch-insert")]
    public async Task<ActionResult<IEnumerable<IngredientDto>>> InsertIngredients(
        [FromBody] List<MinimalIngredient> insertions) => await _repository.InsertIngredients(insertions);
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