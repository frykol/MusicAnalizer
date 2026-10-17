using System;

namespace backend.Services.Results;
public enum AudioFileServiceResult
{
    IsSucces,
    IsNotMusicFile,
    InvalidMetadata,
    FileAlreadyExists,
    TrackNotFound,
    IsTooLong,
    IsTooShort
}