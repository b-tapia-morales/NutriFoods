using Utils.Csv;

namespace Utils.Averages;

public static class RecipeDistribution
{
    private static readonly string FilePathAverage = Path.Combine(
        Directory.GetParent(Directory.GetCurrentDirectory())!.FullName,
        "RecipeInsertion", "DataRecipes", "MacronutrientsAverage.csv");

    private static readonly string FilePathAmount = Path.Combine(
        Directory.GetParent(Directory.GetCurrentDirectory())!.FullName,
        "RecipeInsertion", "DataRecipes", "RecipeAmount.csv");

    private static readonly ICollection<MealTypeRow> MealTypeAverages =
        CsvUtils.RetrieveRows<MealTypeRow, MealTypeMapping>(FilePathAverage).ToList();

    private static readonly IEnumerable<AmountRecipes> RangesAmountRecipes = AmountRecipes.GetAmount(FilePathAmount);

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
            1 => RecipesAmount(energy, 550, averageAmount, mealTypeRow),
            2 => RecipesAmount(energy, 450, averageAmount, mealTypeRow),
            3 => RecipesAmount(energy, 300, averageAmount, mealTypeRow),
            _ => RecipesAmount(energy, 50, averageAmount, mealTypeRow)
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

    private static int RecipesAmount(double energy, double maximumLowerLimit, double averageAmount,
        MealTypeRow mealType)
    {
        var amountRecipesMealType = RangesAmountRecipes.First(x => x.MealTypeValue == mealType.MealTypeValue);
        for (var i = 0; i < amountRecipesMealType.RangesAmount.ToList().Count; i++)
        {
            var level = amountRecipesMealType.RangesAmount.ToList()[i];
            for (var j = 0; j < level.Count; j++)
            {
                if (energy >= level[j].lower && energy < level[j].superior) return i + 3;
            }
        }

        if (energy <= maximumLowerLimit) return 3;
        var recipesAmount = (int)Math.Round(averageAmount);
        return recipesAmount < 2 ? 2 : recipesAmount;
    }
}

/*
por si las dudas if (energy <= limitTwo && energy > limitsThree)
         {
             switch (mealType.MealTypeValue)
             {
                 case 2 when (energy >= 730 && energy <= 980) || (energy >= 600 && energy <= 610):
                 case 1 when energy >= 548 && energy <= 870:
                 case 0 when energy <= 355 && energy > 170:
                 case 3 when energy <= 740 && energy > 610 || energy <= 390 && energy > 305 ||
                             energy > 390 && energy <= 435 || energy >= 520 && energy < 545:
                     return 3;
                 case 0 when energy <= 170 && energy > 145:
                     return 4;
                 case 0 when energy <= 145 && energy > 130:
                     return 5;
                 default:
                     return 2;
             }
         }*/