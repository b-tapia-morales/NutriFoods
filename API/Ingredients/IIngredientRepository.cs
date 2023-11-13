using API.Dto;
using Domain.Enum;

namespace API.Ingredients;

public interface IIngredientRepository
{
    Task<List<IngredientDto>> FindAll();
    Task<IngredientDto?> FindByName(string name);
    Task<IngredientDto?> FindById(int id);
    Task<List<IngredientDto>> FindByFoodGroup(FoodGroups group);
}