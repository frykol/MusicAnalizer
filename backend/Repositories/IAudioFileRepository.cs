using System;
using backend.Models;

namespace backend.Repositories;

public interface IAudioFileRepository
{
    Task<AudioFile> CreateAsync(AudioFile createdAudioFile);
}
