using System.Globalization;
using Npgsql;


namespace RecipeAndMesuris.Recipe_insert;

public class IngredientRecipe
{
    private NpgsqlConnection _connection;

    public IngredientRecipe(NpgsqlConnection connection)
    {
        _connection = connection;
    }
    private void ReadFileRecipeIngredient(string pathReceta)
    {
        StreamReader fileRecipeIngredients = new StreamReader(pathReceta);
        //Console.WriteLine(pathReceta);
        while (!fileRecipeIngredients.EndOfStream)
        {
            string line = fileRecipeIngredients.ReadLine() ?? throw new InvalidOperationException();
            string[] split = line.Split(",");
            string ingredient = split[2];
            TransformUnits(split[0],split[1], ingredient);

        }
        fileRecipeIngredients.Close();
    }

    private void InsertIngredientRecipe(string pathReceta)
    {
        StreamReader fileRecipe = new StreamReader($"Recipe_insert/Recipe/gourmet/recetas_{pathReceta}.txt");
        while (!fileRecipe.EndOfStream)
        {
            String line = fileRecipe.ReadLine() ?? throw new InvalidOperationException();
            String[] spilt = line.Split(";");
            ReadFileRecipeIngredient(
                $"Recipe_insert/Ingredient/parcerIngredientes/Ingredientes_Gourmet/{pathReceta}/ingre_{spilt[0]}.txt");
        }
        fileRecipe.Close();
        
    }

    public void ReadInsertRecipeIngredient()
    {
        string[] path = {"Ensaladas","Entradas","PlatosFondo"};
        foreach (var nameDirectory in path)
        {
            InsertIngredientRecipe(nameDirectory);
        }
    }

    private void TransformUnits(String quantity, String units, String ingredient)
    {
        List<int> numDem;
        String consult = $"SELECT name FROM nutrifoods.ingredient WHERE nutrifoods.similarity(ingredient.name::TEXT,'{ingredient}'::TEXT) > 0.55;";
        NpgsqlCommand commandSelectIngredient = new NpgsqlCommand(consult,_connection);
        Console.Write(ingredient + " ");
        var ingredientResult = commandSelectIngredient.ExecuteScalar();
        Console.WriteLine(ingredientResult);
        /*
        if (quantity.Length == 1)
        {
            numDem = TransformUnicode(char.Parse(quantity));
            if (numDem.Count > 0)
            {
                //Console.WriteLine(numDem[0] + " "+numDem[1]);
            }
            else
            {
                //Console.WriteLine(quantity);
            }
        }
        else
        {
            if (quantity.Contains("/"))
            {
                int numerador = quantity[0] - '0';
                int denominador = quantity[2] - '0';
                //Console.WriteLine(quantity);
            }
            else
            {
                if (quantity.Contains(" "))
                {
                    //Console.WriteLine(quantity + " "+units);
                    int intergerPart = quantity[0] - '0';
                    numDem = TransformUnicode(quantity[2]);
                    //Console.WriteLine(intergerPart+" "+numDem[0] + " "+ numDem[1]);
                }
                else
                {
                    if (!ingredient.ToLower().Equals("agua"))
                    {
                        if(units.Equals("ml") || units.Equals("x")) Console.WriteLine(
                            $"{quantity} {units} {ingredient}");
                    }
                   
                }
            }
        }*/
    }

    private List<int> TransformUnicode(char unit)
    {
        List<int> numDem = new List<int>(2);
        if (Char.GetUnicodeCategory(unit) == UnicodeCategory.OtherNumber)
        {
            if(Math.Abs(CharUnicodeInfo.GetNumericValue(unit) - 0.25) == 0)
            {
                numDem.Add(1);
                numDem.Add(4);
            }

            if (Math.Abs(CharUnicodeInfo.GetNumericValue(unit) - 0.75) == 0)
            {
                numDem.Add(3);
                numDem.Add(4);
            }
            if(Math.Abs(CharUnicodeInfo.GetNumericValue(unit) - 0.5) == 0)
            {
                numDem.Add(1);
                numDem.Add(2);
            }
        }
        return numDem;
    }
}