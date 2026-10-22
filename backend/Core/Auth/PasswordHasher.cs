using System;
using System.Security.Cryptography;
using System.Text;

namespace backend.Core.Auth;

public static class PasswordHasher
{
       private const int SaltSize = 16; // 128-bit salt
    private const int KeySize = 32;  // 256-bit subkey
    private const int Iterations = 100_000; // Recommended minimum PBKDF2 iterations
    private static readonly HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA256;

    /// <summary>
    /// Hashes a plain-text password using PBKDF2 and a random salt.
    /// </summary>
    public static string HashPassword(string password)
    {
        // 1. Generate a unique, cryptographically strong random salt
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

        // 2. Derive the cryptographic key (hash)
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            Iterations,
            HashAlgorithm,
            KeySize
        );

        // 3. Combine salt and hash into a single string for storage
        // Format: [iterations].[salt].[hash]
        return $"{Iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
    }

    /// <summary>
    /// Validates a plain-text password against a previously generated hash.
    /// </summary>
    public static bool VerifyPassword(string password, string hashedPassword)
    {
        try
        {
            // 1. Split and parse the stored hash components
            var parts = hashedPassword.Split('.');
            if (parts.Length != 3) return false;

            int iterations = int.Parse(parts[0]);
            byte[] salt = Convert.FromBase64String(parts[1]);
            byte[] storedHash = Convert.FromBase64String(parts[2]);

            // 2. Hash the incoming password using the exact same salt and iteration count
            byte[] computeHash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                HashAlgorithm,
                KeySize
            );

            // 3. Use FixedTimeEquals to prevent timing attacks
            return CryptographicOperations.FixedTimeEquals(storedHash, computeHash);
        }
        catch
        {
            return false;
        }
    }
}
