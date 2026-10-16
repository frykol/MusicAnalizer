using System;
using backend.Context;
using backend.Models;

namespace backend.Repositories;

public class AudioFileRepository : IAudioFileRepository
{
    private readonly AppDbContext _context;

    public AudioFileRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<AudioFile> CreateAsync(AudioFile createdAudioFile)
    {
        _context.AudioFiles.Add(createdAudioFile);
        await _context.SaveChangesAsync();
        return createdAudioFile;
    }
}
