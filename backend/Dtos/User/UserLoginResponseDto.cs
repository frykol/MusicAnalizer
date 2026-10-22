namespace backend.Dtos.User;

public record class UserLoginResponseDto
(
    UserResponseDto User,
    string JwtToken,
    string RefreshToken
);
