using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using backend.Models;
using Microsoft.IdentityModel.Tokens;

namespace backend.Infrastructure.Auth;

public static class JwtTokenGenerator
{
    public static string GenerateToken(string secretKey, int userId)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "MusicAnalizer", 
            audience: "MusicAnalizerClient", 
            claims: [
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()) 
                    ],
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
