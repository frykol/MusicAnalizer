namespace backend.Dtos.Track;
using backend.Dtos.Artist;

public record class TrackDetailedResponseDto(
    int id,
    string Title,
    string Genre,
    ArtistResponseDto artist
);
