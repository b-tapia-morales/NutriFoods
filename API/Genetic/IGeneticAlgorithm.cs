using API.Dto;

namespace API.Genetic;

public interface IGeneticAlgorithm
{
    IList<PossibleRegime> Solutions { get; }
    IList<PossibleRegime> Winners { get; }

    void CalculatePopulationFitness(double energyTotal, double userValueCarbohydrates, double userValueProteins,
        double userValurFats);

    void GenerateInitialPopulation(int cantRecipes, int cantSolutions, IList<MenuRecipeDto> totalRecipes);

    DailyMenuDto GenerateSolution(int recipeAmount, int solutionsAmount, double energyTotal,
        double carbohydratesPercentage, double lipidsPercentage, double proteinsPercentage);

    void Selection();

    void Crossover(int solutionsAmount);

    void Mutation(IList<MenuRecipeDto> totalRecipes, int recipesAmount, int solutionsAmount);

    bool SolutionExist();
}