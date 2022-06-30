using NutrientRetrieval.Request;

var food = DataCentral.FoodRequest("170186");

Console.WriteLine(food);
/*foreach (var item in food.FoodNutrients)
{
    Console.WriteLine($"Nutriente: {item.Name}\t\t\t\tID: {item.Number}");
}*/

foreach (var item in food.FoodPortions)
{
    Console.WriteLine($"Gramos: {item.GramWeight}\t\t\t\tCantidad: {item.Amount}\t\t\t\tMedida: {item.Modifier}");
}