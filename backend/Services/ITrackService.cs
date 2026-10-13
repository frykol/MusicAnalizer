using System;
using backend.Models;
using backend.Dtos;

namespace backend.Services;

public interface ITrackService
{
    Task<List<Track>> GetAllAsync();
    Task<Track?> GetByIdAsync(int id);
    Task<Track> CreateAsync(TrackCreateDto createdTrack);
    Task<Track?> UpdateAsync(int id, TrackUpdateDto trackUpdate);
    Task<bool> DeleteAsync(int id);
}
