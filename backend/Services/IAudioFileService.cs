using System;

namespace backend.Services;

public interface IAudioFileService
{
    Task<string> SaveAsync(IFormFile file);
}
