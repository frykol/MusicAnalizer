using System;
using backend.Models;
using backend.Dtos.Artist;

namespace backend.Services;

public interface IArtistService
{
    Task<List<ArtistDetailedResponseDto>> GetAllAsync();
}
