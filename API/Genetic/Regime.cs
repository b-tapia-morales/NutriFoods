using API.Dto;
using API.Recipes;
using Domain.Models;

namespace API.Genetic;

public class Regime : IGeneticAlgorithm
{
    private readonly IRecipeRepository _repository;
    private readonly Random _r = new Random(Environment.TickCount);
    public IList<PossibleRegime> Solutions { get; private set; }
    public IList<PossibleRegime> Winners { get; private set; }

    public Regime(IRecipeRepository recipeRepository)
    {
        _repository = recipeRepository;
        Solutions = new List<PossibleRegime>();
        Winners = new List<PossibleRegime>();
    }

    public MealMenuDto GenerateSolution(int recipeAmount, int solutionsAmount, double energyTotal, double carbohydratesPercentage,
        double lipidsPercentage, double proteinsPercentage, MealType mealType)
    {
        Solutions.Clear();
        Winners.Clear();
        var recipes = GetUniverseRecipes(mealType.Id);
        GenerateInitialPopulation(recipeAmount, solutionsAmount, recipes);
        CalculatePopulationFitness(energyTotal, carbohydratesPercentage, proteinsPercentage,
            lipidsPercentage);
        var ite = 0;
        while (!SolutionExist())
        {
            Selection();
            Crossover(solutionsAmount);
            Mutation(recipes, recipeAmount, solutionsAmount);
            CalculatePopulationFitness(energyTotal, carbohydratesPercentage, proteinsPercentage,
            lipidsPercentage);
            ite++;
        }
        Console.WriteLine("Generaciones : "+ite);
        String();

        return Solutions.First(p => p.Fitness == 8).Recipes;
    }

    public void CalculatePopulationFitness(double energyTotal, double userValueCarbohydrates, double userValueProteins,
        double userValurFats)
    {
        foreach (var possibleRegime in Solutions)
        {
            possibleRegime.MacroNutrientCalculation();
            possibleRegime.CalculateFitness(userValueCarbohydrates, userValueProteins, energyTotal, userValurFats);
        }
    }

    public void GenerateInitialPopulation(int cantRecipes, int cantSolutions,
        IList<MealMenuRecipeDto> totalRecipes)
    {
        for (var i = 0; i < cantSolutions; i++)
        {
            var listRecipe = new List<MealMenuRecipeDto>(cantRecipes);
            for (var j = 0; j < cantRecipes; j++)
            {
                var rand = _r.Next(0, totalRecipes.Count);
                listRecipe.Add(totalRecipes[rand]);
            }

            var pr = new PossibleRegime(listRecipe);
            Solutions.Add(pr);
        }
    }

    public void Selection()
    {
        var rangeMax = Solutions.Count / 2;
        var cantTournament = _r.Next(2, rangeMax);
        Winners.Clear();
        for (var i = 0; i < cantTournament;)
        {
            var fighter1 = _r.Next(0, Solutions.Count);
            var fighter2 = _r.Next(0, Solutions.Count);

            if (fighter1 == fighter2) continue;
            var win = Solutions[fighter1].Fitness > Solutions[fighter2].Fitness
                ? Solutions[fighter1]
                : Solutions[fighter2];
            var exist = Winners.Any(s => s.Recipes.MenuRecipes.SequenceEqual(win.Recipes.MenuRecipes));
            if (!exist) Winners.Add(win);
            i++;
        }
    }

    public void Crossover(int solutionsAmount)
    {
        var probability = _r.NextDouble();
        if (probability >= 0.8) return;
        var sonsNew = new List<PossibleRegime>();
        for (var i = 0; i < solutionsAmount;)
        {
            var father1 = Winners[_r.Next(0, Winners.Count)];
            var father2 = Solutions[_r.Next(0, Solutions.Count)];

            var indexGenFather1 = _r.Next(0, father1.Recipes.MenuRecipes.Count);
            var indexGenFather2 = _r.Next(0, father2.Recipes.MenuRecipes.Count);

            var gen1 = father1.Recipes.MenuRecipes[indexGenFather1];
            var gen2 = father2.Recipes.MenuRecipes[indexGenFather2];

            if (gen1.Recipe.Id == gen2.Recipe.Id) continue;
            if (father1.Recipes.MenuRecipes.Any(r => r.Recipe.Id == gen2.Recipe.Id) ||
                father2.Recipes.MenuRecipes.Any(r => r.Recipe.Id == gen1.Recipe.Id)) continue;

            var newChromosome1 = NewChromosome(father1, gen2, indexGenFather1);
            var newChromosome2 = NewChromosome(father2, gen1, indexGenFather2);

            sonsNew.Add(newChromosome1);
            sonsNew.Add(newChromosome2);
            i += 2;
        }

        Solutions.Clear();
        foreach (var newSon in sonsNew)
        {
            Solutions.Add(newSon);
        }
    }

    public void Mutation(IList<MealMenuRecipeDto> totalRecipes, int recipesAmount, int solutionsAmount)
    {
        if (_r.NextDouble() > 0.4) return;
        var numberMutation = _r.Next(1, Solutions.Count);
        for (var i = 0; i < numberMutation; i++)
        {
            var changePosition = _r.Next(0, solutionsAmount);
            var changeIndexRegime = _r.Next(0, recipesAmount);
            var rateChangeRecipe = _r.Next(0, totalRecipes.Count);
            var changeRecipe = totalRecipes[rateChangeRecipe];
            Solutions[changePosition] = NewMutation(changeIndexRegime, changeRecipe, changePosition);
        }
    }


    public bool SolutionExist()
    {
        return Solutions.Any(r => r.Fitness == 8);
    }

    private IList<MealMenuRecipeDto> GetUniverseRecipes(int mealTypeId)
    {
        return _repository.FindAll().Result.Select(r => new MealMenuRecipeDto() { Recipe = r, Quantity = 1 }).ToList();
    }

    private void String()
    {
        Console.WriteLine();
        foreach (var solution in Solutions)
        {
            solution.DataString();
            Console.WriteLine();
        }

        Console.WriteLine();
    }

    private PossibleRegime NewMutation(int changeIndexRegime, MealMenuRecipeDto mealMenuRecipeDto, int changePosition)
    {
        var newRecipes = Solutions[changePosition].Recipes.MenuRecipes
            .Select((t, i) => i != changeIndexRegime ? t : mealMenuRecipeDto).ToList();

        var pr = new PossibleRegime(newRecipes);
        return pr;
    }

    private static PossibleRegime NewChromosome(PossibleRegime father, MealMenuRecipeDto gen, int indexFather)
    {
        var newChromosomal = father.Recipes.MenuRecipes.Select((t, i) => i == indexFather ? gen : t).ToList();
        var pr = new PossibleRegime(newChromosomal);

        return pr;
    }
}