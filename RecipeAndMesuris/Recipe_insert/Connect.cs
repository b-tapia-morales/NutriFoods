using Npgsql;

namespace RecipeAndMesuris.Recipe_insert;

public static class Connect
{
    private static NpgsqlConnection Connecte()
    {
        NpgsqlConnection connection = new NpgsqlConnection();
        var stringConnection =
            "Server=localhost;Port=5432;User Id=nutrifoods_dev;Password=MVmYneLqe91$;Database=nutrifoods_db";

        if (!string.IsNullOrWhiteSpace(stringConnection))
        {
            try
            {
                connection = new NpgsqlConnection(stringConnection);
                connection.Open();
                Console.WriteLine("Connected");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error in establishing connection{e.Message}");
                connection.Close();
                throw;
            }
        }

        return connection;
    }

    public static void InsertRecipe()
    {
        var directory = Directory.GetParent(Directory.GetCurrentDirectory())!.FullName;
        var path = Path.Combine(directory, "RecipeAndMesuris", "Recipe_insert", "Recipe", "recipe.txt");
        var instance = Connecte();
        using var fileRecipe = new StreamReader(path);
        while (!fileRecipe.EndOfStream)
        {
            string line = fileRecipe.ReadLine()!;
            string[] split = line.Split(";");
            var name = split[0];
            var author = split[1];
            var url = split[2];
            var portions = 0;
            var timePreparation = 0;
            if (!split[4].Equals(""))
            {
                portions = Int32.Parse(split[4]);
            }

            if (!split[3].Equals(""))
            {
                timePreparation = Int32.Parse(split[3]);
            }

            string query =
                $"INSERT INTO nutrifoods.recipe (name, author, url, portions, preparation_time) VALUES ('{name}','{author}','{url}',{portions},{timePreparation}) ON CONFLICT DO NOTHING";
            NpgsqlCommand cmd = new NpgsqlCommand(query, instance);
            cmd.ExecuteNonQuery();
        }

        instance.Close();
        Console.WriteLine("Insert Recipe Correct!");
    }

    public static void InsertMeasuris()
    {
        var instance = Connecte();
        var directory = Directory.GetParent(Directory.GetCurrentDirectory())!.FullName;
        var path = Path.Combine(directory, "RecipeAndMesuris", "Recipe_insert", "Ingredient", "measures",
            "measures_id.csv");
        using var fileIngredient = new StreamReader(path);
        while (!fileIngredient.EndOfStream)
        {
            string line = fileIngredient.ReadLine() ?? throw new InvalidOperationException();
            string[] nameIngredient = line.Split(";");
            string queryIngredient =
                $"SELECT id FROM nutrifoods.ingredient WHERE lower(nutrifoods.ingredient.name) ='{nameIngredient[1].ToLower()}';";
            NpgsqlCommand comand = new NpgsqlCommand(queryIngredient, instance);
            var result = comand.ExecuteScalar();

            if (!nameIngredient[2].Equals("") )
            {
                var measuresPath = Path.Combine(directory, "RecipeAndMesuris", "Recipe_insert", "Ingredient",
                    "measures", "ingredient_measures", $"{nameIngredient[1]}.csv");
                using var fileMeasures = new StreamReader(measuresPath);
                while (!fileMeasures.EndOfStream)
                {
                    string lineMeasury = fileMeasures.ReadLine() ?? throw new InvalidOperationException();
                    string[] data = lineMeasury.Split(";");
                    string queryMeasury =
                        $"INSERT INTO nutrifoods.ingredient_measure (ingredient_id, name, grams) VALUES ({result},{data[0]},{data[1]})ON CONFLICT DO NOTHING;";
                    NpgsqlCommand commandMeasury = new NpgsqlCommand(queryMeasury, instance);
                    commandMeasury.ExecuteNonQuery();
                }
                fileMeasures.Close();
            }
        }

        fileIngredient.Close();
        instance.Close();
        Console.WriteLine("Insert Measures Ingredient Correct!");
    }


    public static void InsertRecipeIngredient()
    {
        var instance = Connecte();
        IngredientRecipe i = new IngredientRecipe(instance);
        i.ReadInsertRecipeIngredient();
    }
}