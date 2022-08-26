namespace API.Genetic;

public interface IGeneticAlgorithm
{
    void CalculatePopulationFitness();
    void GenerateInitialPopulation();
    void Selection();
    void Crossover();
    void Mutation();
    void UpdatePopulationFitness();

    void SolutionExist();

}