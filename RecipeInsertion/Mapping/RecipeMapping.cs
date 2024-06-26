using CsvHelper.Configuration;
using Domain.Models;

namespace RecipeInsertion.Mapping;

public sealed class RecipeMapping : ClassMap<Recipe>
{
    public RecipeMapping()
    {
        Map(p => p.Name).Index(0);
        Map(p => p.Author).Index(1);
        Map(p => p.Url).Index(2).Optional();
        Map(p => p.Time).Index(3).Optional();
        Map(p => p.Portions).Index(4).Optional();
    }
}