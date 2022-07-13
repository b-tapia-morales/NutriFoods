
using API.Dto;

namespace API.Genetic;

public class GeneticAlgorithm
{
    private List<Solutions> _listRegime;
    private List<Solutions> _winners;
    private List<RecipeDto> _totalRecipes;
    private readonly int _cantRecipe;
    private double _kilocalories;
    private double _carbohydrates;
    private double _proteins;
    private double _fats;
    public GeneticAlgorithm(int cantRecipe,double kilocalories, double carbohydrates,double proteins,double fats)
    {
        _listRegime = new List<Solutions>(cantRecipe);
        _winners = new List<Solutions>(cantRecipe);
        _totalRecipes = new List<RecipeDto>();
        _cantRecipe = cantRecipe;
        _kilocalories = kilocalories;
        _carbohydrates = carbohydrates;
        _proteins = proteins;
        _fats = fats;
    }

    public List<Solutions> GetRegimen()
    {
        var seed = Environment.TickCount;
        var random = new Random(seed);
        GenerateSolutions();
        while ( 0 < 1000)
        {
            
            Tournamet(random);
            Cross(random);
            Mutation(random);
        }
        
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
            Solutions s = new Solutions(3);
            for (int j = 0; j < 3; j++)
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
        var cantTournamet = r.Next(1, _listRegime.Count);
        _winners = new List<Solutions>();
        var ite = 0;
        while (ite < cantTournamet)
        {
            int combatant1 = r.Next(0, _listRegime.Count);
            int combatant2 = r.Next(0, _listRegime.Count);
            // no tiene que ser los mismos
            if (combatant1 != combatant2)
            {
                // se procede la lucha el que tenga mayor fitness sera el ganador
                if (_listRegime[combatant1].Fitness(0.9, 0.9, 0.9, 0.9,
                        _carbohydrates, _proteins, _kilocalories, _fats) >
                    _listRegime[combatant2].Fitness(0.9, 0.9, 0.9, 0.9,
                        _carbohydrates, _proteins, _kilocalories, _fats))
                {
                    // reviso que si exite el ganador, si no existe lo ingreso a la lista de ganadores y se cumplio un duelo
                    if (_winners.IndexOf(_listRegime[combatant1]) == -1)
                    {
                        _winners.Add(_listRegime[combatant1]);
                        ite++;
                    }
                }
                else
                {
                    // reviso que si exite el ganador, si no existe lo ingreso a la lista de ganadores y se cumplio un duelo
                    if (_winners.IndexOf(_listRegime[combatant2]) == -1)
                    {
                        _winners.Add(_listRegime[combatant2]);
                        ite++;
                    }
                }
            }
        }
    }

    private void Cross(Random r)
    {
        double probability = r.NextDouble();
        List<Solutions> sons = new List<Solutions>();

        if (probability <= 0.8)
        {
            int i = 0;
            while (i < _winners.Count)
            {
                // selecciona uno de la lista de ganadores
                Solutions padre1 = _winners[i];
                // selecciona uno al azar de los anteriores
                Solutions padre2 = _listRegime[r.Next(0, _listRegime.Count)];

                //selecciona al azar una posiciones para cada padre
                int indexGenP1 = r.Next(0, padre1.ListRecipe.Count);
                int indexGenP2 = r.Next(0, padre2.ListRecipe.Count);

                // se obtiene el gen para cada padre (una receta)
                RecipeDto gen1 = padre1.ListRecipe[indexGenP1];
                RecipeDto gen2 = padre2.ListRecipe[indexGenP2];

                // se produce el intercambio de generes
                padre1.ListRecipe[indexGenP1] = gen2;
                padre2.ListRecipe[indexGenP2] = gen1;

                sons.Add(padre1);
                if (sons.Count <= 7)
                {
                    sons.Add(padre2);
                }

                i++;
            }
            _listRegime = sons;
        }
    }

    private void Mutation(Random r)
    {
        
        if (r.NextDouble() <= 0.2)
        {
            int cantMutation = r.Next(0, _listRegime.Count);

            for (int i = 0; i < cantMutation; i++)
            {
                // se elige una posicion al azar de un regimen
                int index = r.Next(0, _listRegime.Count);
                // selecciono ese regimen
                Solutions cromo = _listRegime[index];
                // selecciono un receta al azar para reemplazar
                RecipeDto recipe = _totalRecipes[r.Next(0, _totalRecipes.Count)];
                cromo.ListRecipe[index] = recipe;
            }
        }
    }
    
    
    
    
}