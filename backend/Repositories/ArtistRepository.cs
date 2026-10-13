using System;
using backend.Context;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class ArtistRepository : IArtistRepository
{
    private readonly AppDbContext _context;

    public ArtistRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Artist>> GetAllAsync()
    {
        return await _context.Artists.Include(artist => artist.Tracks).ToListAsync();
    }
    public async Task<Artist?> GetByIdAsync(int id)
    {
        return await _context.Artists.Include(artist => artist.Tracks).FirstOrDefaultAsync(artist => artist.Id == id);
    }

    public async Task<Artist> CreateAsync(Artist createdArtist){
        _context.Artists.Add(createdArtist);
        await _context.SaveChangesAsync();
        return createdArtist;
    }
    public async Task<int> SaveChangesAsync(){
        return await _context.SaveChangesAsync();
    }
    public async Task<bool> DeleteByIdAsync(int id){
        var artist = await _context.Artists.FindAsync(id);
        if (artist is null)
            return false;
        _context.Artists.Remove(artist);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task DeleteAsync(Artist artist)
    {   
        _context.Artists.Remove(artist);
        await _context.SaveChangesAsync();
    }
}
