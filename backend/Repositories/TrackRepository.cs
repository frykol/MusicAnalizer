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
        return await _context.Tracks.AsNoTracking().ToListAsync();
    }

    public async Task<Track?> GetByIdAsync(int id)
    {
        return await _context.Tracks.AsNoTracking().FirstOrDefaultAsync(track => track.Id == id);
    }

    public async Task<Track> CreateAsync(Track createdTrack)
    {
        _context.Tracks.Add(createdTrack);
        await _context.SaveChangesAsync();
        return createdTrack;
    }

    public async Task<Track?> UpdateAsync(int id, TrackUpdateDto trackUpdate, Action<TrackUpdateDto, Track> mapAction)
    {
        var track = await _context.Tracks.FindAsync(id);
        if (track is null)
            return null;

        mapAction(trackUpdate, track);

        await _context.SaveChangesAsync();
        return track;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var track = await _context.Tracks.FindAsync(id);

        if (track is null)
            return false;

        _context.Tracks.Remove(track);
        await _context.SaveChangesAsync();

        return true;
    }
}
