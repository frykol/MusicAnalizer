namespace backend.Services.Results;

public enum UserServiceStatusResponse
{
    Ok, UserAlreadyExists, UserNotFound, InvalidCredentials
}

public record class UserServiceResponse<T>
{
    public UserServiceStatusResponse Status {get; init;}
    public T? Value {get; init;}
}
