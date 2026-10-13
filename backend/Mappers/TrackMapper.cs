using System;
using backend.Dtos;
using backend.Models;

namespace backend.Mappers;

public static class TrackMapper
{
    public static TrackResponseDto toResponseDto(Track track)
    {
        return new TrackResponseDto(track.Id, track.Title, track.Genre);
    }
}
