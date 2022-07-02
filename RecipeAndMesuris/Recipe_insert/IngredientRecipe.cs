using System.Globalization;
using Npgsql;
using OpenQA.Selenium.DevTools.V85.Fetch;


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
        //Console.WriteLine("--------------------------------------");
        //Console.WriteLine(pathReceta);
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
        //Console.WriteLine(cant);
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
        //_missingIngredient.Write();
    }

    private void TransformUnits(String quantity, String units, String ingredient, String pathReceta,String recipeId)
    {

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
        if (ingredient.Equals("papas")) ingredient = "papa";
        if (units.Equals("cda") || units.Equals("cdas")) units = "cucharada"; 
        if (units.Equals("cdta") || units.Equals("cdtas")) units = "cucharadita";
        if (ingredient.ToLower().Equals("caldo carne") || ingredient.Equals("Caldo de carne")) ingredient = "Caldo de carne";
        String consult = $"SELECT id FROM nutrifoods.ingredient WHERE nutrifoods.similarity(ingredient.name,'{ingredient}') > 0.50;";
        NpgsqlCommand commandSelectIngredient = new NpgsqlCommand(consult,_connection);
        var ingredientResult = commandSelectIngredient.ExecuteScalar();
        
        if (ingredientResult != null)
        {
            if (units.Equals("x") || units.Equals("unidad"))
            {
                string avg = $"SELECT grams FROM nutrifoods.ingredient_measure WHERE ingredient_id = {ingredientResult} ORDER BY grams;";
                NpgsqlCommand comAvg = new NpgsqlCommand(avg,_connection);
                var result = comAvg.ExecuteReader();
                List<double> listGram = new List<double>();
                while (result.Read())
                { 
                    listGram.Add(result.GetDouble(0));
                }
                result.Close();
                if (listGram.Count > 0)
                {
                    if (!ingredient.Equals("Caldo de Verduras"))
                    {
                            //double gram = listGram[listGram.Count / 2] * Double.Parse(quantity);
                        double gram = 0;
                        if (quantity.Length == 1)
                        {
                            if (Char.GetUnicodeCategory(Char.Parse(quantity)) == UnicodeCategory.OtherNumber)
                            {
                                if(Math.Abs(CharUnicodeInfo.GetNumericValue(Char.Parse(quantity)) - 0.25) == 0)
                                {
                                    gram = listGram[listGram.Count / 2] * 0.25;
                                        
                                }
                                if (Math.Abs(CharUnicodeInfo.GetNumericValue(Char.Parse(quantity)) - 0.75) == 0)
                                {
                                    gram = listGram[listGram.Count / 2] * 0.75;
                                        
                                }
                                if(Math.Abs(CharUnicodeInfo.GetNumericValue(Char.Parse(quantity)) - 0.5) == 0)
                                {
                                    gram = listGram[listGram.Count / 2] * 0.50;
                                        
                                }
                            }
                            else
                            {
                                if(!quantity.Equals("x")) gram = listGram[listGram.Count / 2] * Double.Parse(quantity);
                            }
                        }
                        else
                        {
                            if (quantity.Length < 3)
                            {
                                gram = listGram[listGram.Count / 2] * Double.Parse(quantity);
                            }
                            else
                            {
                                if (Char.GetUnicodeCategory(quantity[2]) == UnicodeCategory.OtherNumber)
                                {
                                    
                                    gram = listGram[listGram.Count / 2] *
                                           (quantity[0]- '0' + CharUnicodeInfo.GetNumericValue(quantity[2]));
                                    Console.WriteLine(gram + " "+quantity);
                                }
                                else
                                {
                                    if (!quantity.Contains("."))
                                    {
                                        gram = TransformFracGram(quantity,listGram[listGram.Count / 2]);
                                    }
                                    else
                                    {
                                        gram = listGram[listGram.Count / 2] * Double.Parse(quantity[0].ToString());
                                    }
                                }
                            }
                        }
                            
                        string insertRecipeQuantity = $"INSERT INTO nutrifoods.recipe_quantity (recipe_id, ingredient_id, grams) " +
                                                      $"VALUES ({recipeId},{ingredientResult},{gram.ToString().Replace(",",".")}) ON CONFLICT DO NOTHING;";
                        NpgsqlCommand command = new NpgsqlCommand(insertRecipeQuantity, _connection);
                        command.ExecuteNonQuery();
                    }
                }
            }
            else
            {
                if (units.Equals("g") || units.Equals("gr") || units.Equals("grs") || units.Equals("gramos"))
                {
                        
                    string insertRecipeQuantity = $"INSERT INTO nutrifoods.recipe_quantity (recipe_id, ingredient_id, grams) " +
                                                      $"VALUES ({recipeId},{ingredientResult},{quantity}) ON CONFLICT DO NOTHING;";
                    NpgsqlCommand command = new NpgsqlCommand(insertRecipeQuantity, _connection);
                    command.ExecuteNonQuery();
                }
                else
                {
                    if (units.Equals("pote") || units.Equals("potes")) units = "taza";
                    if (units.Contains("grande")) units = "largo";
                    if ((ingredient.Contains("queso") || ingredient.Contains("mozzarella")) && units.Replace("s","").ToLower().Equals("taza"))
                    {
                        ingredientResult = 308;
                        units = "Taza,rallada";
                    }
                    if (ingredient.Equals("queso")) units = "rodaja";
                    if (ingredient.Equals("queso cabra")) units = "oz";
                    if (units.Equals("pqte")) units = "paquete";
                    if ((ingredient.Equals("vino blanco") || ingredient.Equals("vino tinto")) && units.Replace("s", "").ToLower().Equals("taza"))
                        units = "servido";
                    if (ingredient.Equals("pepinillos") && units.Equals("cucharada")) units = "Rodaja";
                    if (ingredient.ToLower().Equals("eneldo")) units = "ramitas";
                    if (ingredient.Equals("pan rallado") && units.Contains("cucharada")) units = "oz";
                    if (ingredient.Equals("Chocolate Amargo") && units.Contains("cucharada")) units = "porcion";
                    if (ingredient.Equals("leche evaporada") && units.Replace("s","").Equals("tarro")) units = "Lata";
                    if (ingredient.Equals("limón") && units.Equals("cucharadita")) units = "Rodaja";
                    if (ingredient.ToLower().Equals("limón") && units.Equals("pica")) units = "Servido";
                    if (units.Replace("s","").Equals("chica")) units = "pequeño";
                    if (ingredient.Equals("tomates") && units.Equals("rodajas")) units = "Rebanada";
                    if (units.Replace("s", "").Equals("rama") || units.Equals("varas")) units = "Tallo";
                    if (ingredient.ToLower().Equals("lechuga") && units.Replace("s","").Equals("hoja")) units = "Hoja,exterior";
                    if (ingredient.ToLower().Equals("tocino")) units = "oz";
                    if (units.Replace("s", "").ToLower().Equals("diente")) units = "Clavo";
                    if (ingredient.Equals("espárragos")) units = "Largo,extra";
                    if (ingredient.ToLower().Equals("pepino") && units.Equals("largo")) units = "Pepino";
                    if (ingredient.Equals("perejil") && (units.Equals("Tallo") || units.Equals("atado"))) units = "ramitas";
                    if (units.Equals("ramita")) units = "Tallo";
                    if (ingredient.Equals("Salmón")) units = "Filete";
                    if (ingredient.Equals("tomillo")) units = "Cucharada,molido";
                    if (ingredient.Replace("s","").Equals("palta") && units.Equals("pequeño")) units = "Servido";
                    if (ingredient.Equals("piña") && units.Equals("lata")) units = "Taza,trozos";
                    if (ingredient.Equals("jamón") && units.Equals("laminas")) units = "Rebanada";
                    string measurisConsult =
                        $"SELECT id FROM nutrifoods.ingredient_measure WHERE ingredient_id = {ingredientResult} " +
                        $"AND nutrifoods.similarity(name,'{units}') > 0.40 LIMIT 1;";
                    NpgsqlCommand commands = new NpgsqlCommand(measurisConsult, _connection);
                    var measurisResult = commands.ExecuteScalar();
                        //Console.WriteLine(units +" == "+measurisResult.GetValue(2) + " | ingrediente "+ingredient);
                    if (measurisResult != null)
                    {
                        if (quantity.Length == 1)
                        {
                            var numDem = TransformUnicode(char.Parse(quantity));
                            if (numDem.Count > 0)
                            {
                                string insertRecipeMeasuris =
                                    $"INSERT INTO nutrifoods.recipe_measure (recipe_id, ingredient_measure_id, integer_part, numerator, denominator) " +
                                    $"VALUES ({recipeId},{measurisResult},{0},{numDem[0]},{numDem[1]}) ON CONFLICT DO NOTHING;";
                                NpgsqlCommand command = new NpgsqlCommand(insertRecipeMeasuris, _connection);
                                command.ExecuteNonQuery();
                            }
                            else
                            {
                                string insertRecipeMeasuris = $"INSERT INTO nutrifoods.recipe_measure (recipe_id, ingredient_measure_id, integer_part, numerator, denominator) " +
                                                              $"VALUES ({recipeId},{measurisResult},{quantity},{0},{0}) ON CONFLICT DO NOTHING;";
                                NpgsqlCommand command = new NpgsqlCommand(insertRecipeMeasuris, _connection);
                                command.ExecuteNonQuery();
                                    
                            }
                        }
                        else
                        {
                            QuantityFracTransform(quantity,recipeId,measurisResult.ToString());
                        }
                    }
                    else
                    {
                        if (units.Equals("kilo") || units.Equals("kilos") || units.Equals("kg")) 
                        { 
                                CalculatedGram(quantity,recipeId,ingredientResult.ToString());
                        }
                        else 
                        { 
                                InsertMLorCC(units,recipeId,ingredientResult.ToString(),quantity,ingredient);
                        }
                    }
                }
            }
        }
    }

    private void QuantityFracTransform(string quantity,string recipeId,string measurisResult)
    {
        if (quantity.Contains("/"))
        {
            if (quantity.Length == 5)
            {
                                        
                int intergerPart = quantity[0] - '0';
                int numerador = quantity[2] - '0';
                int denominador = quantity[4] - '0';
                InsertRecipeMeasuris(recipeId,measurisResult,intergerPart.ToString(),numerador.ToString(),denominador.ToString());
            }
            else
            {
                int numerador = quantity[0] - '0';
                int denominador = quantity[2] - '0';
                InsertRecipeMeasuris(recipeId,measurisResult,0.ToString(),numerador.ToString(),denominador.ToString());
            }
        }
        else
        {
            if (quantity.Contains(" "))
            {
                int intergerPart = quantity[0] - '0';
                var numDem = TransformUnicode(quantity[2]);
                if (numDem.Count > 0)
                {
                    InsertRecipeMeasuris(recipeId,measurisResult.ToString(),intergerPart.ToString(),numDem[0].ToString(),numDem[1].ToString());
                }
            }
            else
            {
                if (quantity.Contains("."))
                {
                    InsertRecipeMeasuris(recipeId,measurisResult.ToString(),quantity.Replace(".5",""),0.ToString(),0.ToString());
                }
                else
                {
                    InsertRecipeMeasuris(recipeId,measurisResult.ToString(),quantity,0.ToString(),0.ToString());
                }
            }
        }
    }

    public void CalculatedGram(string quantity,string recipeId,string ingredientResult)
    {
        double gram = 0; 
        if (quantity.Length == 1)
        {
            if (Char.GetUnicodeCategory(Char.Parse(quantity)) == UnicodeCategory.OtherNumber)
            {
                var list = TransformUnicode(Char.Parse(quantity));
                double frac = Convert.ToDouble(list[0]) / Convert.ToDouble(list[1]);
                gram = 1000 * frac;
                                            
            }
            else
            {
                gram = 1000 * Int32.Parse(quantity);
                                            
            }
        }
        else
        {
            if (quantity.Contains("."))
            {
                gram = 1000 * Double.Parse(quantity.Replace(".",","));
            }
            else
            {
                gram = TransformFracGram(quantity,1000);
            }
        }
        InsertRecipeQuantity(recipeId,ingredientResult,gram.ToString());
    }

    private Double TransformFracGram(string quantity,double grams)
    {
        double gram = 0;
        if (quantity.Contains("/"))
        {
            if (quantity.Length == 5)
            {                       
                double enter = quantity[0] - '0'; 
                double numer = quantity[2] - '0';
                double deno = quantity[4] - '0';
                double frac = enter + (numer / deno);
                gram = grams * frac;
                return gram;
            }
            else
            {
                double numero = quantity[0] - '0';
                double denominador = quantity[2] - '0';
                double frac = numero / denominador;
                gram = grams * frac;
                return gram;
            }
        }
        else
        {
            gram = grams * Int32.Parse(quantity);
            return gram;
        }
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
    
    public void InsertRecipeQuantity(string recipeId,string ingredientResult,string quantity)
    {
        string insertRecipeQuantity = $"INSERT INTO nutrifoods.recipe_quantity (recipe_id, ingredient_id, grams) " +
                                      $"VALUES ({recipeId},{ingredientResult},{quantity}) ON CONFLICT DO NOTHING;";
        NpgsqlCommand command = new NpgsqlCommand(insertRecipeQuantity, _connection);
        command.ExecuteNonQuery();
    }

    public void InsertRecipeMeasuris(string recipeId, string measurisResult, string quantity, string numerator, string denominator)
    {
        string insertRecipeMeasuris = $"INSERT INTO nutrifoods.recipe_measure (recipe_id, ingredient_measure_id, integer_part, numerator, denominator) " +
                                      $"VALUES ({recipeId},{measurisResult},{quantity},{numerator},{denominator}) ON CONFLICT DO NOTHING;";
        NpgsqlCommand command = new NpgsqlCommand(insertRecipeMeasuris, _connection);
        command.ExecuteNonQuery();
    }

    public void InsertMLorCC(string units, string recipeId, string ingredientResult,string quantity, string ingredient)
    {
        if (units.Equals("ml") || units.Equals("cc"))
        {
            InsertRecipeQuantity(recipeId, ingredientResult.ToString(), quantity);
        }
        else
        {
            if (ingredient.Contains("jugo") && !quantity.Equals("x"))
            {
                var id = 5.9 * Int32.Parse(quantity);
                InsertRecipeQuantity(recipeId,ingredientResult.ToString(),id.ToString().Replace(",","."));
            }
        }
    }
}
