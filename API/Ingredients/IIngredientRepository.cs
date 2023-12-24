using API.Dto;
using API.Dto.Insertion;
using Domain.Enum;

namespace API.Ingredients;

public interface IIngredientRepository
{
    Task<List<IngredientDto>> FindAll(int pageNumber, int pageSize);
    Task<List<IngredientDto>> FindOrderedBy(Nutrients nutrient, int pageNumber, int pageSize, bool descending);
    Task<IngredientDto?> FindByName(string name);
    Task<IngredientDto?> FindById(int id);
    Task<List<IngredientDto>> FindByFoodGroup(FoodGroups group, int pageNumber, int pageSize);
    Task<IngredientDto> InsertSynonyms(IngredientDto dto, SynonymInsertion insertion);
    IAsyncEnumerable<IngredientDto> InsertSynonyms(List<SynonymInsertion> insertions);
    Task<IngredientDto> InsertMeasures(IngredientDto dto, MeasureInsertion insertion);
    IAsyncEnumerable<IngredientDto> InsertMeasures(List<MeasureInsertion> insertions);
    Task<List<IngredientDto>> InsertIngredients(List<MinimalIngredient> insertions);
}