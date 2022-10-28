using API.Dto;

namespace API.Genetic;

public interface IGeneticAlgorithm
{
    void CalculatePopulationFitness(double energyTotal, double userValueCarbohydrates,double userValueProteins, double userValurFats);
    void GenerateInitialPopulation(int cantRecipes, ICollection<MealMenuRecipeDto> totalRecipes);
    void Selection();
    void Crossover();
    void Mutation(ICollection<MealMenuRecipeDto> totalRecipes);
    bool SolutionExist();

}