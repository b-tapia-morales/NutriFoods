using Npgsql;

namespace Domain.Recipe_insert;

public class Connect
{
    private NpgsqlConnection Connecte()
    {
        NpgsqlConnection connection = new NpgsqlConnection();
        var stringConnection = "Server=localhost;Port=5432;User Id=nutrifoods_dev;Password=MVmYneLqe91$;Database=nutrifoods_db";

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
                Console.WriteLine("Error in establishing connection");
                connection.Close();
                throw;
            }
        }
        return connection;
    }

    public Boolean InsertRecipe()
    {
        var instance = Connecte();
        StreamReader fileRecipe = new StreamReader("Recipe_insert/Recipe/recipe.txt");

        var contador = 0;
        while(!fileRecipe.EndOfStream)
        {
            string line = fileRecipe.ReadLine()!;
            string[] split = line.Split(";");
            var name = split[0];
            var author = split[1];
            var url = split[2];
            var portions = 0;
            var timePreparation = 0;
            contador++;
            if (!split[4].Equals(""))
            {
                portions = Int32.Parse(split[4]);
            }

            if (!split[3].Equals(""))
            {
                timePreparation = Int32.Parse(split[3]);
            }
            Console.WriteLine(contador);
            string query = "INSERT INTO nutrifoods.recipe (name, author, url, portions, preparation_time) VALUES ("+"'"+name+"'"+",'"+author+"','"+url+"',"+portions+","+timePreparation+") ON CONFLICT DO NOTHING";
            NpgsqlCommand cmd = new NpgsqlCommand(query,instance);
            cmd.ExecuteNonQuery();

        }
        instance.Close();
        return true;
    }
}