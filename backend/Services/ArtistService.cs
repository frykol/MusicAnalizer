using System;
using backend.Repositories;
using backend.Models;
using backend.Mappers;
using backend.Dtos.Artist;
using Microsoft.VisualBasic;
using backend.Services.Results;

namespace backend.Services;

public class ArtistService : IArtistService
{
    private readonly IArtistRepository _repository;

    public ArtistService(IArtistRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ArtistResponseDto>> GetAllAsync()
    {
        var artists = await _repository.GetAllAsync();
        return artists.Select(ArtistMapper.toResponseDto).ToList();
    }

    public async Task<ArtistResponseDto> CreateAsync(ArtistCreateDto artistCreate, User user)
    {
        Artist artist = new Artist{Name = artistCreate.Name, UserId = user.Id, User = user}; 
        var created = await _repository.CreateAsync(artist);
        return ArtistMapper.toResponseDto(created);
    }
    public async Task<ArtistDetailedResponseDto?> GetByIdAsync(int id)
    {
        var artist = await _repository.GetByIdAsync(id);
        if (artist is null)
            return null;

        return ArtistMapper.toDetailedResponseDto(artist);
    }
    public async Task<ServiceResponse<ArtistServiceStatus, ArtistDetailedResponseDto>> UpdateAsync(int id, ArtistUpdateDto artistUpdate, int userId)
    {
        var artist = await _repository.GetByIdAsync(id);
        if (artist is null)
            return new(){Status = ArtistServiceStatus.ArtistNotFound, Value = null};

        if (artist.UserId != userId)
            return new(){Status = ArtistServiceStatus.InvalidCredentials, Value = null};
        
        artist.Name = artistUpdate.Name;

        await _repository.SaveChangesAsync();

        return new(){Status = ArtistServiceStatus.Ok, Value = ArtistMapper.toDetailedResponseDto(artist)};
    }
    public async Task<DeleteArtistResult> DeleteAsync(int id, int userId)
    {
        var artist = await _repository.GetByIdAsync(id);
        if (artist is null)
            return DeleteArtistResult.NotFound;
            
        if (artist.UserId != userId)
            return DeleteArtistResult.InvalidPermission;

        if (artist.Tracks.Count > 0)
            return DeleteArtistResult.HasTracks;

        
        await _repository.DeleteAsync(artist);
        return DeleteArtistResult.Success;
    }
}
