namespace API.Utils;

public static class RegexUtils
{
    public const string Username = @"^(?=.{8,20}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$";

    public const string UsernameRule =
        @"Username must be between 8 to 20 characters long, and can only contain alphanumeric characters, underscore and dot. Underscore and dot can't be at the end or start, can't be next to each other, and can't be used multiple times in a row.
";

    public const string Password = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,20}$";

    public const string PasswordRule =
        @"Password must be 8 characters long minimum, and must contain at least one uppercase letter, one lowercase letter, one number and one special character.
";
}