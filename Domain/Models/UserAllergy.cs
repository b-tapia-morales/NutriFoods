namespace Domain.Models;

public sealed class UserAllergy
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public int IngredientId { get; set; }

    public Ingredient Ingredient { get; set; } = null!;

    public UserProfile User { get; set; } = null!;
}
