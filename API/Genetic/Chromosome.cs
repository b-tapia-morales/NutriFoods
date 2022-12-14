using API.Dto;

namespace API.Genetic;

public class Chromosome
{
    public DailyMenuDto DailyMenu { get; }
    public int Fitness { get; private set; }

    public Chromosome(IList<MenuRecipeDto> menuRecipe)
    {
        DailyMenu = new DailyMenuDto
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
        DailyMenu.EnergyTotal = AggregateMacronutrients(DailyMenu, 1);
        DailyMenu.CarbohydratesTotal = AggregateMacronutrients(DailyMenu, 2);
        DailyMenu.LipidsTotal = AggregateMacronutrients(DailyMenu, 12);
        DailyMenu.ProteinsTotal = AggregateMacronutrients(DailyMenu, 63);
    }

    public void UpdateFitness(double energy, double carbohydrates, double lipids, double proteins,
        double marginOfError)
    {
        Fitness = CalculateFitness(energy, DailyMenu.EnergyTotal, marginOfError) +
                  CalculateFitness(carbohydrates, DailyMenu.CarbohydratesTotal, marginOfError) +
                  CalculateFitness(lipids, DailyMenu.LipidsTotal, marginOfError) +
                  CalculateFitness(proteins, DailyMenu.ProteinsTotal, marginOfError);
    }

    private static double AggregateMacronutrients(DailyMenuDto dailyMenu, int nutrientId) =>
        dailyMenu.MenuRecipes
            .SelectMany(e => e.Recipe.Nutrients)
            .Where(e => e.Nutrient.Id == nutrientId)
            .Sum(e => e.Quantity);

    private static int CalculateFitness(double objectiveValue, double menuValue, double marginOfError)
    {
        if (objectiveValue * (1 - marginOfError / 2) <= menuValue &&
            objectiveValue * (1 + marginOfError / 2) >= menuValue)
            return +2;

        if (objectiveValue * (1 - marginOfError) <= menuValue &&
            objectiveValue * (1 - marginOfError / 2) > menuValue ||
            objectiveValue * (1 + marginOfError / 2) < menuValue &&
            objectiveValue * (1 + marginOfError) >= menuValue)
            return +0;

        if (objectiveValue * (1 - marginOfError) > menuValue ||
            objectiveValue * (1 + marginOfError) < menuValue)
            return -2;

        return +0;
    }

    public void ShowPhenotypes()
    {
        foreach (var recipe in DailyMenu.MenuRecipes)
        {
            Console.Write($"{recipe.Recipe.Id} ");
        }

        Console.WriteLine($"F = {Fitness}");
    }
}