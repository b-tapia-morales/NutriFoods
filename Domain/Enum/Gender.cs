using System.Collections.Immutable;
using Ardalis.SmartEnum;

namespace Domain.Enum;

public class Gender : SmartEnum<Gender>, IEnum<Gender, GenderToken>
{
    public static readonly Gender None =
        new(nameof(None), (int)GenderToken.None, string.Empty);

    public static readonly Gender Male =
        new(nameof(Male), (int)GenderToken.Male, "Masculino");

    public static readonly Gender Female =
        new(nameof(Female), (int)GenderToken.Female, "Femenino");

    public Gender(string name, int value, string readableName) : base(name, value) => ReadableName = readableName;
    public string ReadableName { get; }
    public static IReadOnlyCollection<Gender> Values() => List;
}

public enum GenderToken
{
    None,
    Male,
    Female
}