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
    private void ReadFileRecipeIngredient(string pathReceta,string nameRecipe)
    {
        StreamReader fileRecipeIngredients = new StreamReader(pathReceta);
        Console.WriteLine("--------------------------------------");
        Console.WriteLine(pathReceta);
        var cant = 0;
        while (!fileRecipeIngredients.EndOfStream)
        {
            string line = fileRecipeIngredients.ReadLine() ?? throw new InvalidOperationException();
            string[] split = line.Split(",");
            string ingredient = split[2];
            string consultId = $"SELECT id FROM nutrifoods.recipe WHERE name = '{nameRecipe}';";
            NpgsqlCommand command = new NpgsqlCommand(consultId,_connection);
            var recipeId = command.ExecuteScalar();
            TransformUnits(split[0], split[1], ingredient, pathReceta,recipeId.ToString());
            cant++;
        }
        Console.WriteLine(cant);
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
                $"Recipe_insert/Ingredient/parcerIngredientes/Ingredientes_Gourmet/{pathReceta}/ingre_{spilt[0]}.txt",spilt[0]);
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

    private void TransformUnits(String quantity, String units, String ingredient, String pathReceta,String recipeId)
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
        if (units.Equals("cda") || units.Equals("cdas")) units = "cucharada"; 
        if (units.Equals("cdta") || units.Equals("cdtas")) units = "cucharadita";
        String consult = $"SELECT id FROM nutrifoods.ingredient WHERE nutrifoods.similarity(ingredient.name::TEXT,'{ingredient}'::TEXT) > 0.40;";
        NpgsqlCommand commandSelectIngredient = new NpgsqlCommand(consult,_connection);
        var ingredientResult = commandSelectIngredient.ExecuteScalar();
        
        if (ingredientResult != null)
        {
            if (quantity.Equals("x") && units.Equals("x"))
            {
                //Console.WriteLine(ingredient);
            }
            else
            {
                if (units.Equals("x"))
                {
                    string avg = $"SELECT avg(grams) FROM nutrifoods.ingredient_measure WHERE ingredient_id = {ingredientResult};";
                    NpgsqlCommand comAvg = new NpgsqlCommand(avg,_connection);
                    var result = comAvg.ExecuteScalar();
                    Console.WriteLine(result);
                }
                else
                {
                    if (units.Equals("g") || units.Equals("gr"))
                    {
                        
                        string insertRecipeQuantity = $"INSERT INTO nutrifoods.recipe_quantity (recipe_id, ingredient_id, grams) " +
                                                      $"VALUES ({recipeId},{ingredientResult},{quantity}) ON CONFLICT DO NOTHING;";
                        NpgsqlCommand command = new NpgsqlCommand(insertRecipeQuantity, _connection);
                        command.ExecuteNonQuery();
                        //Console.WriteLine(quantity +" | "+units+ "| ingredient "+ingredient);
                    }
                    else
                    {
                        //Console.WriteLine(quantity +" | "+units.Replace("s","")+ "| ingredient "+ingredient);
                        string measurisConsult = $"SELECT * FROM nutrifoods.ingredient_measure WHERE ingredient_id = {ingredientResult} " +
                                                 $"AND nutrifoods.similarity(name,'{units.Replace("s","")}') > 0.40 LIMIT 1;";
                        NpgsqlCommand commands = new NpgsqlCommand(measurisConsult,_connection);
                        var measurisResult = commands.ExecuteReader();
                        while (measurisResult.Read())
                        {
                            //Console.WriteLine(units +" == "+measurisResult.GetValue(2) + " | ingrediente "+ingredient);
                        }
                        measurisResult.Close();
                    }
                    
                }
            }







            /*
            if (!ingredient.ToLower().Equals("agua"))
            {
                
                
                //_missingIngredient.ExistIngredient(ingredient.ToLower());
            }

            if (ingredient.ToLower().Equals("ralladarura limon"))
            {
                Console.WriteLine(pathReceta);
            }
            */
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