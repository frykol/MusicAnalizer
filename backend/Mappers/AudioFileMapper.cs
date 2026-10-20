using System;
using backend.Dtos.AudioFile;
using backend.Models;

namespace backend.Mappers;

public static class AudioFileMapper
{
    public static AudioFileResponseDto toResponseDto(AudioFile audioFile)
    {
        return new AudioFileResponseDto(audioFile.OriginalFileName, audioFile.UploadedDate, audioFile.Duration, audioFile.Codec, audioFile.Bitrate, audioFile.SampleRate, audioFile.Channels, audioFile.Size);
    }
}
