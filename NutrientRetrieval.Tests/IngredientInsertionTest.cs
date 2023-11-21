using System.Collections.Immutable;
using System.Text.RegularExpressions;
using NutrientRetrieval.Mapping.Ingredient;
using Utils.Csv;
using Utils.Enumerable;
using Xunit.Abstractions;
using static System.StringComparer;

namespace NutrientRetrieval.Tests;

public class IngredientInsertionTest(ITestOutputHelper output)
{
    private const string IngredientScriptPath = @"..\..\..\..\Domain\Schema\Insert\ingredients.sql";
    private const string FdcPath = @"..\..\..\NutrientRetrieval\Files\IngredientIDs.csv";
    private const string NamesPath = @"..\..\..\..\RecipeInsertion\Ingredient\ingredient.csv";
    private static readonly string BasePath = Directory.GetParent(Directory.GetCurrentDirectory())!.FullName;
    private static readonly string AbsolutePath = Path.Combine(BasePath, FdcPath);

    [Fact]
    public void AllIdsAreUnique()
    {
        var rows = RowRetrieval.RetrieveRows<IngredientRow, IngredientMapping>(AbsolutePath).ToList();
        var nutriFoods = rows.Select(e => e.NutriFoodsId).ToList();
        Assert.True(nutriFoods.Count == nutriFoods.Distinct().Count());
    }

    [Fact]
    public void AllNamesAreUnique()
    {
        var rows = RowRetrieval.RetrieveRows<IngredientRow, IngredientMapping>(AbsolutePath).ToList();
        var nutriFoods = rows.Select(e => e.NutriFoodsName).ToList();
        Assert.True(nutriFoods.Count == nutriFoods.Distinct().Count());
    }

    [Fact]
    public void ScriptAndFdcCsvMatch()
    {
        var scriptNames = new HashSet<string>(Regex.Matches(File.ReadAllText(IngredientScriptPath), "'(.*?)'")
            .Select(e => e.Value.Replace("'", string.Empty)), InvariantCultureIgnoreCase);
        output.WriteLine(scriptNames.Count.ToString());
        var rowNames = new HashSet<string>(RowRetrieval.RetrieveRows<IngredientRow, IngredientMapping>(AbsolutePath)
            .Select(e => e.NutriFoodsName), InvariantCultureIgnoreCase);
        output.WriteLine(rowNames.Count.ToString());
        Assert.True(!rowNames.Except(scriptNames).Any());
    }
    
    [Fact]
    public void ScriptAndNamesCsvMatch()
    {
        var scriptNames = new HashSet<string>(Regex.Matches(File.ReadAllText(IngredientScriptPath), "'(.*?)'")
            .Select(e => e.Value.Replace("'", string.Empty)), InvariantCultureIgnoreCase);
        output.WriteLine(scriptNames.Count.ToString());
        var rowNames = new HashSet<string>(File.ReadAllLines(NamesPath).Where(e => !string.IsNullOrWhiteSpace(e)));
        output.WriteLine(rowNames.Count.ToString());
        output.WriteLine(scriptNames.Except(rowNames).ToJoinedString());
        Assert.True(rowNames.Except(scriptNames).Count() <= 3);
    }
}