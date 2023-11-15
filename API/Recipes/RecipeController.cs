using API.Dto;
using Domain.Enum;
using Microsoft.AspNetCore.Mvc;

namespace API.Recipes;

[ApiController]
[Route("api/v1/recipes")]
public class RecipeController(IRecipeRepository repository)
{
    [HttpGet]
    [Route("")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> FindAll()
    {
        return await repository.FindAll();
    }

    [HttpGet]
    [Route("name/{name}")]
    public async Task<ActionResult<RecipeDto>> FindByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return new BadRequestObjectResult("Parameter can't be an empty or whitespace string");

        var recipe = await repository.FindByName(name);
        return recipe == null ? new NotFoundResult() : recipe;
    }

    [HttpGet]
    [Route("id/{id:int}")]
    public async Task<ActionResult<RecipeDto>> FindById(int id)
    {
        if (id < 0)
            return new BadRequestObjectResult($"Parameter can't be a negative integer (Value provided was: {id})");

        var recipe = await repository.FindById(id);
        return recipe == null ? new NotFoundResult() : recipe;
    }

    [HttpGet]
    [Route("meal-type/{mealType}")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> FindByMealType(MealToken mealType)
    {
        var value = IEnum<MealTypes, MealToken>.FromToken(mealType);
        return await (value == MealTypes.None ? repository.FindAll() : repository.FindByMealType(value));
    }

    [HttpGet]
    [Route("dish-type/{dishType}")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> FindByDishType(DishToken dishType)
    {
        var value = IEnum<DishTypes, DishToken>.FromToken(dishType);
        return await (value == DishTypes.None ? repository.FindAll() : repository.FindByDishType(value));
    }

    [HttpGet]
    [Route("diet/{diet}")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> FindVegetarianRecipes(DietToken diet)
    {
        var value = IEnum<Diets, DietToken>.FromToken(diet);
        return await (value == Diets.None ? repository.FindAll() : repository.GetVegetarianRecipes(value));
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

        return await repository.FilterByPreparationTime(lower, upper);
    }

    [HttpGet]
    [Route("portions/{portions:int}")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> FilterByPortions(int portions)
    {
        if (portions < 0)
            return new BadRequestObjectResult(
                $"Parameter can't be a negative integer (Value provided was: {portions})");

        return await repository.FilterByPortions(portions);
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
        return await repository.FilterByPortions(lower, upper);
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

        return await repository.FilterByEnergy(lower, upper);
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

        return await repository.FilterByCarbohydrates(lower, upper);
    }

    [HttpGet]
    [Route("fatty-acids")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> FilterByLipids([FromQuery] int lower, [FromQuery] int upper)
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
    public async Task<ActionResult<IEnumerable<RecipeDto>>> FilterByProteins([FromQuery] int lower,
        [FromQuery] int upper)
    {
        if (lower < 0 || upper < 0)
            return new BadRequestObjectResult(
                $"Neither value can be a negative integer (Values provided were: minimum proteins = {lower}, maximum proteins = {upper})");

        if (lower > upper)
            return new BadRequestObjectResult(
                $"Maximum proteins must be lower or equal to minimum proteins (Values provided were {lower} and {upper} respectively)");

        return await repository.FilterByProteins(lower, upper);
    }

    [HttpGet]
    [Route("distribution")]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> FilterByDistribution(
        [FromQuery] int energyLimit, [FromQuery] int carbohydratesLimit, [FromQuery] int lipidsLimit,
        [FromQuery] int proteinsLimit)
    {
        if (energyLimit < 0 || carbohydratesLimit < 0 || lipidsLimit < 0 || proteinsLimit < 0)
            return new BadRequestObjectResult("No value can be a negative integer");

        return await repository.FilterByMacronutrientDistribution(energyLimit, carbohydratesLimit, lipidsLimit,
            proteinsLimit);
    }
}