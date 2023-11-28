// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global

namespace NutrientRetrieval.Translation;

public class TranslationResult
{
    public Translation[] Translations { get; set; } = null!;
}

public class Translation
{
    public string Text { get; set; } = null!;
    public string To { get; set; } = null!;
}