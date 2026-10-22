using System;
using backend.Dtos.User;

namespace backend.Services;

public interface IUserService
{
    Task<List<UserResponseDto>> GetAllAsync();
    Task<UserDetailedResponseDto?> GetByIdAsync(int id);
}
