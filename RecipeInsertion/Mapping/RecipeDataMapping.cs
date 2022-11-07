using CsvHelper.Configuration;

namespace RecipeInsertion.Mapping;

public sealed class RecipeDataMapping : ClassMap<DataRecipe>
{
    public RecipeDataMapping()
    {
        Map(r => r.Quantity);
        Map(r => r.Units);
        Map(r => r.NameIngredients);
    }
}