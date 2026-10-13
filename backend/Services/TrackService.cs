using System;
using backend.Models;
using backend.Dtos;
using backend.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Components.Web;
using backend.Mappers;

namespace backend.Services;

public class TrackService : ITrackService
{
    private readonly ITrackRepository _repository;

    public TrackService(ITrackRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<TrackResponseDto>> GetAllAsync()
    {
        var tracks = await _repository.GetAllAsync();
        return tracks.Select(track =>
        {
            return TrackMapper.toResponseDto(track);
        }).ToList();
    }

    public async Task<TrackResponseDto?> GetByIdAsync(int id)
    {
        var track = await _repository.GetByIdAsync(id);

        if (track is null)
            return null;

        return TrackMapper.toResponseDto(track);
    }

    public async Task<TrackResponseDto> CreateAsync(TrackCreateDto createdTrack)
    {
        Track t = new Track{Title = createdTrack.Title, Genre = createdTrack.Genre};

        Track created = await _repository.CreateAsync(t);

        return TrackMapper.toResponseDto(created);
    }

    public async Task<TrackResponseDto?> UpdateAsync(int id, TrackUpdateDto trackUpdate)
    {
        var toUpdate = await _repository.GetByIdAsync(id);
        if (toUpdate is null)
            return null;

        toUpdate.Title = trackUpdate.Title;
        toUpdate.Genre = trackUpdate.Genre;

        await _repository.SaveChangesAsync();

        return TrackMapper.toResponseDto(toUpdate);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}
