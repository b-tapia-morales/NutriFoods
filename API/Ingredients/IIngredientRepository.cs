using Domain.Models;

namespace API.Ingredients;

public interface IIngredientRepository
{
    Task<Ingredient> FindByName(string name);
    Task<Ingredient> FindById(int id);
    Task<List<Ingredient>> FindByPrimaryGroup(string name);
    Task<List<Ingredient>> FindByPrimaryGroup(int id);
    Task<List<Ingredient>> FindBySecondaryGroup(string name);
    Task<List<Ingredient>> FindBySecondaryGroup(int id);
    Task<List<Ingredient>> FindByTertiaryGroup(string name);
    Task<List<Ingredient>> FindByTertiaryGroup(int id);
    Task<List<Ingredient>> FindAll();
}