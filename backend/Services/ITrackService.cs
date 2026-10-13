using System;
using backend.Models;

namespace backend.Services;

public interface ITrackService
{
    Task<List<Track>> GetAllAsync();
}
