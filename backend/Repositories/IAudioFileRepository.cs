using System;
using backend.Models;

namespace backend.Repositories;

public interface IAudioFileRepository
{
    Task<AudioFile> CreateAsync(AudioFile createdAudioFile);
    Task<bool> ExistsByHashAsync(string hash);

    Task<AudioFile?> GetByIdAsync(int id);
    Task DeleteAsync(AudioFile audioFile); 
}
