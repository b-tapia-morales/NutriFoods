namespace NutrientRetrieval.Food;

public interface IFoodNutrient
{
    string Number { get; }
    string Name { get; }
    double Amount { get; }
    string UnitName { get; }
}