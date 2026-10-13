using System;
using backend.Models;
using backend.Dtos.Artist;
using backend.Services.Results;

namespace backend.Services;

public interface IArtistService
{
    Task<List<ArtistDetailedResponseDto>> GetAllAsync();
    Task<ArtistDetailedResponseDto?> GetByIdAsync(int id);
    Task<ArtistResponseDto> CreateAsync(ArtistCreateDto artistCreate);
    Task<ArtistDetailedResponseDto?> UpdateAsync(int id, ArtistUpdateDto artistUpdate);
    Task<DeleteArtistResult> DeleteAsync(int id);
}
