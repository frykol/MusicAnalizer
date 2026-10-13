using System;
using backend.Models;

namespace backend.Repositories;

public interface ITrackRepository
{
    Task<List<Track>> GetAllAsync();
}
