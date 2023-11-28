namespace NutrientRetrieval.Food;

public interface IFood<out T> where T : class, IFoodNutrient
{
    int FdcId { get; }
    string Description { get; }
    T[] FoodNutrients { get; }
}

public interface IFoodNutrient
{
    string Number { get; }
    string Name { get; }
    double Amount { get; }
    string UnitName { get; }
}