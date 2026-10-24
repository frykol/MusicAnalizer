using System;

namespace backend.Services.Results;
public enum AudioFileServiceStatus
{
    IsSucces,
    IsNotMusicFile,
    InvalidMetadata,
    FileAlreadyExists,
    TrackNotFound,
    IsTooLong,
    IsTooShort,

    NotFound,
    InvalidCredentials
}