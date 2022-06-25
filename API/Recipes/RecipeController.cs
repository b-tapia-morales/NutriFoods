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
}