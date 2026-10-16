namespace backend.Dtos.Track;
using backend.Dtos.Artist;
using backend.Dtos.AudioFile;

public record class TrackDetailedResponseDto(
    int id,
    string Title,
    string Genre,
    ArtistResponseDto artist,
    AudioFileResponseDto? AudioFile
);
