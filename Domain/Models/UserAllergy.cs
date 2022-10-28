namespace Domain.Models;

public class UserAllergy
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public int IngredientId { get; set; }

    public virtual Ingredient Ingredient { get; set; } = null!;
    public virtual UserProfile User { get; set; } = null!;
}