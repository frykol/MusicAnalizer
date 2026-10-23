using System;
using backend.Core.Auth;
using backend.Dtos.User;
using backend.Infrastructure.Auth;
using backend.Mappers;
using backend.Models;
using backend.Repositories;
using backend.Services.Results;

namespace backend.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _repository;
    private readonly IUserRepository _userRepository;
    private readonly string _secretKey;

    public AuthService(IConfiguration configuration,IAuthRepository repository, IUserRepository userRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
        _secretKey = configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT key is missing");
    }

    private async Task<(string accessToknen, string refreshToken)> GenerateTokens(User user)
    {
        var refresh = RefreshTokenGenerator.GenerateRefreshToken();
        var hashedRefreshToken = RefreshTokenGenerator.HashRefreshToken(refresh);

        var newRefresh = new RefreshToken
        {
            TokenHash = hashedRefreshToken,
            ExpiresAt = DateTime.UtcNow.AddDays(7),

            User = user,
            UserId = user.Id,
        };

        await _repository.CreateRefeshTokenAsync(newRefresh);

        var access = JwtTokenGenerator.GenerateToken(_secretKey, user.Id);

        return (access, refresh);
    }


    public async Task<UserServiceResponse<UserLoginResponseDto>> CreateUserAsync(UserCreateDto userCreate)
    {
        bool userExists = await _userRepository.DoesUserExistsAsync(userCreate.Username);
        if (userExists)
            return new UserServiceResponse<UserLoginResponseDto>{Status = UserServiceStatusResponse.UserAlreadyExists, Value = null};

        var hashedPassword = PasswordHasher.HashPassword(userCreate.Password);

        var user = new User
        {
          Username = userCreate.Username,
          PasswordHash = hashedPassword,
        };

        await _userRepository.CreateUserAsync(user);

        var tokens = await GenerateTokens(user);

        var userLoginResponse = UserMapper.toUserLoginResponseDto(user, tokens.accessToknen, tokens.refreshToken);
        
        return new UserServiceResponse<UserLoginResponseDto>{Status = UserServiceStatusResponse.Ok, Value = userLoginResponse};
    }

    public async Task<User?> GetByIdAsync(int? id)
    {
        if (id is null)
            return null;

        return await _userRepository.GetUserByIdAsync(id ?? default);
    }

    public async Task<UserServiceResponse<UserLoginResponseDto>> LoginAsync(UserLoginDto userLogin)
    {
        var user = await _userRepository.GetUserByNameAsync(userLogin.Username);

        if (user is null)
            return new UserServiceResponse<UserLoginResponseDto>{Status = UserServiceStatusResponse.UserNotFound, Value = null};

        var isPasswordCorrect = PasswordHasher.VerifyPassword(userLogin.Password, user.PasswordHash);

        if (!isPasswordCorrect)
            return new UserServiceResponse<UserLoginResponseDto>{Status = UserServiceStatusResponse.InvalidCredentials, Value = null};

        var tokens = await GenerateTokens(user);

        var userLoginResponse = UserMapper.toUserLoginResponseDto(user, tokens.accessToknen, tokens.refreshToken);

        return new UserServiceResponse<UserLoginResponseDto>{Status = UserServiceStatusResponse.Ok, Value = userLoginResponse};
    }

    public async Task<string?> GetAccessTokenAsync(string refreshToken)
    {
        var hashedRefreshToken = RefreshTokenGenerator.HashRefreshToken(refreshToken);

        var refr = await _repository.GetRefreshTokenByHashAsync(hashedRefreshToken);

        if (refr is null)
            return null;

        if(refr.RevokedAt is not null || DateTime.UtcNow > refr.ExpiresAt)
        {
            if (refr.RevokedAt is null)
            {
                refr.RevokedAt = DateTime.UtcNow;
                await _repository.SaveChangesAsync();
            }
            return null;
        }

        var accessToken = JwtTokenGenerator.GenerateToken(_secretKey, refr.User.Id);

        return accessToken;
    }

    public async Task LogoutAsync(int userId)
    {
        var tokens = await _repository.GetRefreshTokensAsync(userId);

        foreach (RefreshToken token in tokens)
        {
            token.RevokedAt = DateTime.UtcNow;
        }
        await _repository.SaveChangesAsync();
    }
}
