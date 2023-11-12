using Domain.Enum;

namespace Domain.Models;

public class Address
{
    public Guid Id { get; set; }

    public string Street { get; set; } = null!;

    public int Number { get; set; }

    public int? PostalCode { get; set; }

    public Provinces Province { get; set; } = null!;

    public Patient IdNavigation { get; set; } = null!;
}
