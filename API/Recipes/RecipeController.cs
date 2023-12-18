using System.ComponentModel.DataAnnotations;
using API.Dto;
using API.Dto.Insertion;
using Domain.Enum;
using Microsoft.AspNetCore.Mvc;

namespace API.Recipes;

[ApiController]
[Route("api/v1/recipes")]
public class RecipeController(IRecipeRepository repository)
{
    private const int DefaultPageSize = 20;

    [HttpGet]
    [Route("")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> FindAll([FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = DefaultPageSize) =>
        await repository.FindAll(pageNumber, pageSize);

    [HttpGet]
    [Route("name/{name:minlength(2)}/author/{author:minlength(2)}")]
    public async Task<ActionResult<RecipeDto>> FindByNameAndAuthor([Required] string name, [Required] string author)
    {
        var recipe = await repository.FindByNameAndAuthor(name, author);
        return recipe == null ? new NotFoundResult() : recipe;
    }

    [HttpGet]
    [Route("id/{id:int:min(1)}")]
    public async Task<ActionResult<RecipeDto>> FindById([Required] int id)
    {
        var recipe = await repository.FindById(id);
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
            ? repository.FindAll(pageNumber, pageSize)
            : repository.FindByMealType(value, pageNumber, pageSize));
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
            ? repository.FindAll(pageNumber, pageSize)
            : repository.FindByDishType(value, pageNumber, pageSize));
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
            ? repository.FindAll(pageNumber, pageSize)
            : repository.GetVegetarianRecipes(value, pageNumber, pageSize));
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

        return await repository.FilterByPreparationTime(lower, upper, pageNumber, pageSize);
    }

    [HttpGet]
    [Route("portions/{portions:int:min(1)}")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> FilterByExactPortions(
        [Required] int portions,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = DefaultPageSize) =>
        await repository.FilterByPortions(portions, pageNumber, pageSize);

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

        return await repository.FilterByPortions(lower, upper, pageNumber, pageSize);
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

        return await repository.FilterByEnergy(lower, upper);
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

        return await repository.FilterByCarbohydrates(lower, upper);
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

        return await repository.FilterByFattyAcids(lower, upper);
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

        return await repository.FilterByProteins(lower, upper);
    }

    public async Task<ActionResult<RecipeLogging>> InsertRecipe([FromBody] MinimalRecipe recipe)
    {
        if (await repository.FindByNameAndAuthor(recipe.Name, recipe.Author) != null)
            return new ConflictObjectResult("");
        if (await repository.FindByUrl(recipe.Url) != null)
            return new ConflictObjectResult("");

        return new OkObjectResult("");
    }
}