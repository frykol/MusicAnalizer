using System;
using backend.Context;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly AppDbContext _context;

    public AuthRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<RefreshToken> CreateRefeshTokenAsync(RefreshToken token)
    {
        _context.RefreshTokens.Add(token);
        await _context.SaveChangesAsync();
        return token;
    }
    public async Task<RefreshToken?> GetRefreshTokenByHash(string hash)
    {
        return await _context.RefreshTokens.FirstOrDefaultAsync(t => t.TokenHash == hash);
    }
}
