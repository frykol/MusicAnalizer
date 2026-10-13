using backend.Dtos.Artist;

namespace backend.Dtos;

public record class TrackResponseDto(
    int id,
    string Title,
    string Genre
);
