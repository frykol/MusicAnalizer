using System;
using backend.Context;
using Microsoft.EntityFrameworkCore;
using MusicAnalizer.Seeder.Generators;

namespace MusicAnalizer.Seeder;

public class DatabaseSeeder
{
    private readonly AppDbContext _context;
    private readonly UserGenerator _userGenerator;
    private readonly ArtistGenerator _artistGenerator;
    private readonly TrackGenerator _trackGenerator;

    public DatabaseSeeder(
        AppDbContext context, 
        UserGenerator userGenerator,
        ArtistGenerator artistGenerator,
        TrackGenerator trackGenerator)
    {
        _context = context;
        _userGenerator = userGenerator;
        _artistGenerator = artistGenerator;
        _trackGenerator = trackGenerator;
    }

    public async Task CleanAsync()
    {
            await _context.Database.ExecuteSqlRawAsync("""
            TRUNCATE TABLE
                "AudioFiles",
                "Tracks",
                "Artists",
                "RefreshTokens",
                "Users"
            RESTART IDENTITY CASCADE;
            """);
    }

    public async Task SeedAsync(bool cleanBeforeSeed)
    {
        if(cleanBeforeSeed)
            await CleanAsync();

        var users = await _userGenerator.Generate(200);

        _context.Users.AddRange(users);
        await _context.SaveChangesAsync();

        var userIds = users.Select(u => u.Id).ToArray();

        var artists = _artistGenerator.Generate(userIds, 200);

        _context.Artists.AddRange(artists);
        await _context.SaveChangesAsync();

        var artistIds = artists.Select(a => a.Id).ToArray();

        var tracks = _trackGenerator.Generate(userIds, artistIds, 200);

        _context.Tracks.AddRange(tracks);
        await _context.SaveChangesAsync();
    }
}
