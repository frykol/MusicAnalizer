using System;
using backend.Dtos;
using backend.Models;

namespace backend.Repositories;

public interface ITrackRepository
{
    Task<List<Track>> GetAllAsync();
    Task<Track?> GetByIdAsync(int id);
    Task<Track> CreateAsync(Track createdTrack);
    Task<int> SaveChangesAsync();
    Task DeleteAsync(Track track);
}
