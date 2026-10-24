

using backend.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MusicAnalizer.Seeder;
using MusicAnalizer.Seeder.Generators;

var builder = Host.CreateApplicationBuilder(args);

var configPath = Path.GetFullPath(
    "../backend/appsettings.Development.json",
    builder.Environment.ContentRootPath
);

builder.Configuration.AddJsonFile(configPath, optional: false);

builder.Services.AddDbContext<AppDbContext>(options =>
{
   options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")); 
});

builder.Services.AddScoped<UserGenerator>();
builder.Services.AddScoped<ArtistGenerator>();
builder.Services.AddScoped<TrackGenerator>();
builder.Services.AddScoped<DatabaseSeeder>();

using var host = builder.Build();

await using var scope = host.Services.CreateAsyncScope();

var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();


bool clean = args.Contains("--clean");

Console.WriteLine($"Clean Database: {clean}");


await seeder.SeedAsync(clean);


