namespace backend.Dtos.Artist;

public record class ArtistDetailedResponseDto
(
    int Id,
    string Name,
    List<TrackResponseDto> tracks
);
