using CsvHelper.Configuration;

namespace RecipeInsertion.Mapping;

public sealed class RecipeIngredientMapping : ClassMap<RecipeIngredient>
{
    public RecipeIngredientMapping()
    {
        Map(r => r.Quantity);
        Map(r => r.MeasureName);
        Map(r => r.IngredientName);
    }
}