using API.Dto;
using Microsoft.AspNetCore.Mvc;

namespace API.Recipes;

[ApiController]
[Route("api/v1/recipes")]
public class RecipeController
{
    private readonly IRecipeService _service;

    public RecipeController(IRecipeService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> FindAll()
    {
        return await _service.FindAll();
    }

    [HttpGet]
    [Route("name/{name}")]
    public async Task<ActionResult<RecipeDto>> FindByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return new BadRequestObjectResult("Parameter can't be an empty or whitespace string");

        try
        {
            return await _service.FindByName(name.ToLower());
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
            return await _service.FindById(id);
        }
        catch (InvalidOperationException)
        {
            return new RecipeDto();
        }
    }

    [HttpGet]
    [Route("vegetarian")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> GetVegetarianRecipes()
    {
        return await _service.GetVegetarianRecipes();
    }

    [HttpGet]
    [Route("ovo-vegetarian")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> GetOvoVegetarianRecipes()
    {
        return await _service.GetOvoVegetarianRecipes();
    }

    [HttpGet]
    [Route("lacto-vegetarian")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> GetLactoVegetarianRecipes()
    {
        return await _service.GetLactoVegetarianRecipes();
    }

    [HttpGet]
    [Route("ovo-lacto-vegetarian")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> GetOvoLactoVegetarianRecipes()
    {
        return await _service.GetOvoLactoVegetarianRecipes();
    }

    [HttpGet]
    [Route("pollotarian")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> GetPollotarianRecipes()
    {
        return await _service.GetPollotarianRecipes();
    }

    [HttpGet]
    [Route("pescetarian")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> GetPescetarianRecipes()
    {
        return await _service.GetPescetarianRecipes();
    }

    [HttpGet]
    [Route("pollo-pescetarian")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> GetPolloPescetarianRecipes()
    {
        return await _service.GetPolloPescetarianRecipes();
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

        return await _service.FilterByPreparationTime(lower, upper);
    }

    [HttpGet]
    [Route("portions/{portions:int}")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> FilterByPortions(int portions)
    {
        if (portions < 0)
            return new BadRequestObjectResult(
                $"Parameter can't be a negative integer (Value provided was: {portions})");
        
        return await _service.FilterByPortions(portions);
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
        return await _service.FilterByPortions(lower, upper);
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

        return await _service.FilterByEnergy(lower, upper);
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

        return await _service.FilterByCarbohydrates(lower, upper);
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

        return await _service.FilterByLipids(lower, upper);
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
        
        return await _service.FilterByProteins(lower, upper);
    }
}