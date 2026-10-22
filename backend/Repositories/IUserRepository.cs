using System;
using backend.Models;

namespace backend.Repositories;

public interface IUserRepository
{
    Task<bool> DoesUserExistsAsync(string username);
    Task<User> CreateUserAsync(User user);
    Task<List<User>> GetUsersAsync();
    Task<User?> GetUserByIdAsync(int id);

    Task<User?> GetUserByNameAsync(string username);
}
