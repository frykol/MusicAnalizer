using System;
using Microsoft.EntityFrameworkCore;
using backend.Models;


namespace backend.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options) {}

    public DbSet<Artist> Artists => Set<Artist>();
    public DbSet<Track> Tracks => Set<Track>();

    public DbSet<AudioFile> AudioFiles => Set<AudioFile>();

    public DbSet<User> Users => Set<User>();

    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AudioFile>().HasIndex(a => a.Hash).IsUnique();

        modelBuilder.Entity<RefreshToken>().HasIndex(rf => rf.TokenHash).IsUnique();
    }
}
