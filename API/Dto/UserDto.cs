namespace API.Dto;

public class UserDto
{
    public Guid Id { get; set; }
    public string Username { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string ApiKey { get; init; } = string.Empty;
    public ICollection<UserBodyMetricDto> BodyMetrics { get; set; } = null!;
}