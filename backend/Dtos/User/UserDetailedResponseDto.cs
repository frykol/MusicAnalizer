using backend.Dtos.Artist;
using backend.Dtos.AudioFile;

namespace backend.Dtos.User;

public record class UserDetailedResponseDto
(
    string Username,
    List<ArtistResponseDto> Artist,
    List<TrackResponseDto> Track,
    List<AudioFileResponseDto> AudioFile
);
