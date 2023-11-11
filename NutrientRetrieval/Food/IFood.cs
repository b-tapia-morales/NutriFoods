namespace NutrientRetrieval.Food;

public interface IFood<out T> where T : class, IFoodNutrient
{
    int FdcId { get; }
    string Description { get; }
    T[] FoodNutrients { get; }
}