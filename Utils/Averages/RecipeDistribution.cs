using Utils.Csv;

namespace Utils.Averages;

public static class RecipeDistribution
{
    private static readonly string FilePath = Path.Combine(
        Directory.GetParent(Directory.GetCurrentDirectory())!.FullName,
        "RecipeInsertion", "DataRecipes", "MacronutrientsAverage.csv");

    private static readonly ICollection<MealTypeRow> MealTypeAverages =
        RowRetrieval.RetrieveRows<MealTypeRow, MealTypeMapping>(FilePath).ToList();

    public static int CalculateRecipesAmount(double energy, double carbohydrates, double lipids, double proteins,
        int mealTypeValue)
    {
        var row = FindRow(mealTypeValue);
        return CalculateRecipesAmount(row, energy, carbohydrates, lipids, proteins);
    }

    private static MealTypeRow FindRow(int mealTypeValue) =>
        MealTypeAverages.First(e => e.MealTypeValue == mealTypeValue);

    private static int CalculateRecipesAmount(MealTypeRow mealTypeRow, double energy, double carbohydrates,
        double lipids, double proteins)
    {
        var averageAmount = (energy / mealTypeRow.Energy + carbohydrates / mealTypeRow.Carbohydrates +
                             lipids / mealTypeRow.Lipids + proteins / mealTypeRow.Proteins) / 4;
        var recipesAmount = (int)Math.Round(averageAmount);
        return mealTypeRow.MealTypeValue switch
        {
            1 => RecipesAmount(energy, 875, 632, averageAmount),
            2 => RecipesAmount(energy, 530, 450,averageAmount),
            3 => RecipesAmount(energy, 565, 616,averageAmount),
            _ => recipesAmount < 2 ? 2 : recipesAmount
        };
    }

    public static (double EnergyLimits, double CarbohydratesLimits, double LipidsLimits, double ProteinsLimits)
        CalculateDistributionLimits(double energyTarget, double carbohydrates, double lipids,
            double proteins)
    {
        var limits = (energyTarget * 6 / 8, carbohydrates * 6 / 8,
            lipids * 6 / 8, proteins * 6 / 8);
        return limits;
    }

    private static int RecipesAmount(double energy, double limitTwo, double limitsThree, double averageAmount)
    {
        if (energy <= limitTwo && energy > limitsThree) return 2;
        if (energy <= limitsThree) return 3;
        var recipesAmount = (int)Math.Round(averageAmount);
        return recipesAmount < 2 ? 2 : recipesAmount;
    }
}