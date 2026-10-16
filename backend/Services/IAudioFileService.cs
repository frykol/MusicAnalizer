using System;

namespace backend.Services;

public interface IAudioFileService
{
    Task<AudioFileServiceResult> SaveAsync(IFormFile file);
}
