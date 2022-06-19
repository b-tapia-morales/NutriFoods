using System.Globalization;
using Microsoft.FSharp.Core;
using OpenQA.Selenium;

namespace RecipeAndMesuris.Recipe_insert;

public class IngredientRecipe
{

    private void ReadFileRecipeIngredient(string pathReceta)
    {
        StreamReader fileRecipeIngredients = new StreamReader(pathReceta);
        //Console.WriteLine(pathReceta);
        while (!fileRecipeIngredients.EndOfStream)
        {
            string line = fileRecipeIngredients.ReadLine() ?? throw new InvalidOperationException();
            string[] split = line.Split(",");
            TransformUnits(split[0],split[1]);

        }
        fileRecipeIngredients.Close();
    }

    private void InsertIngredientRecipe(string pathReceta)
    {
        StreamReader fileRecipe = new StreamReader("Recipe_insert/Recipe/gourmet/recetas_"+pathReceta+".txt");
        while (!fileRecipe.EndOfStream)
        {
            string line = fileRecipe.ReadLine() ?? throw new InvalidOperationException();
            string[] spilt = line.Split(";");
            ReadFileRecipeIngredient("Recipe_insert/Ingredient/parcerIngredientes/Ingredientes_Gourmet/"+pathReceta+"/ingre_"+spilt[0]+".txt");
        }
        fileRecipe.Close();
        
    }

    public void ReadInsertRecipeIngredient()
    {
        string[] path = {"Ensaladas","Entradas","PlatosFondo"};
        foreach (var nameDirectory in path)
        {
            InsertIngredientRecipe(nameDirectory);
        }
    }

    private void TransformUnits(string quantity, string units)
    {
        if (quantity.Length == 1)
        {
            List<int> numDem = TransformUnicode(quantity);
        }
        else
        {
            if (quantity.Contains("/"))
            {
                int numerador = quantity[0] - '0';
                int denominador = quantity[2] - '0';
                //Console.WriteLine(quantity);
            }
            else
            {
                if (quantity.Contains(" "))
                {
                    Console.WriteLine(quantity + " "+units);
                    
                }
                else
                {
                    //Console.WriteLine(quantity+ " "+units);
                }
            }
            
        }
    }

    private List<int> TransformUnicode(string quantity)
    {
        List<int> numDem = new List<int>(2);
        char unit = Char.Parse(quantity);
        if (Char.GetUnicodeCategory(unit) == UnicodeCategory.OtherNumber)
        {
            if(Math.Abs(CharUnicodeInfo.GetNumericValue(unit) - 0.25) == 0)
            {
                numDem.Add(1);
                numDem.Add(4);
            }

            if (Math.Abs(CharUnicodeInfo.GetNumericValue(unit) - 0.75) == 0)
            {
                numDem.Add(3);
                numDem.Add(4);
            }
            if(Math.Abs(CharUnicodeInfo.GetNumericValue(unit) - 0.5) == 0)
            {
                numDem.Add(1);
                numDem.Add(2);
            }
        }
        return numDem;
    }
}