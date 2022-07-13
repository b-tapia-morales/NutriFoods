using API.Dto;
using Microsoft.AspNetCore.Mvc;

namespace API.Recipes;

[ApiController]
[Route("api/recipes")]
public class RecipeController
{
    private readonly IRecipeService _service;

    public RecipeController(IRecipeService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("")]
    public async Task<IEnumerable<RecipeDto>> FindAll()
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
    public async Task<IEnumerable<RecipeDto>> GetVegetarianRecipes()
    {
        return await _service.GetVegetarianRecipes();
    }

    [HttpGet]
    [Route("ovo-vegetarian")]
    public async Task<IEnumerable<RecipeDto>> GetOvoVegetarianRecipes()
    {
        return await _service.GetOvoVegetarianRecipes();
    }

    [HttpGet]
    [Route("lacto-vegetarian")]
    public async Task<IEnumerable<RecipeDto>> GetLactoVegetarianRecipes()
    {
        return await _service.GetLactoVegetarianRecipes();
    }

    [HttpGet]
    [Route("ovo-lacto-vegetarian")]
    public async Task<IEnumerable<RecipeDto>> GetOvoLactoVegetarianRecipes()
    {
        return await _service.GetOvoLactoVegetarianRecipes();
    }

    [HttpGet]
    [Route("pollotarian")]
    public async Task<IEnumerable<RecipeDto>> GetPollotarianRecipes()
    {
        return await _service.GetPollotarianRecipes();
    }

    [HttpGet]
    [Route("pescetarian")]
    public async Task<IEnumerable<RecipeDto>> GetPescetarianRecipes()
    {
        return await _service.GetPescetarianRecipes();
    }

    [HttpGet]
    [Route("pollo-pescetarian")]
    public async Task<IEnumerable<RecipeDto>> GetPolloPescetarianRecipes()
    {
        return await _service.GetPolloPescetarianRecipes();
    }

    [HttpGet]
    [Route("preparationTime")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> FilterByPreparationTime([FromQuery] int gte, [FromQuery] int lte)
    {
        if (gte < 0 || lte < 0)
            return new BadRequestObjectResult(
                $"Neither value can be a negative integer (Values provided were: minimum prep time = {gte}, maximum prep time = {lte})");

        if (gte > lte)
            return new BadRequestObjectResult(
                $"Maximum prep time must be lower or equal to minimum prep time (Values provided were {gte} and {lte} respectively)");

        return await _service.FilterByPreparationTime(gte, lte);
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
    public async Task<ActionResult<IEnumerable<RecipeDto>>> FilterByPortions([FromQuery] int gte,
        [FromQuery] int lte)
    {
        if (gte < 0 || lte < 0)
            return new BadRequestObjectResult(
                $"Neither value can be a negative integer (Values provided were: minimum portions = {gte}, maximum portions = {lte})");

        if (gte > lte)
            return new BadRequestObjectResult(
                $"Maximum portions must be lower or equal to minimum portions (Values provided were {gte} and {lte} respectively)");

        return await _service.FilterByPortions(gte, lte);
    }
}