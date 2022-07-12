using API.Dto;

namespace API.Genetic;

public class Solutions
{
    public List<RecipeDto> ListRecipe { get; set; }
    private double CantKilocalories { get; set; }
    private double CantProteins { get; set; }
    private double CantFats { get; set; }
    private double CantCarbohydrates { get; set; }

    public Solutions(int quantity)
    {
        ListRecipe = new List<RecipeDto>(quantity);
        CantCarbohydrates = 0;
        CantFats = 0;
        CantProteins = 0;
        CantKilocalories = 0;
    }
    
    public void AddRecipe(RecipeDto r)
    {
        // al insertar la receta se le suma el total que contiene con los 4 elementos y se ingresa
        CantCarbohydrates = 0;
        CantKilocalories = 0;
        CantProteins = 0;
        CantFats = 0;
        ListRecipe.Add(r);
    }

    public int Fitness(double porcentCarbohydrates, double porcentProteins, double porcentKiloCalories,
        double porcentFats,
        double userValueCarhohydrates, double userValueProteins, double userValueKilocalories, double userValueFats)
    {
        return FitnessResult(porcentKiloCalories, userValueKilocalories,CantKilocalories) +
               FitnessResult(porcentProteins, userValueProteins,CantProteins)+
               FitnessResult(porcentFats,userValueFats,CantFats)+
               FitnessResult(porcentCarbohydrates,userValueCarhohydrates,CantCarbohydrates);
    }

    private int FitnessResult(double porcent, double userValue, double cantMicroNutrients )
    {
        if ((userValue * (1 - (porcent/2))) <= cantMicroNutrients &&
            (userValue) * (1 + (porcent/2)) >= cantMicroNutrients)
        {
            return 2;
        }
        if (((userValue * (1 - porcent) <= cantMicroNutrients &&
              (userValue * (1 - (porcent / 2))) > cantMicroNutrients)) ||
            ((userValue * (1 + (porcent / 2))) < cantMicroNutrients &&
             (userValue * (1 + porcent)) < cantMicroNutrients))
        {
                return 0;
        }
          
        if ((userValue * (1 - porcent) > cantMicroNutrients) &&
            (userValue * (1 + porcent) < cantMicroNutrients))
        {
            return -2;
        }

        return 0;
    }
}