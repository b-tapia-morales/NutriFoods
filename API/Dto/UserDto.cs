namespace API.Dto;

public class UserDto
{
    public Guid Id { get; set; }
    public string Username { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string ApiKey { get; init; } = string.Empty;
    public UserDataDto UserData { get; init; } = null!;
    public ICollection<UserBodyMetricDto> BodyMetrics { get; set; } = new List<UserBodyMetricDto>();
}