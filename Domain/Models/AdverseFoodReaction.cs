using Domain.Enum;

namespace Domain.Models;

public sealed class AdverseFoodReaction
{
    public Guid Id { get; set; }

    public int FoodGroup { get; set; }

    public FoodReactionType Type { get; set; } = null!;

    public Guid NutritionalAnamnesis { get; set; }

    public NutritionalAnamnesis NutritionalAnamnesesNavigation { get; set; } = null!;
}
