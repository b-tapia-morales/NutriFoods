using Domain.Models;

namespace API.Genetic;

public class Solutions
{
    public List<Recipe> ListRecipe { get; set; }
    public double CantKilocalories { get; set; }
    public double CantProteins { get; set; }
    public double CantFats { get; set; }
    public double CantCarbohydrates { get; set; }
    public double porcent { get; set; }

    public int fittnes { get; set; }

    public Solutions(int quantity)
    {
        ListRecipe = new List<Recipe>(quantity);
        porcent = 0.1;
        CantCarbohydrates = 0;
        CantFats = 0;
        CantProteins = 0;
        CantKilocalories = 0;
        fittnes = 0;
    }
    
    public void AddRecipe(Recipe r)
    {
        ListRecipe.Add(r);
    }

    private void Reset()
    {
        CantCarbohydrates = 0;
        CantKilocalories = 0;
        CantFats = 0;
        CantProteins = 0;
    }

    public void CalculatFittnes()
    {
        Reset();
        foreach (var recipe in ListRecipe)
        {
            foreach (var nutrient in recipe.RecipeNutrients.ToList())
            {
                switch (nutrient.NutrientId)
                {
                    case 1:
                        CantKilocalories += nutrient.Quantity;
                        break;
                    case 63:
                        CantProteins += nutrient.Quantity;
                        break;
                    case 12:
                        CantFats += nutrient.Quantity;
                        break;
                    case 2:
                        CantCarbohydrates += nutrient.Quantity;
                        break;
                }
            }
        }
    }

    public void Fitness(double userValueCarhohydrates, double userValueProteins, double userValueKilocalories, double userValueFats)
    {
        fittnes = FitnessResult(userValueKilocalories,CantKilocalories) +
                  FitnessResult(userValueProteins,CantProteins)+
                  FitnessResult(userValueFats,CantFats)+
                  FitnessResult(userValueCarhohydrates,CantCarbohydrates);
    }

    private int FitnessResult(double userValue, double cantMicroNutrients )
    {
        if ((userValue * (1 - (porcent/2))) <= cantMicroNutrients && ((userValue) * (1 + (porcent/2))) >= cantMicroNutrients)
        {
            return 2;
        }

        if (((userValue * (1 - porcent) <= cantMicroNutrients && (userValue * (1 - (porcent / 2))) > cantMicroNutrients)) ||
              ((userValue * (1 + (porcent / 2))) < cantMicroNutrients && (userValue * (1 + porcent)) >= cantMicroNutrients))
        {
                return 0;
        }

        if ((userValue * (1 - porcent) > cantMicroNutrients) ||
            (userValue * (1 + porcent) < cantMicroNutrients))
        {
            return -2;
        }

        return 0;
    }

    public void Print()
    {
        Console.WriteLine("  Energy : "+CantKilocalories +"\n"+
                          "  Proteins : "+CantProteins +"\n"+
                          "  Carbohidratos : "+CantCarbohydrates + "\n"+
                          "  Grasas : "+CantFats + "\n");
    }
}