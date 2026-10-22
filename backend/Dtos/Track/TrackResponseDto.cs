using backend.Dtos.Artist;
using backend.Dtos.User;


namespace backend.Dtos;

public record class TrackResponseDto(
    int id,
    string Title,
    string Genre,
    UserResponseDto User
);
