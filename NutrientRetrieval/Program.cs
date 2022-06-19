
using NutrientRetrieval.Request;

var request = new DataCentral("aLGkW4nbdeEhoFefi68nOYLNPaSXhiSjO7bIBzQk");
var a = request.FoodRequest("170186");

Console.WriteLine(a.Description);
foreach (var item in a.FoodNutrients)
{
    Console.WriteLine("nutriente: " + item.Name +  " " + "ID: " + item.Number);

}