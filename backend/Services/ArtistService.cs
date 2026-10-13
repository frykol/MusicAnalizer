using System;
using backend.Repositories;
using backend.Models;
using backend.Mappers;
using backend.Dtos.Artist;

namespace backend.Services;

public class ArtistService : IArtistService
{
    private readonly IArtistRepository _repository;

    public ArtistService(IArtistRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ArtistDetailedResponseDto>> GetAllAsync()
    {
        var artists = await _repository.GetAllAsync();
        return artists.Select(ArtistMapper.toDetailedResponseDto).ToList();
    }
}
