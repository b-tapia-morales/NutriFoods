using Domain.Enum;

namespace Domain.Models;

public class AdverseFoodReaction
{
    public Guid Id { get; set; }

    public FoodGroups FoodGroup { get; set; } = null!;

    public FoodReactions Type { get; set; } = null!;

    public Guid NutritionalAnamnesisId { get; set; }

    public virtual NutritionalAnamnesis NutritionalAnamnesis { get; set; } = null!;
}
