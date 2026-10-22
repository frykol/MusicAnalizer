using System;
using backend.Context;
using backend.Models;
using backend.Dtos;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class TrackRepository : ITrackRepository
{
    private readonly AppDbContext _context;

    public TrackRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Track>> GetAllAsync()
    {
        return await _context.Tracks.AsNoTracking().Include(t => t.Artist).Include(t => t.User).ToListAsync();
    }

    public async Task<Track?> GetByIdAsync(int id)
    {
        return await _context.Tracks.Include(t => t.Artist).Include(t => t.AudioFile).Include(t => t.User).FirstOrDefaultAsync(track => track.Id == id);
    }

    public async Task<Track> CreateAsync(Track createdTrack)
    {
        _context.Tracks.Add(createdTrack);
        await _context.SaveChangesAsync();
        return createdTrack;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Track track)
    {
        _context.Tracks.Remove(track);
        await _context.SaveChangesAsync();
    }
}
