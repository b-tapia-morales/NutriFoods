
using API.Dto;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Genetic;

public class GeneticAlgorithm
{
    private List<Solutions> _listRegime;
    private List<Solutions> _winners;
    private List<Recipe> _totalRecipes;
    private readonly int _cantRecipe;
    private double _kilocalories;
    private double _carbohydrates;
    private double _proteins;
    private double _fats;
    private const string ConnectionString =
        "Host=localhost;Database=nutrifoods_db;Username=nutrifoods_dev;Password=MVmYneLqe91$";
    public GeneticAlgorithm(int cantRecipe,double kilocalories, double carbohydrates,double proteins,double fats)
    {
        _listRegime = new List<Solutions>(cantRecipe);
        _winners = new List<Solutions>(cantRecipe);
        _totalRecipes = TotalRecipes();
        _cantRecipe = cantRecipe;
        _kilocalories = kilocalories;
        _carbohydrates = carbohydrates;
        _proteins = proteins;
        _fats = fats;
        /*
        foreach (var variaRecipe in _totalRecipes)
        {
            foreach (var vaNutrient in variaRecipe.RecipeNutrients.ToList())
            {
                if (vaNutrient.NutrientId == 108)
                {
                    Console.WriteLine(vaNutrient.Quantity +" "+variaRecipe.Name);
                }
            }
        }
        Console.WriteLine(_totalRecipes.Count);
        */
    }

    public static List<Recipe> TotalRecipes()
    {
        var options = new DbContextOptionsBuilder<NutrifoodsDbContext>()
            .UseNpgsql(ConnectionString,
                builder => builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
            .Options;
        using var context = new NutrifoodsDbContext(options);
        var recipes = IncludeSubfields(context.Recipes).Where(e => e.Portions is > 0).ToList();
        return recipes;
    }

    public List<Solutions> GetRegimen()
    {
        var seed = Environment.TickCount;
        var random = new Random(seed);
        GenerateSolutions();
        var ite = 0;
        while ( ite < 1000000)
        {
            //Solutions();
            Tournamet(random);
            Cross(random);
            Mutation(random);
            ModifyFitness();
            ite++;
        }
        Solutions();
        SolutionsFinal();
        return _listRegime;
    }
    private void GenerateSolutions()
    {
        var seed = Environment.TickCount;
        var random = new Random(seed);
        for (int i = 0; i < _cantRecipe; i++)
        {
            /* una solucion consta de 3 platos por ejemplo un almuerzo
             se crea una solucion de tamaño 3 [R1,R2,R3]
            */
            Solutions s = new Solutions(6);
            for (int j = 0; j < 6; j++)
            {
                // numero random para elegir al azar una receta
                int rand = random.Next(0, _totalRecipes.Count);
                s.AddRecipe(_totalRecipes[rand]);
            }
            // termine de generar y lo ingresa a la lista de soluciones
            _listRegime.Add(s);
        }
    }

    private void Tournamet(Random r)
    {
        int cantTournamet = r.Next(2, _listRegime.Count);
        _winners.Clear();
        var ite = 0;
        while (ite < cantTournamet)
        {
            int combatant1 = r.Next(0, _listRegime.Count);
            int combatant2 = r.Next(0, _listRegime.Count);

            if (combatant1 != combatant2)
            {
                // se procede la lucha el que tenga mayor fitness sera el ganador
                if (_listRegime[combatant1].fittnes > _listRegime[combatant2].fittnes)
                {
                    // reviso que si exite el ganador, si no existe lo ingreso a la lista de ganadores y se cumplio un duelo

                        _winners.Add(_listRegime[combatant1]);
                        ite++;

                }
                else
                {
                    // reviso que si exite el ganador, si no existe lo ingreso a la lista de ganadores y se cumplio un duelo

                        _winners.Add(_listRegime[combatant2]);
                        ite++;
                    
                }
            }
        }
        
    }

    private Solutions Sons(Recipe r,int indexm,Solutions father)
    {
        Solutions s = new Solutions(6);
        for (int i = 0; i < father.ListRecipe.Count; i++)
        {
            if (indexm != i)
            {
                s.AddRecipe(father.ListRecipe[i]);
            }
            else
            {
                s.AddRecipe(r);
            }
        }

        return s;
    }
    private void Cross(Random r)
    {
        double probability = r.NextDouble();
        List<Solutions> sons = new List<Solutions>();
        if (probability <= 0.8)
        {
            int i = 0;
            while (i < 6)
            {
                // selecciona uno de la lista de ganadores
                Solutions padre1 = _winners[r.Next(0, _winners.Count)];
                // selecciona uno al azar de los anteriores
                Solutions padre2 = _listRegime[r.Next(0, _listRegime.Count)];

                //selecciona al azar una posiciones para cada padre
                int indexGenP1 = r.Next(0, padre1.ListRecipe.Count);
                int indexGenP2 = r.Next(0, padre2.ListRecipe.Count);

                Recipe gen1 = padre1.ListRecipe[indexGenP1];
                Recipe gen2 = padre2.ListRecipe[indexGenP2];
                
                
                // se produce el intercambio de genes
                if(gen1.Id != gen2.Id)
                {
                    sons.Add(Sons(gen2,indexGenP1,padre1));
                    sons.Add(Sons(gen1,indexGenP2,padre2));
                    i+=2;
                }
            }

            _listRegime = sons;
        }
    }

    private void Mutation(Random r)
    {
        if (r.NextDouble() <= 0.4)
        {
            int cantMutation = r.Next(1, _listRegime.Count);
            for (int i = 0; i < cantMutation; i++)
            {
                int index = r.Next(0, _listRegime.Count);
                int indexAMutation = r.Next(0, _listRegime[index].ListRecipe.Count);
                int indexNewRecipe = r.Next(0, _totalRecipes.Count);
                _listRegime[index] = NewSolution(index,indexAMutation,indexNewRecipe);
            }
        }
    }

    private Solutions NewSolution(int index, int indexNew,int indexNewRecipe)
    {
        Solutions s = new Solutions(6);
        for (int i = 0; i < _listRegime[index].ListRecipe.Count; i++)
        {
            if (i != indexNew)
            {
                s.AddRecipe(_listRegime[index].ListRecipe[i]);
            }
            else
            {
                s.AddRecipe(_totalRecipes[indexNewRecipe]);
            }
        }

        return s;
    }
    private static IEnumerable<Recipe> IncludeSubfields(IQueryable<Recipe> recipes)
    {
        return recipes
            .Include(e => e.RecipeNutrients)
            .ThenInclude(e => e.Nutrient)
            .Include(e => e.RecipeMeasures)
            .ThenInclude(e => e.IngredientMeasure)
            .ThenInclude(e => e.Ingredient)
            .ThenInclude(e => e.IngredientNutrients)
            .ThenInclude(e => e.Nutrient)
            .Include(e => e.RecipeQuantities)
            .ThenInclude(e => e.Ingredient)
            .ThenInclude(e => e.IngredientNutrients)
            .ThenInclude(e => e.Nutrient)
            .AsNoTracking();
    }

    private void ModifyFitness()
    {
        foreach (var solutions in _listRegime)
        {
            solutions.CalculatFittnes();
            solutions.Fitness(_carbohydrates,_proteins,_kilocalories,_fats);
        }
    }

    private void Solutions()
    {
        foreach (var solutions in _listRegime)
        {
            foreach (var recipe in solutions.ListRecipe)
            {
                Console.Write(recipe.Id+ " ");
            }
            Console.Write(" | "+solutions.fittnes+ " | "+solutions.CantKilocalories);
            Console.WriteLine();
        }
        Console.WriteLine("-----------------------");
    }

    private void SolutionsFinal()
    {
        foreach (var solutions in _listRegime)
        {
            solutions.Print();
        }
        Console.WriteLine();
    }

    private Boolean ExistWinners(Solutions s)
    {
        foreach (var solutions in _winners)
        {
            int i;
            for (i = 0; i < solutions.ListRecipe.Count; i++)
            {
                if(!solutions.ListRecipe[i].Name.Equals(s.ListRecipe[i].Name)) break;
            }

            if (i == solutions.ListRecipe.Count)
            {
                return true;
            }
        }
        return false;
    }
    
}