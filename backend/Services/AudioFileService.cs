using System;
using backend.Core;
using backend.Core.File;

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

        AudioFileType audioType = AudioFileValidator.isMp3(file);

        if (audioType == AudioFileType.Unknown)
            return "Not mp3";

        //AudioFileMetadataExtractor.Mp3Metadata(file, audioType);

        // var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        // var fileName = $"{Guid.NewGuid():N}{extension}";

        var fileName = FileNameGenerator.GenerateName(file);

        var fullPath = Path.Combine(_storagePath, fileName);

        // await using var stream = new FileStream(fullPath, FileMode.CreateNew);
        // await file.CopyToAsync(stream);

        LocalFileSaver.SaveFile(file, fullPath);

        using var metadata = await AudioFileMetadata.GetMetadataAsync(fullPath);

        string duration = metadata.RootElement.GetProperty("format").GetProperty("duration").GetString()!;

        Console.WriteLine($"Duration: {duration}");

        return fileName;
    }
}
