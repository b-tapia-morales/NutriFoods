using CsvHelper.Configuration;
using Domain.Models;

namespace RecipeInsertion.Mapping;

public sealed class IngredientMeasureMapping : ClassMap<IngredientMeasure>
{
    public IngredientMeasureMapping()
    {
        Map(m => m.Name);
        Map(m => m.Grams);
    }
}