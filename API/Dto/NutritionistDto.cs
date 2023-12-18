namespace API.Dto;

public sealed class NutritionistDto
{
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public DateTime JoinedOn { get; set; }
    public List<PatientDto> Patients { get; set; } = null!;
}