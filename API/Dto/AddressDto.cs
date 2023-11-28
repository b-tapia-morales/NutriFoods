namespace API.Dto;

public sealed class AddressDto
{
    public string Street { get; set; } = null!;
    public int Number { get; set; }
    public int? PostalCode { get; set; }
    public string Province { get; set; } = null!;
}