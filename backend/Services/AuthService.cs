using System;
using backend.Core.Auth;
using backend.Dtos.User;
using backend.Models;
using backend.Repositories;
using backend.Services.Results;

namespace backend.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _repository;
    private readonly IUserRepository _userRepository;

    public AuthService(IAuthRepository repository, IUserRepository userRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
    }
    public async Task<UserServiceResponse<User>> CreateUserAsync(UserCreateDto userCreate)
    {
        bool userExists = await _userRepository.DoesUserExistsAsync(userCreate.Username);
        if (userExists)
            return new UserServiceResponse<User>{Status = UserServiceStatusResponse.UserAlreadyExists, Value = null};

        var hashedPassword = PasswordHasher.HashPassword(userCreate.Password);

        var user = new User
        {
          Username = userCreate.Username,
          PasswordHash = hashedPassword,
        };

        await _userRepository.CreateUserAsync(user);
        
        return new UserServiceResponse<User>{Status = UserServiceStatusResponse.Ok, Value = user};
    }

    public async Task<User?> GetByIdAsync(int? id)
    {
        if (id is null)
            return null;

        return await _userRepository.GetUserByIdAsync(id ?? default);
    }

    public async Task<UserServiceResponse<User>> LoginAsync(UserLoginDto userLogin)
    {
        var user = await _userRepository.GetUserByNameAsync(userLogin.Username);

        if (user is null)
            return new UserServiceResponse<User>{Status = UserServiceStatusResponse.UserNotFound, Value = null};

        var isPasswordCorrect = PasswordHasher.VerifyPassword(userLogin.Password, user.PasswordHash);

        if (!isPasswordCorrect)
            return new UserServiceResponse<User>{Status = UserServiceStatusResponse.InvalidCredentials, Value = null};

        return new UserServiceResponse<User>{Status = UserServiceStatusResponse.Ok, Value = user};
    }
}
