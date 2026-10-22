using System;
using backend.Dtos;
using backend.Dtos.AudioFile;
using backend.Dtos.Track;
using backend.Dtos.User;
using backend.Models;

namespace backend.Mappers;

public static class TrackMapper
{
    public static TrackResponseDto toResponseDto(Track track)
    {   
        var userResponseDto = UserMapper.toResponseDto(track.User);

        return new TrackResponseDto(track.Id, track.Title, track.Genre, userResponseDto);
    }

    public static TrackDetailedResponseDto toDetailedResponseDto(Track track)
    {
        var userResponseDto = UserMapper.toResponseDto(track.User);

        AudioFileResponseDto? audioFileResponse = null;
        if (track.AudioFile is not null)
            audioFileResponse = AudioFileMapper.toResponseDto(track.AudioFile);
        return new TrackDetailedResponseDto(track.Id, track.Title, track.Genre, ArtistMapper.toResponseDto(track.Artist), audioFileResponse, userResponseDto);
    }
}
