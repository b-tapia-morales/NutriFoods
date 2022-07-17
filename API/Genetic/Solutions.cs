using Domain.Models;

namespace API.Genetic;

public class Solutions
{
    public Solutions(int quantity)
    {
        ListRecipe = new List<Recipe>(quantity);
        AcceptancePercentage = 0.8;
        CarbohydratesTotal = 0;
        LipidsTotal = 0;
        ProteinsTotal = 0;
        EnergyTotal = 0;
        Fitness = 0;
    }

    public List<Recipe> ListRecipe { get; set; }
    public double EnergyTotal { get; set; }
    private double ProteinsTotal { get; set; }
    private double LipidsTotal { get; set; }
    private double CarbohydratesTotal { get; set; }
    private double AcceptancePercentage { get; }

    public int Fitness { get; set; }

    public void AddGene(Recipe gene)
    {
        ListRecipe.Add(gene);
    }

    public void CalculateFitness()
    {
        EnergyTotal = AccumulateNutrientQuantity(ListRecipe, 108);
        ProteinsTotal = AccumulateNutrientQuantity(ListRecipe, 109);
        LipidsTotal = AccumulateNutrientQuantity(ListRecipe, 11);
        CarbohydratesTotal = AccumulateNutrientQuantity(ListRecipe, 1);
    }

    public void EvaluateFitness(double carbohydratesTarget, double proteinsTarget, double energyTarget,
        double lipidsTarget)
    {
        Fitness = FitnessResult(energyTarget, EnergyTotal) +
                  FitnessResult(proteinsTarget, ProteinsTotal) +
                  FitnessResult(lipidsTarget, LipidsTotal) +
                  FitnessResult(carbohydratesTarget, CarbohydratesTotal);
    }

    private int FitnessResult(double targetQuantity, double actualQuantity)
    {
        if (targetQuantity * (1 - (AcceptancePercentage / 2)) <= actualQuantity &&
            targetQuantity * (1 + (AcceptancePercentage / 2)) >= actualQuantity)
            return +2;

        if ((targetQuantity * (1 - AcceptancePercentage) <= actualQuantity &&
             targetQuantity * (1 - (AcceptancePercentage / 2)) > actualQuantity) ||
            (targetQuantity * (1 + (AcceptancePercentage / 2)) < actualQuantity &&
             targetQuantity * (1 + AcceptancePercentage) >= actualQuantity))
            return 0;

        if (targetQuantity * (1 - AcceptancePercentage) > actualQuantity ||
            targetQuantity * (1 + AcceptancePercentage) < actualQuantity)
            return -2;

        return 0;
    }

    public void Print()
    {
        Console.WriteLine(
            $"Energy: {EnergyTotal}\nProteins: {ProteinsTotal}\nCarbohydrates: {CarbohydratesTotal}\nLipids: {LipidsTotal}\n");
    }

    private static double AccumulateNutrientQuantity(IEnumerable<Recipe> recipes, int id)
    {
        return recipes.Where(e => e.RecipeNutrients.Any(x => x.NutrientId == id)).SelectMany(e => e.RecipeNutrients)
            .Sum(e => e.Quantity);
    }
}