using Domain.Models;

namespace API.Ingredients;

public interface IIngredientRepository
{
    Task<Ingredient> FindByName(string name);
    Task<Ingredient> FindById(int id);
    Task<List<Ingredient>> FindByPrimaryGroup(string name);
    Task<List<Ingredient>> FindBySecondaryGroup(string name);
    Task<List<Ingredient>> FindByTertiaryGroup(string name);
    Task<List<Ingredient>> FindAll();
}