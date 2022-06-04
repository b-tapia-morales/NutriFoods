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
        return await _repository.FindAll();
    }

    [HttpGet]
    [Route("name/{name}")]
    public async Task<ActionResult<Ingredient>> FindByName(string name)
    {
        try
        {
            return await _repository.FindByName(name.ToLower());
        }
        catch (InvalidOperationException)
        {
            return new StatusCodeResult(StatusCodes.Status404NotFound);
        }
    }

    [HttpGet]
    [Route("primaryGroup/{name}")]
    public async Task<ActionResult<IEnumerable<Ingredient>>> FindByPrimaryGroup(string name)
    {
        var list = await _repository.FindByPrimaryGroup(name.ToLower());
        if (!list.Any())
        {
            return new StatusCodeResult(StatusCodes.Status404NotFound);
        }

        return list;
    }

    [HttpGet]
    [Route("secondaryGroup/{name}")]
    public async Task<ActionResult<IEnumerable<Ingredient>>> FindBySecondaryGroup(string name)
    {
        var list = await _repository.FindBySecondaryGroup(name.ToLower());
        if (!list.Any())
        {
            return new StatusCodeResult(StatusCodes.Status404NotFound);
        }

        return list;
    }

    [HttpGet]
    [Route("tertiaryGroup/{name}")]
    public async Task<ActionResult<IEnumerable<Ingredient>>> FindByTertiaryGroup(string name)
    {
        var list = await _repository.FindByTertiaryGroup(name.ToLower());
        if (!list.Any())
        {
            return new StatusCodeResult(StatusCodes.Status404NotFound);
        }

        return list;
    }
}