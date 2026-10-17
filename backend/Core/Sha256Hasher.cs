using System;
using System.Security.Cryptography;

namespace backend.Core;

public static class Sha256Hasher
{
    public static async Task<string> ComputeAsync(Stream stream)
    {
        byte[] hash = await SHA256.HashDataAsync(stream);
        return Convert.ToHexString(hash);
    }
}
