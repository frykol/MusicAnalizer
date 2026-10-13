using System;
using backend.Dtos;
using backend.Dtos.Track;
using backend.Models;

namespace backend.Mappers;

public static class TrackMapper
{
    public static TrackResponseDto toResponseDto(Track track)
    {
        return new TrackResponseDto(track.Id, track.Title, track.Genre);
    }

    public static TrackDetailedResponseDto toDetailedResponseDto(Track track)
    {
        return new TrackDetailedResponseDto(track.Id, track.Title, track.Genre, ArtistMapper.toResponseDto(track.Artist));
    }
}
