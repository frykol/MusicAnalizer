using backend.Context;
using backend.Dtos;
using backend.Repositories;
using backend.Services;
using backend.Services.Results;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(
  builder.Configuration.GetConnectionString("DefaultConnection")
));

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

builder.Services.AddScoped<ITrackRepository, TrackRepository>();
builder.Services.AddScoped<ITrackService, TrackService>();

builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
builder.Services.AddScoped<IArtistService, ArtistService>();

builder.Services.AddScoped<IAudioFileRepository, AudioFileRepository>();
builder.Services.AddScoped<IAudioFileService, AudioFileService>();

var app = builder.Build();

app.MapControllers();
app.UseCors("Frontend");
app.Run();
