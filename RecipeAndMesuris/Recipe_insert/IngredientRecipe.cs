using System.Globalization;
using Npgsql;


namespace RecipeAndMesuris.Recipe_insert;

public class IngredientRecipe
{
    private NpgsqlConnection _connection;
    private MissingIngredient _missingIngredient;

    public IngredientRecipe(NpgsqlConnection connection)
    {
        _connection = connection;
        //_missingIngredient = new MissingIngredient();
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
            TransformUnits(split[0], split[1], ingredient, pathReceta);
            
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
        string[] path = {"Ensaladas","Entradas","PlatosFondo","Postres","Vegano"};
        foreach (var nameDirectory in path)
        {
            InsertIngredientRecipe(nameDirectory);
        }
        _missingIngredient.Write();
    }

    private void TransformUnits(String quantity, String units, String ingredient, String pathReceta)
    {
        List<int> numDem;

        if (ingredient.ToLower().Contains("merquen") || ingredient.Contains("Merquén") || ingredient.Contains("merquén")) ingredient = "Merkén";
        if (ingredient.ToLower().Equals("caldo pollo")) ingredient = "Caldo de ave";
        if (ingredient.Equals("cous-cous") || ingredient.Equals("couscous")) ingredient = "Cuscús";
        if (ingredient.Equals("camarones")) ingredient = "Camarón";
        if (ingredient.ToLower().Equals("pimentón") || ingredient.Equals("pimenton")) ingredient = "Pimiento";
        if (ingredient.Equals("limon") || ingredient.Equals("limones")) ingredient = "Limón";
        if (ingredient.Equals("yemas") || ingredient.Equals("yema")) ingredient = "yema de huevo";
        if (ingredient.Equals("claras")) ingredient = "Clara de huevo";
        if (ingredient.ToLower().Equals("callampas")) ingredient = "Champiñón";
        if (ingredient.Equals("pasas rubias")) ingredient = "Pasa";
        if (ingredient.ToLower().Equals("Caldo Costilla")) ingredient = "Caldo de carne";
        if (ingredient.Equals("Pimiento Ahumano") || ingredient.Equals("Pimento Ahumado") || ingredient.Equals("Pimentón Ahumado")) ingredient = "Paprika";
        if (ingredient.Equals("pechugas")) ingredient = "Pechuga de pollo";
        if (ingredient.Equals("pasta ají amarillo")) ingredient = "Pasta de aji";
        if (ingredient.Equals("yoghurt natural") || ingredient.ToLower().Equals("yogurts naturales")) ingredient = "Yogurt";
        if (ingredient.Equals("ají amarillos")) ingredient = "Ají";
        if (ingredient.ToLower().Equals("salvia")) ingredient = "Salvia deshidratada";
        if (ingredient.ToLower().Equals("cranberries")) ingredient = "arándano";
        if (ingredient.ToLower().Equals("aji")) ingredient = "ají";
            String consult = $"SELECT name FROM nutrifoods.ingredient WHERE nutrifoods.similarity(ingredient.name::TEXT,'{ingredient}'::TEXT) > 0.40;";
        NpgsqlCommand commandSelectIngredient = new NpgsqlCommand(consult,_connection);
        var ingredientResult = commandSelectIngredient.ExecuteScalar();
        
        if (ingredientResult == null)
        {
            if (!ingredient.ToLower().Equals("agua"))
            {
                
                
                //_missingIngredient.ExistIngredient(ingredient.ToLower());
            }

            if (ingredient.ToLower().Equals("achiote"))
            {
                Console.WriteLine(pathReceta);
            }
        }
        
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