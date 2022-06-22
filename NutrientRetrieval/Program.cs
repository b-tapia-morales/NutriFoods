using Application.Utils.Input;
using Application.Utils.Nutrition;

var gender = Input.ReadInteger("Ingrese su sexo", 1, 2);
var age = Input.ReadInteger("Ingrese su edad", 18, 60);
var weight = Input.ReadInteger("Ingrese su peso (en [Kg])", 50, 200);
var height = Input.ReadInteger("Ingrese su altura (en [cm])", 150, 200);
var physicalActivity = Input.ReadInteger("Ingrese su nivel de actividad física", 1, 4);
var get = TotalMetabolicRate.Calculate(gender, weight, height, age, physicalActivity);
Console.WriteLine($"Su GET es: {Math.Round(get, 2)}");