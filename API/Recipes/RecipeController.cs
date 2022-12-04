using API.Dto;
using Microsoft.AspNetCore.Mvc;
using Utils.Enum;

namespace API.Recipes;

[ApiController]
[Route("api/v1/recipes")]
public class RecipeController
{
    private readonly IRecipeRepository _repository;

    public RecipeController(IRecipeRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [Route("")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> FindAll()
    {
        return await _repository.FindAll();
    }

    [HttpGet]
    [Route("name/{name}")]
    public async Task<ActionResult<RecipeDto>> FindByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return new BadRequestObjectResult("Parameter can't be an empty or whitespace string");

        try
        {
            return await _repository.FindByName(name.ToLower());
        }
        catch (InvalidOperationException)
        {
            return new RecipeDto();
        }
    }

    [HttpGet]
    [Route("id/{id:int}")]
    public async Task<ActionResult<RecipeDto>> FindById(int id)
    {
        if (id < 0)
            return new BadRequestObjectResult($"Parameter can't be a negative integer (Value provided was: {id})");

        try
        {
            return await _repository.FindById(id);
        }
        catch (InvalidOperationException)
        {
            return new RecipeDto();
        }
    }

    [HttpGet]
    [Route("by-meal-type")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> FindByMealType(MealType mealType)
    {
        return await (mealType == MealType.None ? _repository.FindAll() : _repository.FindByMealType(mealType));
    }

    [HttpGet]
    [Route("by-dish-type")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> FindByDishType(DishType dishType)
    {
        return await (dishType == DishType.None ? _repository.FindAll() : _repository.FindByDishType(dishType));
    }

    [HttpGet]
    [Route("exclude-by-id")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> FindExcludeById([FromQuery] IList<int> ids)
    {
        return await _repository.FindExcludeById(ids);
    }

    [HttpGet]
    [Route("vegetarian")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> GetVegetarianRecipes()
    {
        return await _repository.GetVegetarianRecipes();
    }

    [HttpGet]
    [Route("ovo-vegetarian")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> GetOvoVegetarianRecipes()
    {
        return await _repository.GetOvoVegetarianRecipes();
    }

    [HttpGet]
    [Route("lacto-vegetarian")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> GetLactoVegetarianRecipes()
    {
        return await _repository.GetLactoVegetarianRecipes();
    }

    [HttpGet]
    [Route("ovo-lacto-vegetarian")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> GetOvoLactoVegetarianRecipes()
    {
        return await _repository.GetOvoLactoVegetarianRecipes();
    }

    [HttpGet]
    [Route("pollotarian")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> GetPollotarianRecipes()
    {
        return await _repository.GetPollotarianRecipes();
    }

    [HttpGet]
    [Route("pescetarian")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> GetPescetarianRecipes()
    {
        return await _repository.GetPescetarianRecipes();
    }

    [HttpGet]
    [Route("pollo-pescetarian")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> GetPolloPescetarianRecipes()
    {
        return await _repository.GetPolloPescetarianRecipes();
    }

    [HttpGet]
    [Route("preparationTime")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> FilterByPreparationTime([FromQuery] int lower,
        [FromQuery] int upper)
    {
        if (lower < 0 || upper < 0)
            return new BadRequestObjectResult(
                $"Neither value can be a negative integer (Values provided were: minimum prep time = {lower}, maximum prep time = {upper})");

        if (lower > upper)
            return new BadRequestObjectResult(
                $"Maximum prep time must be lower or equal to minimum prep time (Values provided were {lower} and {upper} respectively)");

        return await _repository.FilterByPreparationTime(lower, upper);
    }

    [HttpGet]
    [Route("portions/{portions:int}")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> FilterByPortions(int portions)
    {
        if (portions < 0)
            return new BadRequestObjectResult(
                $"Parameter can't be a negative integer (Value provided was: {portions})");

        return await _repository.FilterByPortions(portions);
    }

    [HttpGet]
    [Route("portions")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> FilterByPortions([FromQuery] int lower,
        [FromQuery] int upper)
    {
        if (lower < 0 || upper < 0)
            return new BadRequestObjectResult(
                $"Neither value can be a negative integer (Values provided were: minimum portions = {lower}, maximum portions = {upper})");

        if (lower > upper)
            return new BadRequestObjectResult(
                $"Maximum portions must be lower or equal to minimum portions (Values provided were {lower} and {upper} respectively)");

        Console.WriteLine($"{lower} - {upper}");
        return await _repository.FilterByPortions(lower, upper);
    }

    [HttpGet]
    [Route("energy")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> FilterByEnergy([FromQuery] int lower, [FromQuery] int upper)
    {
        if (lower < 0 || upper < 0)
            return new BadRequestObjectResult(
                $"Neither value can be a negative integer (Values provided were: minimum energy = {lower}, maximum energy = {upper})");

        if (lower > upper)
            return new BadRequestObjectResult(
                $"Maximum energy must be lower or equal to minimum energy (Values provided were {lower} and {upper} respectively)");

        return await _repository.FilterByEnergy(lower, upper);
    }

    [HttpGet]
    [Route("carbohydrates")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> FilterByCarbohydrates([FromQuery] int lower,
        [FromQuery] int upper)
    {
        if (lower < 0 || upper < 0)
            return new BadRequestObjectResult(
                $"Neither value can be a negative integer (Values provided were: minimum carbohydrates = {lower}, maximum carbohydrates = {upper})");

        if (lower > upper)
            return new BadRequestObjectResult(
                $"Maximum carbohydrates must be lower or equal to minimum carbohydrates (Values provided were {lower} and {upper} respectively)");

        return await _repository.FilterByCarbohydrates(lower, upper);
    }

    [HttpGet]
    [Route("lipids")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> FilterByLipids([FromQuery] int lower, [FromQuery] int upper)
    {
        if (lower < 0 || upper < 0)
            return new BadRequestObjectResult(
                $"Neither value can be a negative integer (Values provided were: minimum lipids = {lower}, maximum lipids = {upper})");

        if (lower > upper)
            return new BadRequestObjectResult(
                $"Maximum lipids must be lower or equal to minimum lipids (Values provided were {lower} and {upper} respectively)");

        return await _repository.FilterByLipids(lower, upper);
    }

    [HttpGet]
    [Route("proteins")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> FilterByProteins([FromQuery] int lower,
        [FromQuery] int upper)
    {
        if (lower < 0 || upper < 0)
            return new BadRequestObjectResult(
                $"Neither value can be a negative integer (Values provided were: minimum proteins = {lower}, maximum proteins = {upper})");

        if (lower > upper)
            return new BadRequestObjectResult(
                $"Maximum proteins must be lower or equal to minimum proteins (Values provided were {lower} and {upper} respectively)");

        return await _repository.FilterByProteins(lower, upper);
    }
}