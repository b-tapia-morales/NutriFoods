namespace API.Dto;

public sealed class PatientDto
{
    public Guid Id { get; set; }
    public DateTime JoinedOn { get; set; }
    public PersonalInfoDto? PersonalInfo { get; set; }
    public ContactInfoDto? ContactInfo { get; set; }
    public AddressDto? Address { get; set; }
    public List<ConsultationDto> Consultations { get; set; } = null!;
}