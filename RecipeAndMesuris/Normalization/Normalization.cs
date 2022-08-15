using System.Globalization;
using F23.StringSimilarity;

namespace RecipeAndMesuris.Normalization;

public static class Normalization
{
    private static readonly Dictionary<string,List<string>> MeasuresNormalization = ValuesMeasuresOrIngredient(1);
    private static readonly Dictionary<string, List<string>> IngredientNormalization = ValuesMeasuresOrIngredient(2);
    public static void NormalizationMeasures(string file, double porcent)
    {
        var measures = File.ReadAllLines($"Normalization/{file}").ToList();
        var measuresUtility = File.ReadAllLines($"Normalization/{file}").ToList();
        measures.Remove("x");
        measures.Remove("g");
        measures.Remove("l");
        var similitary = new Cosine(2);
        var result = file.Equals("ingredient.csv") ? "groupedIngredient.csv" : "groupedMeasures.csv";
        var sw = new StreamWriter($"C:/Users/Eduardo/RiderProjects/NutriFoods-Latest/RecipeAndMesuris/Normalization/{result}");
        
        foreach (var simility in measures.Select(similitaryIngredient =>
                     measuresUtility.Where(x => similitary.Similarity(similitary.GetProfile(x), similitary.GetProfile(similitaryIngredient)) > porcent).ToList()))
        {
            if (simility.Count > 0)
            {
                Measures(simility,sw,measuresUtility);
            }
        }
        sw.Close();
    }

    private static void Measures(List<string> measures, TextWriter sw,ICollection<string> measuresReset)
    {
        var concat = "";
        foreach (var name in measures)
        {
            measuresReset.Remove(name);
            concat += name + ",";
        }
        sw.WriteLine(concat);
    }

    public static void NormalizationFilesRecipeIngredient()
    {
        string[] category = { "Ensaladas", "Entradas", "PlatosFondo", "Postres", "Vegano" };
        foreach (var nameCategory in category)
        {
            var nameRecipe = File.ReadAllLines($"Recipe_insert/Recipe/gourmet/recetas_{nameCategory}.txt");
            foreach (var x in nameRecipe)
            {
                var name = x.Split(";")[0];
                var fileRecipeIngredientMeasures = File.ReadAllLines($"Recipe_insert/ingredient/parcerIngredientes/" +
                                                                     $"ingredientes_Gourmet/{nameCategory}/ingre_{name}.txt");
                var dataRecipeIngredientMeasures = new StreamWriter($"C:/Users/Eduardo/RiderProjects/NutriFoods/RecipeAndMesuris/Recipes/Ingredient/{nameCategory}/{name.ToLower().Replace(" ","_")}.csv");

                foreach (var dataRecipe in fileRecipeIngredientMeasures)
                {
                    var quantity = dataRecipe.Split(",")[0];
                    var ingredient = FrecuencyTable.RemoveAccentsWithNormalization(dataRecipe.Split(",")[2].ToLower());
                    var measures = FrecuencyTable.RemoveAccentsWithNormalization(dataRecipe.Split(",")[1].ToLower());
                    var measuresNormalized = GetKeyMeasures(measures);
                    var ingredientNormalized = GetKeyIngredient(ingredient);
                    var quantityNormalized = NormalizationUnicodeQuantity(quantity,measures);
                    var lineData = quantityNormalized+","+measuresNormalized+","+ingredientNormalized;
                    dataRecipeIngredientMeasures.WriteLine(lineData);
                }
                dataRecipeIngredientMeasures.Close();

                var fileRecipeStep = File.ReadAllLines($"C:/Users/Eduardo/RiderProjects/NutriFoods/RecipeAndMesuris/Recipe_insert/ingredient/preparacion_recetas_gourmet/{nameCategory}/prepa_{name}.txt");
                
                var dataRecipeIngredientSteps = new StreamWriter($"C:/Users/Eduardo/RiderProjects/NutriFoods/RecipeAndMesuris/Recipes/Steps/{nameCategory}/{name.ToLower().Replace(" ","_")}.csv");
                foreach (var dataSteps in fileRecipeStep)
                {
                    dataRecipeIngredientSteps.WriteLine(dataSteps.Normalize());
                }
                dataRecipeIngredientSteps.Close();
            }
        }
    }

    public static Dictionary<string, List<string>> ValuesMeasuresOrIngredient(int values)
    {
        var dictionary = new Dictionary<string, List<string>>();
        // 1 para Measures, 2 para ingredient(cualquier otro numero)
        var file = values == 1 ? "groupedMeasures.csv" :"groupedIngredient.csv";
        var measuresNormalization = File.ReadAllLines($"Normalization/{file}");
        foreach (var data in measuresNormalization)
        {
            var key = data.Split(";")[0];
            var similarWords = data.Split(";")[1].Split(",").ToList();
            dictionary.Add(key,similarWords);
        }
        return dictionary;
    }

    public static string NormalizationUnicodeQuantity(string unicode,string units)
    {
        var normalized = "";
        if (unicode.Contains('.'))
        {
            return NormalizationDecimal(unicode, units);
        }
        foreach (var character in unicode)
        {
            if (char.GetUnicodeCategory(character) == UnicodeCategory.OtherNumber)
            {
                normalized += TransformUnicode(character);
            }else normalized += character;
            
        }
        return normalized;

    }

    private static string TransformUnicode(char unicode)
    {
        if (char.GetUnicodeCategory(unicode) != UnicodeCategory.OtherNumber) return unicode.ToString();
        if (Math.Abs(CharUnicodeInfo.GetNumericValue(unicode) - 0.25) == 0) return "1/4";
        if (Math.Abs(CharUnicodeInfo.GetNumericValue(unicode) - 0.75) == 0) return "3/4";
        if (Math.Abs(CharUnicodeInfo.GetNumericValue(unicode) - 0.5) == 0) return "1/2";

        return unicode.ToString();
    }

    private static string NormalizationDecimal(string quantity, string units)
    {
        var enters = quantity.Split(".")[0];
        var fraction = quantity.Split(".")[1];
        if (!units.Contains("cd") && !units.Equals("x"))
        {
            return (1000*int.Parse(enters)+100*int.Parse(fraction)).ToString();
        }
        return fraction switch
        {
            "5" => enters + " 1/2",
            "25" => enters + " 1/4",
            _ => quantity
        };
    }

    public static string GetKeyMeasures(string measures)
    {
        foreach (var dictionaryKeys in MeasuresNormalization.Where(dictionaryKeys =>
                     dictionaryKeys.Value.Any(measuresSimile => measuresSimile.Equals(measures))))
        {
            return dictionaryKeys.Key;
        }

        return measures;
    }
    
    public static string GetKeyIngredient(string measures)
    {
        foreach (var dictionaryKeys in IngredientNormalization.Where(dictionaryKeys =>
                     dictionaryKeys.Value.Any(ingredientSimile => ingredientSimile.Equals(measures))))
        {
            return dictionaryKeys.Key;
        }

        return measures;
    }
}