using System;
using System.Security.Cryptography;
using System.Text;

namespace backend.Infrastructure.Auth;

public static class RefreshTokenGenerator
{
    public static string GenerateRefreshToken()
    {
        byte[] bytes = RandomNumberGenerator.GetBytes(32);
        return Convert.ToBase64String(bytes);
    }

    public static string HashRefreshToken(string token)
    {
        byte[] hash = SHA256.HashData(Encoding.UTF8.GetBytes(token));

        return Convert.ToBase64String(hash);
    }

    public static bool CompareHashed(string token, string hashedToken)
    {
        var toHash = HashRefreshToken(token);
        return toHash == hashedToken;
    }
}
