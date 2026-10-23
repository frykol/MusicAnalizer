using System;
using backend.Models;

namespace backend.Repositories;

public interface IAuthRepository
{
    Task<RefreshToken> CreateRefeshTokenAsync(RefreshToken token);
    Task<RefreshToken?> GetRefreshTokenByHashAsync(string hash);

    Task<int> SaveChangesAsync();

    Task<List<RefreshToken>> GetRefreshTokensAsync(int userId);
}
