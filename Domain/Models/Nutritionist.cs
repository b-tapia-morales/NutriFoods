namespace Domain.Models;

public class Nutritionist
{
    public Guid Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime JoinedOn { get; set; }

    public virtual ICollection<Patient> Patients { get; set; } = null!;
}
