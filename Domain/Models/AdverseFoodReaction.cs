using Domain.Enum;

namespace Domain.Models;

public class AdverseFoodReaction
{
    public Guid Id { get; set; }

    public FoodGroup FoodGroup { get; set; } = null!;

    public FoodReactionType Type { get; set; } = null!;

    public Guid NutritionalAnamnesis { get; set; }

    public NutritionalAnamnesis NutritionalAnamnesesNavigation { get; set; } = null!;
}
