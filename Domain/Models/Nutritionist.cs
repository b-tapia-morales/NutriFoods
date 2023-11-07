namespace Domain.Models;

public sealed class Nutritionist
{
    public Guid Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime JoinedOn { get; set; }

    public ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
