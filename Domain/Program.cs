
/*var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
*/

using Domain.Recipe_insert;

Connect connect = new Connect();
connect.InsertRecipe();