using Utils.Csv;
using Utils.Enum;

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
        return mealTypeRow.MealTypeValue switch
        {
            1 => RecipesAmount(energy, 870, 550, averageAmount, mealTypeRow),
            2 => RecipesAmount(energy, 900, 450, averageAmount, mealTypeRow),
            3 => RecipesAmount(energy, 565, 616, averageAmount, mealTypeRow),
            _ => RecipesAmount(energy, 240, 50, averageAmount, mealTypeRow)
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

    private static int RecipesAmount(double energy, double limitTwo, double limitsThree, double averageAmount,
        MealTypeRow mealType)
    {
        if (energy <= limitTwo && energy > limitsThree)
        {
            switch (mealType.MealTypeValue)
            {
                case 2 when (energy >= 845 && energy <= 860) || (energy >= 600 && energy <= 610):
                case 1 when energy >= 551 && energy <= 860:
                case 0 when energy <= 240 && energy > 160:
                    return 3;
                case 0 when (energy <= 160):
                    return 4;
                default:
                    return 2;
            }
        }

        if (energy <= limitsThree) return 3;
        var recipesAmount = (int)Math.Round(averageAmount);
        return recipesAmount < 2 ? 2 : recipesAmount;
    }
}