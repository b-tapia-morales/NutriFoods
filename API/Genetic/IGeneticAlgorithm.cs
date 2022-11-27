using API.Dto;
using Utils.Nutrition;
using static Utils.Nutrition.Macronutrient;

namespace API.Genetic;

public interface IGeneticAlgorithm
{
    IList<Chromosome> Solutions { get; }
    IList<Chromosome> Winners { get; }

    DailyMenuDto GenerateSolution(IEnumerable<RecipeDto> recipes, int recipesAmount, double energy,
        double carbohydrates, double lipids, double proteins, int solutionsAmount = 20, double marginOfError = 0.08)
    {
        Solutions.Clear();
        Winners.Clear();
        var menus = GenerateTotalPopulation(recipes);
        GenerateInitialPopulation(recipesAmount, solutionsAmount, menus);
        CalculatePopulationFitness(energy, carbohydrates, lipids, proteins, marginOfError);
        var i = 0;
        while (!SolutionExists())
        {
            Selection();
            Crossover(solutionsAmount);
            Mutation(menus, recipesAmount, solutionsAmount);
            CalculatePopulationFitness(energy, carbohydrates, lipids, proteins, marginOfError);
            i++;
        }

        Console.WriteLine("Generaciones : " + i);
        ShowPopulation();

        return Solutions.First(p => p.Fitness == 8).Recipes;
    }

    DailyMenuDto GenerateSolution(IEnumerable<RecipeDto> recipes, int recipesAmount, double energy,
        int solutionsAmount = 20, double marginOfError = 0.08)
    {
        var (carbohydrates, lipids, proteins) = EnergyDistribution.Calculate(energy);
        return GenerateSolution(recipes, recipesAmount, energy, carbohydrates, lipids, proteins, solutionsAmount,
            marginOfError);
    }

    DailyMenuDto GenerateCustomSolution(IEnumerable<RecipeDto> recipes, int recipesAmount, double energy,
        double carbsPercent, double fatsPercent, double proteinsPercent, int solutionsAmount = 20,
        double marginOfError = 0.08)
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
        return GenerateSolution(recipes, recipesAmount, energy, carbohydrates, lipids, proteins, solutionsAmount,
            marginOfError);
    }

    IList<MenuRecipeDto> GenerateTotalPopulation(IEnumerable<RecipeDto> recipes);

    void GenerateInitialPopulation(int recipesAmount, int solutionsAmount, IList<MenuRecipeDto> menus);

    void CalculatePopulationFitness(double energy, double carbohydrates, double lipids, double proteins,
        double marginOfError);

    bool SolutionExists();

    void Selection();

    void Crossover(int solutionsAmount);

    void Mutation(IList<MenuRecipeDto> menus, int recipesAmount, int solutionsAmount);

    void ShowPopulation();
}