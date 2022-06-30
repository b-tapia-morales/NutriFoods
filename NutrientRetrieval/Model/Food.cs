namespace NutrientRetrieval.Model;

public class Food
{
    public int FdcId { get; set; }
    public string Description { get; set; }
    public FoodNutrient[] FoodNutrients { get; set; }
    public FoodPortion[]? FoodPortions { get; set; }

    public override string ToString()
    {
        return $@"
        {FdcId}
        {Description}
        {string.Join(Environment.NewLine, (IEnumerable<FoodNutrient>)FoodNutrients)}
        {string.Join(Environment.NewLine, (IEnumerable<FoodPortion>)FoodPortions)}
        ";
    }
}