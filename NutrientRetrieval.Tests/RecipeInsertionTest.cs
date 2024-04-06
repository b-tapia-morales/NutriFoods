using Domain.Models;
using RecipeInsertion.Mapping;
using Utils.Csv;
using Utils.Enumerable;
using Xunit.Abstractions;

namespace NutrientRetrieval.Tests;

public class RecipeInsertionTest(ITestOutputHelper output)
{
    private const string RecipesPath = @"..\..\..\..\RecipeInsertion\Recipe\recipe.csv";
    
    [Fact]
    public void AllUrlsAreUnique()
    {
        var mappings = CsvUtils
            .RetrieveRows<Recipe, RecipeMapping>(RecipesPath, DelimiterToken.Semicolon, true)
            .ToList();
        var urlDictionary = mappings
            .GroupBy(e => e.Url)
            .ToDictionary(g => g.Key, g => g.ToList());
        var repeated = urlDictionary.Where(e => e.Value.Count > 1).ToDictionary(e => e.Key, e => e.Value);
        var formattedOutput = repeated.Select(e => $"{e.Key} : {e.Value.Select(x => x.Name).ToJoinedString()}");
        output.WriteLine(formattedOutput.ToJoinedString());
        Assert.True(repeated.All(e => e.Value.Count == 1));
    }
    
    [Fact]
    public void AllNamesAreUnique()
    {
        var mappings = CsvUtils
            .RetrieveRows<Recipe, RecipeMapping>(RecipesPath, DelimiterToken.Semicolon, true)
            .ToHashSet();
        var allMappings = mappings.Select(e => (e.Name, e.Author)).ToHashSet();
        var distinctByUrl = mappings.DistinctBy(e => new {e.Name, e.Author}).Select(e => (e.Name, e.Author)).ToHashSet();
        output.WriteLine(allMappings.Except(distinctByUrl).ToJoinedString());
        output.WriteLine(distinctByUrl.Except(allMappings).ToJoinedString());
        Assert.False(allMappings.Except(distinctByUrl).Any());
        Assert.False(distinctByUrl.Except(allMappings).Any());
    }
}