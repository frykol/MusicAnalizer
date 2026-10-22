using System;
using backend.Models;

namespace backend.Repositories;

public interface IAuthRepository
{
    Task<RefreshToken> CreateRefeshTokenAsync(RefreshToken token);
    Task<RefreshToken?> GetRefreshTokenByHash(string hash);
}
