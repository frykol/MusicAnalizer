using System;
using backend.Dtos.AudioFile;
using backend.Models;

namespace backend.Services.Results;

public interface IAudioFileService
{
    Task<AudioFileServiceResult> SaveAsync(int trackId, IFormFile file, User user);

    Task<List<AudioFileResponseDto>> GetAllAsync();
    Task<AudioFileResponseDto?> GetByIdAsync(int id);
    Task<bool> DeleteByIdAsync(int id);
}
