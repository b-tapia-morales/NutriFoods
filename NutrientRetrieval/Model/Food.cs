namespace NutrientRetrieval.Model;

public class Food
{
    public int FdcId { get; set; }
    public string Description { get; set; }
    public FoodNutrient[] FoodNutrients { get; set; }
}