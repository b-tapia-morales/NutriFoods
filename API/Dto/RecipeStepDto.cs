namespace API.Dto;

public class RecipeStepDto
{
    public int Id { get; set; }
    public int Recipe { get; set; }
    public int Step { get; set; }
    public string Description { get; set; } = null!;
}