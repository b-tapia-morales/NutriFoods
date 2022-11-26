using API.Dto;

namespace API.Genetic;

public class PossibleRegime
{
    public DailyMenuDto Recipes { get; }
    private const double Percent = 0.08;
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
        double userValueFats)
    {
        Fitness = FitnessResult(userValueKilocalories, Recipes.EnergyTotal) +
                  FitnessResult(userValueProteins, Recipes.ProteinsTotal) +
                  FitnessResult(userValueFats, Recipes.LipidsTotal) +
                  FitnessResult(userValueCarbohydrates, Recipes.CarbohydratesTotal);
    }

    private static int FitnessResult(double userValue, double cantMicroNutrients)
    {
        if ((userValue * (1 - (Percent / 2)) <= cantMicroNutrients) &&
            (userValue * (1 + (Percent / 2)) >= cantMicroNutrients))
        {
            return 2;
        }

        if (((userValue * (1 - Percent) <= cantMicroNutrients) &&
             (userValue * (1 - (Percent / 2)) > cantMicroNutrients)) ||
            ((userValue * (1 + (Percent / 2)) < cantMicroNutrients) &&
             (userValue * (1 + Percent) >= cantMicroNutrients)))
        {
            return 0;
        }

        if (userValue * (1 - Percent) > cantMicroNutrients ||
            userValue * (1 + Percent) < cantMicroNutrients)
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