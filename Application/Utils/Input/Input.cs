using System.Text.RegularExpressions;

namespace Application.Utils.Input;

public static class Input
{
    public static int ReadInteger(string message, int lowerBound, int upperBound)
    {
        for (var i = 1;; i++)
        {
            try
            {
                Console.WriteLine($"{message} [{lowerBound}, {upperBound}]: ");
                var integer = int.Parse(Console.ReadLine() ?? string.Empty);
                if (integer >= lowerBound && integer <= upperBound)
                {
                    return integer;
                }

                if (i is 3)
                {
                    Console.WriteLine("Se han registrado demasiados intentos sin éxito. La aplicación se detendrá");
                    Console.ReadKey();
                    Environment.Exit(-1);
                }

                Console.WriteLine("Debe ingresar un valor entero dentro del rango establecido. Intente nuevamente.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Debe ingresar un valor entero. Intente nuevamente.");
            }
        }
    }

    public static char ReadConfirmation(string message)
    {
        return ReadChar(message, 'y', 'n');
    }

    public static char ReadChar(string message, char characterOne, char characterTwo)
    {
        for (var i = 1;; i++)
        {
            Console.WriteLine($"{message} <{characterOne}, {characterTwo}>: ");
            var character = char.ToLower(Console.ReadKey().KeyChar);
            if (character == characterOne || character == characterTwo)
            {
                return character;
            }

            if (i is not 3) continue;
            Console.WriteLine("Se han registrado demasiados intentos sin éxito. La aplicación se detendrá");
            Console.ReadKey();
            Environment.Exit(-1);
        }
    }

    public static string ReadString(string message)
    {
        Console.WriteLine($"{message}: ");
        var str = Console.ReadLine() ?? string.Empty;
        str = str.Trim();
        return str;
    }

    public static string ReadString(string request, string formatRequest, params string[] expressions)
    {
        return ReadString(true, request, formatRequest, expressions);
    }
    
    public static string ReadString(bool verbosity, string request, string formatRequest,
        params string[] expressions)
    {
        if (expressions.Length == 0)
        {
            throw new ArgumentException("No regular expressions were received to validate input");
        }

        var regexes = expressions.Select(s => new Regex(s)).ToList();
        for (var i = 1;; i++)
        {
            Console.WriteLine(verbosity ? $"{request}\n{formatRequest}: " : $"{request}: ");
            var str = Console.ReadLine() ?? string.Empty;
            str = str.Trim();
            if (regexes.Select(regex => regex.Match(str)).Any(match => match.Success))
            {
                return str;
            }

            if (i is not 3) continue;
            Console.WriteLine("Se han registrado demasiados intentos sin éxito. La aplicación se detendrá");
            Console.ReadKey();
            Environment.Exit(-1);
        }
    }

}