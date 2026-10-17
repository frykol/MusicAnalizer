using System;
using backend.Dtos.AudioFile;

namespace backend.Services.Results;

public interface IAudioFileService
{
    Task<AudioFileServiceResult> SaveAsync(int trackId, IFormFile file);

    Task<List<AudioFileResponseDto>> GetAllAsync();
    Task<AudioFileResponseDto?> GetByIdAsync(int id);
    Task<bool> DeleteByIdAsync(int id);
}
