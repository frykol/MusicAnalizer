using System.Text;
using backend.Context;
using backend.Dtos;
using backend.Repositories;
using backend.Services;
using backend.Services.Results;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

string secretKey = builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt key not configured");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                  options.TokenValidationParameters = new()
                  {
                    ValidateIssuer = true,
                    ValidIssuer = "MusicAnalizer",

                    ValidateAudience = true,
                    ValidAudience = "MusicAnalizerClient",

                    ValidateLifetime = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                      Encoding.UTF8.GetBytes(secretKey)
                    ),

                    ClockSkew = TimeSpan.Zero
                  };
                });

  builder.Services.AddAuthorization();

builder.Services.AddScoped<ITrackRepository, TrackRepository>();
builder.Services.AddScoped<ITrackService, TrackService>();

builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
builder.Services.AddScoped<IArtistService, ArtistService>();

builder.Services.AddScoped<IAudioFileRepository, AudioFileRepository>();
builder.Services.AddScoped<IAudioFileService, AudioFileService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseCors("Frontend");
app.Run();


public partial class Program{}