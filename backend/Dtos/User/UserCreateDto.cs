namespace backend.Dtos.User;

public record class UserCreateDto
(
    string Username,
    string Password,
    string Description
);
