using System;
using backend.Models;
using backend.Dtos;

namespace backend.Services;

public interface ITrackService
{
    Task<List<TrackResponseDto>> GetAllAsync();
    Task<TrackResponseDto?> GetByIdAsync(int id);
    Task<TrackResponseDto> CreateAsync(TrackCreateDto createdTrack);
    Task<TrackResponseDto?> UpdateAsync(int id, TrackUpdateDto trackUpdate);
    Task<bool> DeleteAsync(int id);
}
