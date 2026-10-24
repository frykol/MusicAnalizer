using System;
using backend.Models;
using backend.Dtos;
using backend.Dtos.Track;
using backend.Services.Results;

namespace backend.Services;

public interface ITrackService
{
    Task<List<TrackResponseDto>> GetAllAsync();
    Task<TrackDetailedResponseDto?> GetByIdAsync(int id);
    Task<TrackResponseDto?> CreateAsync(TrackCreateDto trackCreate, User user);
    Task<ServiceResponse<TrackServiceStatus, TrackResponseDto>>  UpdateAsync(int id, TrackUpdateDto trackUpdate, int userId);
    Task<bool> DeleteAsync(int id, int userId);
}
