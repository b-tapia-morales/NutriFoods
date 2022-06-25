using API.Dto;

namespace API.Ingredients;

public interface IIngredientRepository
{
    Task<IngredientDto> FindByName(string name);
    Task<IngredientDto> FindById(int id);
    Task<List<IngredientDto>> FindByPrimaryGroup(string name);
    Task<List<IngredientDto>> FindByPrimaryGroup(int id);
    Task<List<IngredientDto>> FindBySecondaryGroup(string name);
    Task<List<IngredientDto>> FindBySecondaryGroup(int id);
    Task<List<IngredientDto>> FindByTertiaryGroup(string name);
    Task<List<IngredientDto>> FindByTertiaryGroup(int id);
    Task<List<IngredientDto>> FindAll();
}