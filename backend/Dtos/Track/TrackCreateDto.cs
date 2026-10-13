namespace backend.Dtos;

public record class TrackCreateDto(
    string Title,
    string Genre,
    int ArtistId
);
