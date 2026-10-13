using System;
using backend.Models;
using backend.Dtos;
using backend.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Components.Web;

namespace backend.Services;

public class TrackService : ITrackService
{
    private readonly ITrackRepository _repository;

    public TrackService(ITrackRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Track>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Track?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Track> CreateAsync(TrackCreateDto createdTrack)
    {
        Track t = new Track{Title = createdTrack.Title, Genre = createdTrack.Genre};

        Track created = await _repository.CreateAsync(t);

        return created;
    }

    public async Task<Track?> UpdateAsync(int id, TrackUpdateDto trackUpdate)
    {
        return await _repository.UpdateAsync(id, trackUpdate, (src, dest) =>
        {
           dest.Title = src.Title;
           dest.Genre = src.Genre;
        });
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}
