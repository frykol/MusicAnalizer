using System;

namespace backend.Services;

public class AudioFileService : IAudioFileService
{
    private readonly string _storagePath;

    public AudioFileService(IConfiguration configuration, IWebHostEnvironment environment)
    {
        var relativePath = configuration["Storage:AudioPath"] ?? "Storage/audio";

        _storagePath = Path.GetFullPath(relativePath, environment.ContentRootPath);

        Directory.CreateDirectory(_storagePath);
    }
    public async Task<string> SaveAsync(IFormFile file){

        if (file is null || file.Length == 0)
            return "No file";

        

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        var fileName = $"{Guid.NewGuid():N}{extension}";

        var fullPath = Path.Combine(_storagePath, fileName);

        await using var stream = new FileStream(fullPath, FileMode.CreateNew);
        await file.CopyToAsync(stream);

        return fileName;
    }
}
