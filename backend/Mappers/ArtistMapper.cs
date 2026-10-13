using System;
using backend.Dtos.Artist;
using backend.Models;

namespace backend.Mappers;

public static class ArtistMapper
{
    public static ArtistResponseDto toResponseDto(Artist artist)
    {
        return new ArtistResponseDto(artist.Id, artist.Name);
    }

    public static ArtistDetailedResponseDto toDetailedResponseDto(Artist artist)
    {
        var tracks = artist.Tracks.Select(TrackMapper.toResponseDto).ToList();
        return new ArtistDetailedResponseDto(artist.Id, artist.Name, tracks);
    }
}
