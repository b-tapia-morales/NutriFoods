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