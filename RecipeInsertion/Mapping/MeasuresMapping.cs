using CsvHelper.Configuration;
using Domain.Models;

namespace RecipeInsertion.Mapping;

public sealed class MeasuresMapping : ClassMap<IngredientMeasure>
{
    public MeasuresMapping()
    {
        Map(m => m.Name);
        Map(m => m.Grams);
    }
}