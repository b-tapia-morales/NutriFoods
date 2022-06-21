using System.Text.RegularExpressions;
using static System.Console;
using static System.String;

namespace Application.Utils.Input;

public static class Input
{
    public static int ReadInteger(string message, int lowerBound, int upperBound)
    {
        while (true)
        {
            try
            {
                WriteLine($"{message} [{lowerBound}, {upperBound}]: ");
                var integer = int.Parse(ReadLine() ?? Empty);
                if (integer >= lowerBound && integer <= upperBound)
                {
                    return integer;
                }

                WriteLine("Debe ingresar un valor entero dentro del rango establecido. Intente nuevamente.");
            }
            catch (FormatException exception)
            {
                WriteLine("Debe ingresar un valor entero. Intente nuevamente.");
            }
        }
    }

    public static char ReadConfirmation(string message)
    {
        return ReadChar(message, 'y', 'n');
    }
    
    public static char ReadChar(string message, char characterOne, char characterTwo)
    {
        while (true)
        {
            WriteLine($"{message} <{characterOne}, {characterTwo}>: ");
            var character = char.ToLower(ReadKey().KeyChar);
            if (character == characterOne || character == characterTwo)
            {
                return character;
            }
        }
    }

    public static string ReadString(string message)
    {
        WriteLine($"{message}: ");
        var str = ReadLine() ?? Empty;
        str = str.Trim();
        return str;
    }

    public static string ReadString(bool verbosity, string request, string formatRequest,
        params string[] expressions)
    {
        if (expressions.Length == 0)
        {
            throw new ArgumentException("No regular expressions were received to validate input");
        }

        var regexes = expressions.Select(s => new Regex(s)).ToList();
        while (true)
        {
            WriteLine(verbosity ? $"{request}\n{formatRequest}: " : $"{request}: ");
            var str = ReadLine() ?? Empty;
            str = str.Trim();
            if (regexes.Select(regex => regex.Match(str)).Any(match => match.Success))
            {
                return str;
            }
        }
    }
}