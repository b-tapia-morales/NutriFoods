﻿using Domain.Models;
using Microsoft.EntityFrameworkCore;
using RecipeInsertion.Mapping;
using Utils.Csv;

namespace RecipeInsertion;

public static class Recipes
{
    private const string ConnectionString =
        "Host=localhost;Database=nutrifoods_db;Username=nutrifoods_dev;Password=MVmYneLqe91$";

    private static readonly string FilePathRecipes = Path.Combine(Directory.GetParent(
            Directory.GetCurrentDirectory())!.FullName, "RecipeInsertion", "Recipe", "recipe.csv");

    private static readonly string FilePathIngredient = Path.Combine(Directory.GetParent(
        Directory.GetCurrentDirectory())!.FullName, "RecipeInsertion","Ingredient","ingredient.csv");

    public static void RecipeInsert()
    {
        using var context = Context();
        
        var recipes = RowRetrieval
            .RetrieveRows<Recipe, RecipeMapping>(FilePathRecipes, DelimiterToken.Semicolon, true)
            .DistinctBy(e => e.Name);

        foreach (var recipe in recipes)
        {
            context.Add(new Recipe
            {
                Name = recipe.Name,
                Author = recipe.Author,
                Url = recipe.Url,
                Portions = recipe.Portions,
                PreparationTime = recipe.PreparationTime
            });
        }

        context.SaveChanges();
    }

    public static void RecipeMeasures()
    {
        using var context = Context();
        var ingredients = context.Ingredients.ToList();
        var nameIngredient = File.ReadAllLines(FilePathIngredient);
        foreach (var name in nameIngredient)
        {
            var id = ingredients.Find(x => x.Name.Equals(name))!.Id;
            var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.FullName,
                "RecipeInsertion", "Measures" , $"{name}.csv");
            var measuresIngredient =
                RowRetrieval.RetrieveRows<IngredientMeasure, MeasuresMapping>(path);
            foreach (var measure in measuresIngredient)
            {
                context.Add(new IngredientMeasure
                {
                    IngredientId = id,
                    Name = measure.Name,
                    Grams = measure.Grams
                });
            }
        }

        context.SaveChanges();
    }
    private static NutrifoodsDbContext Context()
    {
        var options = new DbContextOptionsBuilder<NutrifoodsDbContext>()
            .UseNpgsql(ConnectionString,
                builder => builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
            .Options;
        var context = new NutrifoodsDbContext(options);
        return context;
    }
    
}