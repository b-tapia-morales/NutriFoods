using API.Dto;
using API.Recipes;

namespace API.Genetic;

public class Regime : IGeneticAlgorithm
{
    private readonly IRecipeRepository _repository;
    private static readonly ICollection<MealMenuDto> Solutions = new List<MealMenuDto>();
    private static readonly ICollection<MealMenuDto> Winners = new List<MealMenuDto>();

    
    public Regime(IRecipeRepository recipeRepository)
    {
        _repository = recipeRepository;
    }
    public MealPlanDto GenerateSolution(int recipeAmount, double energyTotal, double carbohydratesPercentage, double lipidsPercentage, MealTypeDto mealType)
    {
        return null;
    }
    public void CalculatePopulationFitness()
    {
        throw new NotImplementedException();
    }

    public void GenerateInitialPopulation()
    {
        throw new NotImplementedException();
    }

    public void Selection()
    {
        throw new NotImplementedException();
    }

    public void Crossover()
    {
        throw new NotImplementedException();
    }

    public void Mutation()
    {
        throw new NotImplementedException();
    }

    public void UpdatePopulationFitness()
    {
        throw new NotImplementedException();
    }

    public void SolutionExist()
    {
        throw new NotImplementedException();
    }
}