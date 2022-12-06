namespace Utils.Nutrition;

public static class DistributionRecipes
{
    private static readonly ICollection<double> AverageBreakfasts = AverageMacronutrients(0);
    private static readonly ICollection<double> AverageLunch = AverageMacronutrients(1);
    private static readonly ICollection<double> AverageDinner = AverageMacronutrients(2);
    private static readonly ICollection<double> AverageBrunch = AverageMacronutrients(3);

    public static int GetAmountOfRecipes(double energy, double carbohydrates, double lipids, double proteins,
        int kindOfFood)
    {
        return kindOfFood switch
        {
            1 => GetAmountRecipes(AverageBreakfasts, energy, carbohydrates, lipids, proteins),
            2 => GetAmountRecipes(AverageLunch, energy, carbohydrates, lipids, proteins),
            3 => GetAmountRecipes(AverageDinner, energy, carbohydrates, lipids, proteins),
            4 => GetAmountRecipes(AverageBrunch,energy,carbohydrates,lipids,proteins),
            _ => 0
        };
    }

    private static ICollection<double> AverageMacronutrients(int kindOfFood)
    {
        var pathAvereage = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.FullName,
            "RecipeInsertion", "DataRecipes", "avereageMacronutrients.csv");
 
        var averages = File.ReadAllLines(pathAvereage).Select(x => x.Split(";")).ToList();

        var energyAverage = Convert.ToDouble(averages[kindOfFood][0]);
        var carboAverage = Convert.ToDouble(averages[kindOfFood][1]);
        var proteyAverage = Convert.ToDouble(averages[kindOfFood][2]);
        var lipAverage = Convert.ToDouble(averages[kindOfFood][3]);

        var averageMacro = new List<double>
        {
            energyAverage,
            carboAverage,
            proteyAverage,
            lipAverage
        };

        return averageMacro;
    }

    private static int GetAmountRecipes(ICollection<double> averageRecipes, double energy, double carbohydrates,
        double lipids, double proteins)
    {
        var cantRecipes = (energy / averageRecipes.ToList()[0] + carbohydrates / averageRecipes.ToList()[1] +
                          lipids / averageRecipes.ToList()[3] + proteins / averageRecipes.ToList()[2]) /4;
        if (cantRecipes < 2.0) return 2;
        return (int) Math.Round(cantRecipes);

    }
}