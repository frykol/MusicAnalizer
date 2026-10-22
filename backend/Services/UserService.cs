using System;
using backend.Dtos.User;
using backend.Mappers;
using backend.Repositories;

namespace backend.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<UserResponseDto>> GetAllAsync()
    {
        var users = await _repository.GetUsersAsync();
        return users.Select(UserMapper.toResponseDto).ToList();
    }
    public async Task<UserDetailedResponseDto?> GetByIdAsync(int id)
    {
        var user = await _repository.GetUserByIdAsync(id);
        if (user is null)
            return null;
        return UserMapper.toDetailedResponseDto(user);
    }
}
