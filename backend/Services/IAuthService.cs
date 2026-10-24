using System;
using backend.Dtos.User;
using backend.Models;
using backend.Services.Results;

namespace backend.Services;

public interface IAuthService
{
    //TODO: Make valid user response dto
    Task<ServiceResponse<AuthServiceStatus, UserLoginResponseDto>> CreateUserAsync(UserCreateDto userCreate);
    Task<User?> GetByIdAsync(int? id);

    Task<ServiceResponse<AuthServiceStatus, UserLoginResponseDto>> LoginAsync(UserLoginDto userLogin);

    Task<string?> GetAccessTokenAsync(string refreshToken);

    Task LogoutAsync(int userId);
}
