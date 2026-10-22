using System;
using backend.Dtos.User;
using backend.Models;

namespace backend.Mappers;

public static class UserMapper
{
    public static UserResponseDto toResponseDto(User user)
    {
        return new UserResponseDto(user.Username);
    }

    public static UserDetailedResponseDto toDetailedResponseDto(User user)
    {   
        var artistsDto = user.Artists.Select(ArtistMapper.toResponseDto).ToList();
        var tracksDto = user.Tracks.Select(TrackMapper.toResponseDto).ToList();
        var audioFilesDto = user.AudioFiles.Select(AudioFileMapper.toResponseDto).ToList();
        return new UserDetailedResponseDto(user.Username, artistsDto, tracksDto, audioFilesDto);
    }
}
