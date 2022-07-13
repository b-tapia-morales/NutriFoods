using Npgsql;

namespace Domain.DatabaseInitialization;

public static class DatabaseInitialization
{
    private const string ConnectionString =
        "Server=localhost;Port=5432;User Id=nutrifoods_dev;Password=MVmYneLqe91$;Database=nutrifoods_db";

    private static readonly string BaseDirectory = Directory.GetParent(Directory.GetCurrentDirectory())!.FullName;
    private static readonly string DomainDirectory = Path.Combine(BaseDirectory, "Domain");
    private static readonly string SchemaDirectory = Path.Combine(DomainDirectory, "Schema");
    private static readonly string CreateDirectory = Path.Combine(SchemaDirectory, "Create");
    private static readonly string InsertDirectory = Path.Combine(SchemaDirectory, "Insert");
    private static readonly string EnumerationsDirectory = Path.Combine(InsertDirectory, "Enumerations");

    public static void Initialize()
    {
        var schemaScript = Path.Combine(CreateDirectory, "schema.sql");
        var enumerationScript = Path.Combine(EnumerationsDirectory, "food_group__diet__meal_and_dish_type.sql");
        var nutrientScript = Path.Combine(EnumerationsDirectory, "nutrient.sql");
        var ingredientScript = Path.Combine(InsertDirectory, "ingredients.sql");
        var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        using var schemaCommand = new NpgsqlCommand(File.ReadAllText(schemaScript), connection);
        schemaCommand.ExecuteNonQuery();
        using var enumerationCommand = new NpgsqlCommand(File.ReadAllText(enumerationScript), connection);
        enumerationCommand.ExecuteNonQuery();
        using var nutrientCommand = new NpgsqlCommand(File.ReadAllText(nutrientScript), connection);
        nutrientCommand.ExecuteNonQuery();
        using var ingredientCommand = new NpgsqlCommand(File.ReadAllText(ingredientScript), connection);
        ingredientCommand.ExecuteNonQuery();
        connection.Close();
    }
}