using System.Security.Cryptography;
using System.Text;
using Link.Application.Interfaces;

namespace Link.Infrastructure.Services;

public class UrlShorteningService : IUrlShorteningService
{
    private const string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    private const int Length = 7;

    public string GenerateShortCode()
    {
        // Use RandomNumberGenerator for cryptographically secure randomness
        // avoiding collisions better than standard Random()
        var bytes = new byte[Length];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(bytes);
        }

        var result = new StringBuilder(Length);
        foreach (var b in bytes)
        {
            result.Append(Alphabet[b % Alphabet.Length]);
        }

        return result.ToString();
    }
}