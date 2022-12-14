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
        var recipesAmount = (int) Math.Round(averageAmount);
        return recipesAmount < 2 ? 2 : recipesAmount;
    }
}