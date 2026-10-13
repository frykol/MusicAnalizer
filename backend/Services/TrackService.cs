using System;
using backend.Models;
using backend.Dtos;
using backend.Dtos.Track;
using backend.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Components.Web;
using backend.Mappers;

namespace backend.Services;

public class TrackService : ITrackService
{
    private readonly ITrackRepository _repository;
    private readonly IArtistRepository _artistRepository;

    public TrackService(ITrackRepository repository, IArtistRepository artistRepository)
    {
        _repository = repository;
        _artistRepository = artistRepository;
    }

    public async Task<List<TrackResponseDto>> GetAllAsync()
    {
        var tracks = await _repository.GetAllAsync();
        return tracks.Select(TrackMapper.toResponseDto).ToList();
    }

    public async Task<TrackDetailedResponseDto?> GetByIdAsync(int id)
    {
        var track = await _repository.GetByIdAsync(id);

        if (track is null)
            return null;

        return TrackMapper.toDetailedResponseDto(track);
    }

    public async Task<TrackResponseDto?> CreateAsync(TrackCreateDto trackCreate)
    {
        var artist = await _artistRepository.GetByIdAsync(trackCreate.ArtistId);
        if (artist is null)
            return null;

        Track t = new Track{Title = trackCreate.Title, Genre = trackCreate.Genre, ArtistId = trackCreate.ArtistId, Artist = artist};

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
