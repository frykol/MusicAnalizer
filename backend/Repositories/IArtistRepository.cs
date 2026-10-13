using System;
using backend.Models;

namespace backend.Repositories;

public interface IArtistRepository
{
    Task<List<Artist>> GetAllAsync();
}
