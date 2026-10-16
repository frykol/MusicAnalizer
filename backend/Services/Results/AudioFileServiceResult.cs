using System;

public enum AudioFileServiceResult
{
    IsSucces,
    IsNotMusicFile,
    InvalidMetadata,
    TrackNotFound,
    IsTooLong,
    IsTooShort
}