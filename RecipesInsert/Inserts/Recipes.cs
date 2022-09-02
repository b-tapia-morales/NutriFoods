using API.Dto;
using CsvHelper.Configuration;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace RecipesInsert.Inserts;

public class Recipes
{
    private const string ConnectionString =
        "Host=localhost;Database=nutrifoods_db;Username=nutrifoods_dev;Password=MVmYneLqe91$";

    public static void RecipeInsert()
    {
        var options = new DbContextOptionsBuilder<NutrifoodsDbContext>()
            .UseNpgsql(ConnectionString,
                builder => builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
            .Options;
        using var context = new NutrifoodsDbContext(options);
        var recipes = RetrieveRecipes();
        foreach (var recipe in recipes)
            context.Add(new Recipe
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Author = recipe.Author,
                Url = recipe.Url,
                Portions = recipe.Portions,
                PreparationTime = recipe.PreparationTime
            });
    }

    public static void RetrieveRecipes()
    {
        
    }
}


public sealed class RecipeMapping : ClassMap<RecipeDto>
{
    public RecipeMapping()
    {
        Map(p => p.Name).Index(0);
        Map(p => p.Author).Index(1);
        Map(p => p.Url).Index(2).Optional();
        Map(p => p.Portions).Index(3).Optional();
        Map(p => p.PreparationTime).Index(4).Optional();
    }
}