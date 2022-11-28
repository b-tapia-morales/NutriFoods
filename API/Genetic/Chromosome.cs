using API.Dto;

namespace API.Genetic;

public class Chromosome
{
    public DailyMenuDto Recipes { get; }
    public int Fitness { get; private set; }

    public Chromosome(IList<MenuRecipeDto> menuRecipe)
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


    public void AggregateMacronutrients()
    {
        double energy = 0;
        double carbohydrates = 0;
        double lipids = 0;
        double proteins = 0;
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
                    case 12:
                        lipids += nutrient.Quantity;
                        break;
                    case 63:
                        proteins += nutrient.Quantity;
                        break;
                }
            }
        }

        Recipes.CarbohydratesTotal = carbohydrates;
        Recipes.EnergyTotal = energy;
        Recipes.LipidsTotal = lipids;
        Recipes.ProteinsTotal = proteins;
    }

    public void CalculateFitness(double energy, double carbohydrates, double lipids, double proteins,
        double marginOfError)
    {
        Fitness = FitnessResult(energy, Recipes.EnergyTotal, marginOfError) +
                  FitnessResult(carbohydrates, Recipes.CarbohydratesTotal, marginOfError) +
                  FitnessResult(lipids, Recipes.LipidsTotal, marginOfError) +
                  FitnessResult(proteins, Recipes.ProteinsTotal, marginOfError);
    }

    private static int FitnessResult(double objectiveValue, double menuValue, double marginOfError)
    {
        if ((objectiveValue * (1 - (marginOfError / 2)) <= menuValue) &&
            (objectiveValue * (1 + (marginOfError / 2)) >= menuValue))
        {
            return 2;
        }

        if (((objectiveValue * (1 - marginOfError) <= menuValue) &&
             (objectiveValue * (1 - (marginOfError / 2)) > menuValue)) ||
            ((objectiveValue * (1 + (marginOfError / 2)) < menuValue) &&
             (objectiveValue * (1 + marginOfError) >= menuValue)))
        {
            return 0;
        }

        if (objectiveValue * (1 - marginOfError) > menuValue ||
            objectiveValue * (1 + marginOfError) < menuValue)
        {
            return -2;
        }

        return 0;
    }

    public void ShowPhenotypes()
    {
        foreach (var recipe in Recipes.MenuRecipes)
        {
            Console.Write($"{recipe.Recipe.Id} ");
        }

        Console.WriteLine($"F = {Fitness}");
    }
}