using API.Dto;
using Utils.Nutrition;
using static Utils.Nutrition.Macronutrient;

namespace API.Genetic;

public interface IGeneticAlgorithm
{
    IList<PossibleRegime> Solutions { get; }
    IList<PossibleRegime> Winners { get; }

    void CalculatePopulationFitness(double energy, double carbohydrates, double lipids, double proteins,
        double marginOfError);

    void GenerateInitialPopulation(int recipesAmount, int solutionsAmount, IList<MenuRecipeDto> totalRecipes);

    DailyMenuDto GenerateSolution(int recipesAmount, double energy, double carbohydrates, double lipids,
        double proteins, int solutionsAmount = 20, double marginOfError = 0.08);

    DailyMenuDto GenerateSolution(int recipesAmount, double energy, int solutionsAmount = 20,
        double marginOfError = 0.08)
    {
        var (carbohydrates, lipids, proteins) = EnergyDistribution.Calculate(energy);
        return GenerateSolution(recipesAmount, energy, carbohydrates, lipids, proteins, solutionsAmount, marginOfError);
    }

    DailyMenuDto GenerateCustomSolution(int recipesAmount, double energy, double carbsPercent, double fatsPercent,
        double proteinsPercent, int solutionsAmount = 20, double marginOfError = 0.08)
    {
        carbsPercent = Math.Round(carbsPercent, 2);
        fatsPercent = Math.Round(fatsPercent, 2);
        proteinsPercent = Math.Round(proteinsPercent, 2);
        if (carbsPercent < Carbohydrates.MinPercent || fatsPercent < Lipids.MinPercent ||
            proteinsPercent < Proteins.MinPercent)
            throw new ArgumentException("One of the percentages is below the threshold");
        if (carbsPercent > Carbohydrates.MaxPercent || fatsPercent > Lipids.MaxPercent ||
            proteinsPercent > Proteins.MaxPercent)
            throw new ArgumentException("One of the percentages is above the threshold");
        if (Math.Abs(1 - (carbsPercent + fatsPercent + proteinsPercent)) >= 1e-1)
            throw new ArgumentException("The sum of the percentages do not equal to one");
        var (carbohydrates, lipids, proteins) =
            EnergyDistribution.Calculate(energy, carbsPercent, fatsPercent, proteinsPercent);
        return GenerateSolution(recipesAmount, energy, carbohydrates, lipids, proteins, solutionsAmount, marginOfError);
    }

    void Selection();

    void Crossover(int solutionsAmount);

    void Mutation(IList<MenuRecipeDto> totalRecipes, int recipesAmount, int solutionsAmount);

    bool SolutionExists();
}