using System;
using backend.Models;

namespace backend.Repositories;

public interface IArtistRepository
{
    Task<List<Artist>> GetAllAsync();
    Task<Artist?> GetByIdAsync(int id);

    Task<Artist> CreateAsync(Artist createdArtist);
    Task<int> SaveChangesAsync();
    Task<bool> DeleteByIdAsync(int id);
    Task DeleteAsync(Artist artist);
}
