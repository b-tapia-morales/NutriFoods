using API.Dto;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Ingredients;

[ApiController]
[Route("api/ingredients")]
public class IngredientController
{
    private readonly IIngredientRepository _repository;

    public IngredientController(IIngredientRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [Route("")]
    public async Task<ActionResult<IEnumerable<IngredientDto>>> GetAllIngredients()
    {
        return await _repository.FindAll();
    }

    [HttpGet]
    [Route("name/{name}")]
    public async Task<ActionResult<IngredientDto>> FindByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return new BadRequestObjectResult("Parameter can't be a null, empty or whitespace string");
        }

        try
        {
            return await _repository.FindByName(name.ToLower());
        }
        catch (InvalidOperationException)
        {
            return new IngredientDto();
        }
    }

    [HttpGet]
    [Route("id/{id:int}")]
    public async Task<ActionResult<IngredientDto>> FindById(int id)
    {
        if (id < 0)
        {
            return new BadRequestObjectResult("Parameter can't be a negative integer");
        }

        try
        {
            return await _repository.FindById(id);
        }
        catch (InvalidOperationException)
        {
            return new IngredientDto();
        }
    }

    [HttpGet]
    [Route("primaryGroup/{name}")]
    public async Task<ActionResult<IEnumerable<IngredientDto>>> FindByPrimaryGroup(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return new BadRequestObjectResult("Parameter can't be a null, empty or whitespace string");
        }

        return await _repository.FindByPrimaryGroup(name.ToLower());
    }

    [HttpGet]
    [Route("primaryGroup/{id:int}")]
    public async Task<ActionResult<IEnumerable<IngredientDto>>> FindByPrimaryGroup(int id)
    {
        if (id < 0)
        {
            return new BadRequestObjectResult("Parameter can't be a negative integer");
        }

        return await _repository.FindByPrimaryGroup(id);
    }

    [HttpGet]
    [Route("secondaryGroup/{name}")]
    public async Task<ActionResult<IEnumerable<IngredientDto>>> FindBySecondaryGroup(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return new BadRequestObjectResult("Parameter can't be a null, empty or whitespace string");
        }

        return await _repository.FindBySecondaryGroup(name.ToLower());
    }

    [HttpGet]
    [Route("secondaryGroup/{id:int}")]
    public async Task<ActionResult<IEnumerable<IngredientDto>>> FindBySecondaryGroup(int id)
    {
        if (id < 0)
        {
            return new BadRequestObjectResult("Parameter can't be a negative integer");
        }

        return await _repository.FindBySecondaryGroup(id);
    }

    [HttpGet]
    [Route("tertiaryGroup/{name}")]
    public async Task<ActionResult<IEnumerable<IngredientDto>>> FindByTertiaryGroup(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return new BadRequestObjectResult("Parameter can't be a null, empty or whitespace string");
        }

        return await _repository.FindByTertiaryGroup(name.ToLower());
    }

    [HttpGet]
    [Route("tertiaryGroup/{id:int}")]
    public async Task<ActionResult<IEnumerable<IngredientDto>>> FindByTertiaryGroup(int id)
    {
        if (id < 0)
        {
            return new BadRequestObjectResult("Parameter can't be a negative integer");
        }

        return await _repository.FindByTertiaryGroup(id);
    }
}