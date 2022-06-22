using Application.Utils.Input;
using Application.Utils.Nutrition;

namespace Application;

public static class Program
{
    public static void Main(string[] args)
    {
        var gender = Input.ReadInteger("Ingrese su sexo", 1, 2);
        var age = Input.ReadInteger("Ingrese su edad", 18, 60);
        var weight = Input.ReadInteger("Ingrese su peso (en [Kg])", 50, 200);
        var height = Input.ReadInteger("Ingrese su peso (en [cm])", 150, 200);
        var physicalActivity = Input.ReadInteger("Ingrese su nivel de actividad física", 1, 4);
        Console.WriteLine($"Su GET es: {1}", TotalMetabolicRate.Calculate(gender, weight, height, age, physicalActivity));
    }
}