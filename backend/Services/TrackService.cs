using System;
using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

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
}
