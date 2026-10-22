using System;
using backend.Dtos.User;
using backend.Models;
using backend.Services.Results;

namespace backend.Services;

public interface IAuthService
{
    //TODO: Make valid user response dto
    Task<UserServiceResponse<User>> CreateUserAsync(UserCreateDto userCreate);
    Task<User?> GetByIdAsync(int? id);

    Task<UserServiceResponse<User>> LoginAsync(UserLoginDto userLogin);
}
