using API.Dto;

namespace API.Genetic;

public class GeneticAlgorithm : IGeneticAlgorithm
{
    private readonly Random _random = new(Environment.TickCount);

    public IList<MenuRecipeDto> GenerateUniverse(IEnumerable<RecipeDto> recipes)
    {
        return recipes.Select(r => new MenuRecipeDto {Recipe = r, Portions = 1}).ToList();
    }

    public void GenerateInitialPopulation(IList<MenuRecipeDto> universe, IList<Chromosome> population,
        int chromosomeSize, int populationSize)
    {
        for (var i = 0; i < populationSize; i++)
        {
            var recipes = new List<MenuRecipeDto>(chromosomeSize);
            for (var j = 0; j < chromosomeSize; j++)
            {
                var rand = _random.Next(0, universe.Count);
                recipes.Add(universe[rand]);
            }

            population.Add(new Chromosome(recipes));
        }
    }

    public void CalculatePopulationFitness(IList<Chromosome> population, double energy, double carbohydrates,
        double lipids, double proteins, double marginOfError)
    {
        foreach (var possibleRegime in population)
        {
            possibleRegime.AggregateMacronutrients();
            possibleRegime.UpdateFitness(energy, carbohydrates, lipids, proteins, marginOfError);
        }
    }

    public bool SolutionExists(IList<Chromosome> population, int iteration)
    {
        return iteration > 50000 || population.Any(r => r.Fitness == 8);
    }

    public void Selection(IList<Chromosome> population, IList<Chromosome> winners)
    {
        var rangeMax = population.Count / 2;
        var cantTournament = _random.Next(2, rangeMax);
        winners.Clear();
        for (var i = 0; i < cantTournament;)
        {
            var fighter1 = _random.Next(0, population.Count);
            var fighter2 = _random.Next(0, population.Count);

            if (fighter1 == fighter2) continue;
            var win = population[fighter1].Fitness > population[fighter2].Fitness
                ? population[fighter1]
                : population[fighter2];
            var exist = winners.Any(s => s.DailyMenu.MenuRecipes.SequenceEqual(win.DailyMenu.MenuRecipes));
            if (!exist) winners.Add(win);
            i++;
        }
    }

    public void Crossover(IList<Chromosome> population, IList<Chromosome> winners, int populationSize)
    {
        var probability = _random.NextDouble();
        if (probability >= 0.8) return;
        var sonsNew = new List<Chromosome>();
        for (var i = 0; i < populationSize;)
        {
            var father1 = winners[_random.Next(0, winners.Count)];
            var father2 = population[_random.Next(0, population.Count)];

            var indexGenFather1 = _random.Next(0, father1.DailyMenu.MenuRecipes.Count);
            var indexGenFather2 = _random.Next(0, father2.DailyMenu.MenuRecipes.Count);

            var gen1 = father1.DailyMenu.MenuRecipes[indexGenFather1];
            var gen2 = father2.DailyMenu.MenuRecipes[indexGenFather2];

            if (gen1.Recipe.Id == gen2.Recipe.Id) continue;
            if (father1.DailyMenu.MenuRecipes.Any(r => r.Recipe.Id == gen2.Recipe.Id) ||
                father2.DailyMenu.MenuRecipes.Any(r => r.Recipe.Id == gen1.Recipe.Id)) continue;

            var newChromosome1 = NewChromosome(father1, gen2, indexGenFather1);
            var newChromosome2 = NewChromosome(father2, gen1, indexGenFather2);

            sonsNew.Add(newChromosome1);
            sonsNew.Add(newChromosome2);
            i += 2;
        }

        population.Clear();
        foreach (var newSon in sonsNew)
        {
            population.Add(newSon);
        }
    }

    public void Mutation(IList<MenuRecipeDto> menus, IList<Chromosome> population, int chromosomeSize,
        int populationSize)
    {
        if (_random.NextDouble() > 0.4) return;
        var numberMutation = _random.Next(1, population.Count);
        for (var i = 0; i < numberMutation; i++)
        {
            var changePosition = _random.Next(0, populationSize);
            var changeIndexRegime = _random.Next(0, chromosomeSize);
            var rateChangeRecipe = _random.Next(0, menus.Count);
            var changeRecipe = menus[rateChangeRecipe];
            population[changePosition] = NewMutation(population, changeIndexRegime, changeRecipe, changePosition);
        }
    }

    public void ShowPopulation(IList<Chromosome> population)
    {
        Console.WriteLine();
        foreach (var solution in population)
        {
            solution.ShowPhenotypes();
        }

        Console.WriteLine();
    }

    private static Chromosome NewMutation(IList<Chromosome> solutions, int changeIndexRegime,
        MenuRecipeDto mealMenuRecipeDto, int changePosition)
    {
        var newRecipes = solutions[changePosition].DailyMenu.MenuRecipes
            .Select((t, i) => i != changeIndexRegime ? t : mealMenuRecipeDto).ToList();
        return new Chromosome(newRecipes);
    }

    private static Chromosome NewChromosome(Chromosome father, MenuRecipeDto gen, int indexFather)
    {
        var newChromosome = father.DailyMenu.MenuRecipes.Select((t, i) => i == indexFather ? gen : t).ToList();
        return new Chromosome(newChromosome);
    }
}