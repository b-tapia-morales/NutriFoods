using System.ComponentModel.DataAnnotations;
using API.Dto;
using API.Dto.Insertion;
using Domain.Enum;
using Microsoft.AspNetCore.Mvc;

namespace API.Recipes;

[ApiController]
[Route("api/v1/recipes")]
public class RecipeController
{
    private const int DefaultPageSize = 20;

    private readonly IRecipeRepository _repository;

    public RecipeController(IRecipeRepository repository) => _repository = repository;

    [HttpGet]
    [Route("")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> FindAll(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = DefaultPageSize) =>
        await _repository.FindAll(pageNumber, pageSize);

    [HttpGet]
    [Route("order-by")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> FindOrderedBy(
        [FromQuery, Required] NutrientToken nutrient,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = DefaultPageSize,
        [FromQuery] bool descending = true) =>
        await _repository.FindOrderedBy(IEnum<Nutrients, NutrientToken>.ToValue(nutrient), pageNumber, pageSize,
            descending);

    [HttpGet]
    [Route("name/{name:minlength(2)}/author/{author:minlength(2)}")]
    public async Task<ActionResult<RecipeDto>> FindByNameAndAuthor([Required] string name, [Required] string author)
    {
        var recipe = await _repository.FindByNameAndAuthor(name, author);
        return recipe == null ? new NotFoundResult() : recipe;
    }

    [HttpGet]
    [Route("id/{id:int:min(1)}")]
    public async Task<ActionResult<RecipeDto>> FindById([Required] int id)
    {
        var recipe = await _repository.FindById(id);
        return recipe == null ? new NotFoundResult() : recipe;
    }

    [HttpGet]
    [Route("meal-type/{mealType}")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> FindByMealType(
        [Required] MealToken mealType,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = DefaultPageSize)
    {
        var value = IEnum<MealTypes, MealToken>.ToValue(mealType);
        return await (value == MealTypes.None
            ? _repository.FindAll(pageNumber, pageSize)
            : _repository.FindByMealType(value, pageNumber, pageSize));
    }

    [HttpGet]
    [Route("dish-type/{dishType}")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> FindByDishType(
        [Required] DishToken dishType,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = DefaultPageSize)
    {
        var value = IEnum<DishTypes, DishToken>.ToValue(dishType);
        return await (value == DishTypes.None
            ? _repository.FindAll(pageNumber, pageSize)
            : _repository.FindByDishType(value, pageNumber, pageSize));
    }

    [HttpGet]
    [Route("diet/{diet}")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> FindVegetarianRecipes(
        [Required] DietToken diet,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = DefaultPageSize)
    {
        var value = IEnum<Diets, DietToken>.ToValue(diet);
        return await (value == Diets.None
            ? _repository.FindAll(pageNumber, pageSize)
            : _repository.GetVegetarianRecipes(value, pageNumber, pageSize));
    }

    [HttpGet]
    [Route("preparationTime")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> FilterByPreparationTime(
        [FromQuery(Name = "gte"), Required] int lower,
        [FromQuery(Name = "lte"), Required] int upper,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = DefaultPageSize)
    {
        if (lower < 0 || upper < 0)
            return new BadRequestObjectResult(
                $"Neither value can be a negative integer (Values provided were: minimum prep time = {lower}, maximum prep time = {upper})");

        if (lower > upper)
            return new BadRequestObjectResult(
                $"Maximum prep time must be lower or equal to minimum prep time (Values provided were {lower} and {upper} respectively)");

        return await _repository.FindByPreparationTime(lower, upper, pageNumber, pageSize);
    }

    [HttpGet]
    [Route("portions/{portions:int:min(1)}")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> FilterByExactPortions(
        [Required] int portions,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = DefaultPageSize) =>
        await _repository.FindByPortions(portions, pageNumber, pageSize);

    [HttpGet]
    [Route("portions")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> FilterByPortions(
        [FromQuery(Name = "gte"), Required] int lower,
        [FromQuery(Name = "lte"), Required] int upper,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = DefaultPageSize)
    {
        if (lower < 0 || upper < 0)
            return new BadRequestObjectResult(
                $"Neither value can be a negative integer (Values provided were: minimum portions = {lower}, maximum portions = {upper})");

        if (lower > upper)
            return new BadRequestObjectResult(
                $"Maximum portions must be lower or equal to minimum portions (Values provided were {lower} and {upper} respectively)");

        return await _repository.FindByPortions(lower, upper, pageNumber, pageSize);
    }

    [HttpGet]
    [Route("energy")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> FilterByEnergy(
        [FromQuery(Name = "gte"), Required] int lower,
        [FromQuery(Name = "lte"), Required] int upper)
    {
        if (lower < 0 || upper < 0)
            return new BadRequestObjectResult(
                $"Neither value can be a negative integer (Values provided were: minimum energy = {lower}, maximum energy = {upper})");

        if (lower > upper)
            return new BadRequestObjectResult(
                $"Maximum energy must be lower or equal to minimum energy (Values provided were {lower} and {upper} respectively)");

        return await _repository.FindByEnergy(lower, upper);
    }

    [HttpGet]
    [Route("carbohydrates")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> FilterByCarbohydrates(
        [FromQuery(Name = "gte"), Required] int lower,
        [FromQuery(Name = "lte"), Required] int upper)
    {
        if (lower < 0 || upper < 0)
            return new BadRequestObjectResult(
                $"Neither value can be a negative integer (Values provided were: minimum carbohydrates = {lower}, maximum carbohydrates = {upper})");

        if (lower > upper)
            return new BadRequestObjectResult(
                $"Maximum carbohydrates must be lower or equal to minimum carbohydrates (Values provided were {lower} and {upper} respectively)");

        return await _repository.FindByCarbohydrates(lower, upper);
    }

    [HttpGet]
    [Route("fatty-acids")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> FilterByLipids(
        [FromQuery(Name = "gte"), Required] int lower,
        [FromQuery(Name = "lte"), Required] int upper)
    {
        if (lower < 0 || upper < 0)
            return new BadRequestObjectResult(
                $"Neither value can be a negative integer (Values provided were: minimum = {lower}, maximum = {upper})");

        if (lower > upper)
            return new BadRequestObjectResult(
                $"Maximum fatty acids must be lower or equal to minimum fatty acids (Values provided were {lower} and {upper} respectively)");

        return await _repository.FindByFattyAcids(lower, upper);
    }

    [HttpGet]
    [Route("proteins")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> FilterByProteins(
        [FromQuery(Name = "gte"), Required] int lower,
        [FromQuery(Name = "lte"), Required] int upper)
    {
        if (lower < 0 || upper < 0)
            return new BadRequestObjectResult(
                $"Neither value can be a negative integer (Values provided were: minimum proteins = {lower}, maximum proteins = {upper})");

        if (lower > upper)
            return new BadRequestObjectResult(
                $"Maximum proteins must be lower or equal to minimum proteins (Values provided were {lower} and {upper} respectively)");

        return await _repository.FindByProteins(lower, upper);
    }

    [HttpPost]
    [Route("")]
    public async Task<ActionResult<RecipeLogging>> InsertRecipe([FromBody] MinimalRecipe minimalRecipe)
    {
        if (await _repository.FindByNameAndAuthor(minimalRecipe.Name, minimalRecipe.Author) is not null)
            return new ConflictObjectResult(
                $"A recipe with the same name and by the same author (“{minimalRecipe.Name}” by “{minimalRecipe.Author}”) already exists");
        if (await _repository.FindByUrl(minimalRecipe.Url) is not null)
            return new ConflictObjectResult(
                $"A recipe with the same url (“{minimalRecipe.Url}”) already exists");

        return await _repository.InsertRecipe(minimalRecipe);
    }

    [HttpPut]
    [Route("")]
    public async Task<ActionResult<IEnumerable<RecipeLogging>>> InsertRecipe(
        [FromBody] List<MinimalRecipe> minimalRecipes)
    {
        return await _repository.InsertRecipes(minimalRecipes);
    }
}