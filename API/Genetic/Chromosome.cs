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
        Recipes.EnergyTotal = AggregateMacronutrients(Recipes, 1);
        Recipes.CarbohydratesTotal = AggregateMacronutrients(Recipes, 2);
        Recipes.LipidsTotal = AggregateMacronutrients(Recipes, 12);
        Recipes.ProteinsTotal = AggregateMacronutrients(Recipes, 63);
    }

    public void UpdateFitness(double energy, double carbohydrates, double lipids, double proteins,
        double marginOfError)
    {
        Fitness = CalculateFitness(energy, Recipes.EnergyTotal, marginOfError) +
                  CalculateFitness(carbohydrates, Recipes.CarbohydratesTotal, marginOfError) +
                  CalculateFitness(lipids, Recipes.LipidsTotal, marginOfError) +
                  CalculateFitness(proteins, Recipes.ProteinsTotal, marginOfError);
    }

    private static double AggregateMacronutrients(DailyMenuDto dailyMenu, int nutrientId) =>
        dailyMenu.MenuRecipes
            .SelectMany(e => e.Recipe.Nutrients)
            .Where(e => e.Nutrient.Id == nutrientId)
            .Sum(e => e.Quantity);

    private static int CalculateFitness(double objectiveValue, double menuValue, double marginOfError)
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