namespace Utils.Averages;

public class AmountRecipes
{
    public int MealTypeValue { get; set; }
    public ICollection<IList<(double lower, double superior)>> RangesAmount { get; set; } = null!;

    public static IEnumerable<AmountRecipes> GetAmount(string path)
    {
        var file = File.ReadAllLines(path).Select(x => x.Split(";"));
        var amountRecipes = new List<AmountRecipes>();
        foreach (var line in file)
        {
            var rangesAmount = new List<IList<(double lower, double superior)>>();
            for (var i = 1; i < line.Length; i++)
            {
                var rangesList = line[i].Split(":");
                var ranges = rangesList.Select(values => values.Split(","))
                    .Select(value => (double.Parse(value[0]), double.Parse(value[1]))).ToList();
                rangesAmount.Add(ranges);
            }
            var amountRecipe = new AmountRecipes
            {
                MealTypeValue = int.Parse(line[0]),
                RangesAmount = rangesAmount
            };
            amountRecipes.Add(amountRecipe);
        }

        return amountRecipes;
    }
}