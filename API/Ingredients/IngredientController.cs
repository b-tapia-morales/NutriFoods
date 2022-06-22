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
    [Route("id/{id:int}")]
    public async Task<ActionResult<Ingredient>> FindById(int id)
    {
        try
        {
            return await _repository.FindById(id);
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
        if (!list.Any()) return new StatusCodeResult(StatusCodes.Status404NotFound);

        return list;
    }

    [HttpGet]
    [Route("primaryGroup/{id:int}")]
    public async Task<ActionResult<IEnumerable<Ingredient>>> FindByPrimaryGroup(int id)
    {
        var list = await _repository.FindByPrimaryGroup(id);
        if (!list.Any()) return new StatusCodeResult(StatusCodes.Status404NotFound);

        return list;
    }

    [HttpGet]
    [Route("secondaryGroup/{name}")]
    public async Task<ActionResult<IEnumerable<Ingredient>>> FindBySecondaryGroup(string name)
    {
        var list = await _repository.FindBySecondaryGroup(name.ToLower());
        if (!list.Any()) return new StatusCodeResult(StatusCodes.Status404NotFound);

        return list;
    }

    [HttpGet]
    [Route("secondaryGroup/{id:int}")]
    public async Task<ActionResult<IEnumerable<Ingredient>>> FindBySecondaryGroup(int id)
    {
        var list = await _repository.FindBySecondaryGroup(id);
        if (!list.Any()) return new StatusCodeResult(StatusCodes.Status404NotFound);

        return list;
    }

    [HttpGet]
    [Route("tertiaryGroup/{name}")]
    public async Task<ActionResult<IEnumerable<Ingredient>>> FindByTertiaryGroup(string name)
    {
        var list = await _repository.FindByTertiaryGroup(name.ToLower());
        if (!list.Any()) return new StatusCodeResult(StatusCodes.Status404NotFound);

        return list;
    }

    [HttpGet]
    [Route("tertiaryGroup/{id:int}")]
    public async Task<ActionResult<IEnumerable<Ingredient>>> FindByTertiaryGroup(int id)
    {
        var list = await _repository.FindByTertiaryGroup(id);
        if (!list.Any()) return new StatusCodeResult(StatusCodes.Status404NotFound);

        return list;
    }
}