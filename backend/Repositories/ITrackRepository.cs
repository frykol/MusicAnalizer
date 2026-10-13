using System;
using backend.Dtos;
using backend.Models;

namespace backend.Repositories;

public interface ITrackRepository
{
    Task<List<Track>> GetAllAsync();
    Task<Track?> GetByIdAsync(int id);
    Task<Track> CreateAsync(Track createdTrack);
    Task<Track?> UpdateAsync(int id, TrackUpdateDto trackUpdate, Action<TrackUpdateDto, Track> mapAction);
    Task<bool> DeleteAsync(int id);
}
