using System.Security.Cryptography;

namespace API.Utils;

public static class ApiKeyGenerator
{
    private const string Prefix = "NF-";
    private const int SecureBytesLength = 32;
    private const int KeyLength = 36;

    public static string Generate()
    {
        var bytes = RandomNumberGenerator.GetBytes(SecureBytesLength);
        return string.Concat(Prefix, Convert.ToBase64String(bytes)
            .Replace("/", "")
            .Replace("+", "")
            .Replace("=", "")
            .AsSpan(0, KeyLength - Prefix.Length));
    }
}

public static class PasswordEncryption
{
    public static string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public static bool Verify(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}