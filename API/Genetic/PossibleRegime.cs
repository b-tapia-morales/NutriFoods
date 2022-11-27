using API.Dto;

namespace API.Genetic;

public class PossibleRegime
{
    public DailyMenuDto Recipes { get; }
    public int Fitness { get; private set; }

    public PossibleRegime(IList<MenuRecipeDto> menuRecipe)
    {
        Recipes = new DailyMenuDto
        {
            EnergyTotal = 0,
            CarbohydratesTotal = 0,
            LipidsTotal = 0,
            ProteinsTotal = 0,
            MenuRecipes = menuRecipe
        };
        Fitness = 0;
    }


    public void MacroNutrientCalculation()
    {
        double carbohydrates = 0;
        double lipids = 0;
        double proteins = 0;
        double energy = 0;
        foreach (var recipesMenu in Recipes.MenuRecipes)
        {
            foreach (var nutrient in recipesMenu.Recipe.Nutrients)
            {
                switch (nutrient.Nutrient.Id)
                {
                    case 1:
                        energy += nutrient.Quantity;
                        break;
                    case 2:
                        carbohydrates += nutrient.Quantity;
                        break;
                    case 63:
                        proteins += nutrient.Quantity;
                        break;
                    case 12:
                        lipids += nutrient.Quantity;
                        break;
                }
            }
        }

        Recipes.CarbohydratesTotal = carbohydrates;
        Recipes.EnergyTotal = energy;
        Recipes.LipidsTotal = lipids;
        Recipes.ProteinsTotal = proteins;
    }

    public void CalculateFitness(double userValueCarbohydrates, double userValueProteins, double userValueKilocalories,
        double userValueFats, double marginOfError)
    {
        Fitness = FitnessResult(userValueKilocalories, Recipes.EnergyTotal, marginOfError) +
                  FitnessResult(userValueProteins, Recipes.ProteinsTotal, marginOfError) +
                  FitnessResult(userValueFats, Recipes.LipidsTotal, marginOfError) +
                  FitnessResult(userValueCarbohydrates, Recipes.CarbohydratesTotal, marginOfError);
    }

    private static int FitnessResult(double userValue, double cantMicroNutrients, double marginOfError)
    {
        if ((userValue * (1 - (marginOfError / 2)) <= cantMicroNutrients) &&
            (userValue * (1 + (marginOfError / 2)) >= cantMicroNutrients))
        {
            return 2;
        }

        if (((userValue * (1 - marginOfError) <= cantMicroNutrients) &&
             (userValue * (1 - (marginOfError / 2)) > cantMicroNutrients)) ||
            ((userValue * (1 + (marginOfError / 2)) < cantMicroNutrients) &&
             (userValue * (1 + marginOfError) >= cantMicroNutrients)))
        {
            return 0;
        }

        if (userValue * (1 - marginOfError) > cantMicroNutrients ||
            userValue * (1 + marginOfError) < cantMicroNutrients)
        {
            return -2;
        }

        return 0;
    }

    public void DataString()
    {
        foreach (var recipe in Recipes.MenuRecipes)
        {
            Console.Write($"{recipe.Recipe.Id} ");
        }

        Console.Write($"F = {Fitness}");
    }
}