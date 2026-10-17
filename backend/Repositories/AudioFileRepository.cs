using System;
using backend.Context;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class AudioFileRepository : IAudioFileRepository
{
    private readonly AppDbContext _context;

    public AudioFileRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsByHashAsync(string hash)
    {
        return await _context.AudioFiles.AnyAsync(a => a.Hash == hash);
    }

    public async Task<List<AudioFile>> GetAllAsync()
    {
        return await _context.AudioFiles.AsNoTracking().ToListAsync();
    }

    public async Task<AudioFile> CreateAsync(AudioFile createdAudioFile)
    {
        _context.AudioFiles.Add(createdAudioFile);
        await _context.SaveChangesAsync();
        return createdAudioFile;
    }

    public async Task<AudioFile?> GetByIdAsync(int id)
    {
        return await _context.AudioFiles.FirstOrDefaultAsync(a => a.Id == id);
    }
    public async Task DeleteAsync(AudioFile audioFile)
    {
        _context.AudioFiles.Remove(audioFile);
        await _context.SaveChangesAsync();
    }
}
