using Domain.Enum;

namespace Domain.Models;

public sealed class Address
{
    public Guid Id { get; set; }

    public string Street { get; set; } = null!;

    public int Number { get; set; }

    public int? PostalCode { get; set; }

    public Province Province { get; set; } = null!;

    public Patient IdNavigation { get; set; } = null!;
}
