using System;
using backend.Dtos.User;

namespace backend.Services;

public interface IAuthService
{
    Task<string> getAccessToken(UserCreateDto userCreate);
}
