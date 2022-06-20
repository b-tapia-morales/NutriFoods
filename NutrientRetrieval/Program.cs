using NutrientRetrieval;
using NutrientRetrieval.Dictionaries;
using NutrientRetrieval.Request;

var ingredientIds = IngredientDictionary.CreateDictionaryIds();
ingredientIds.Select(e => $"{e.Key} : {e.Value}").ToList().ForEach(Console.WriteLine);
var foods = DataCentral.FoodRequest();
foods.Select(e => $"{e.Key}\n{e.Value}").ToList().ForEach(Console.WriteLine);
ApiRetrieval.InsertNutrients();