using API.Ingredients;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientController
{
    private readonly IIngredientRepository _repository;

    public IngredientController(IIngredientRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [Route("")]
    public async Task<ActionResult<IEnumerable<Ingredient>>> GetAllIngredients()
    {
        try
        {
            return (await _repository.FindAll()).ToList();
        }
        catch (Exception)
        {
            return new StatusCodeResult(StatusCodes.Status503ServiceUnavailable);
        }
    }

    [HttpGet]
    [Route("name/{name}")]
    public async Task<ActionResult<Ingredient>> FindByName(string name)
    {
        return await _repository.FindByName(name.ToLower());
    }

    [HttpGet]
    [Route("primaryGroup/{name}")]
    public async Task<ActionResult<IEnumerable<Ingredient>>> FindByPrimaryGroup(string name)
    {
        return await _repository.FindByPrimaryGroup(name.ToLower());
    }

    [HttpGet]
    [Route("secondaryGroup/{name}")]
    public async Task<ActionResult<IEnumerable<Ingredient>>> FindBySecondaryGroup(string name)
    {
        return await _repository.FindBySecondaryGroup(name.ToLower());
    }

    [HttpGet]
    [Route("tertiaryGroup/{name}")]
    public async Task<ActionResult<IEnumerable<Ingredient>>> FindByTertiaryGroup(string name)
    {
        return await _repository.FindByTertiaryGroup(name.ToLower());
    }
}