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


        var fileName = FileNameGenerator.GenerateName(file);

        var fullPath = Path.Combine(_storagePath, fileName);

        await LocalFileManager.SaveFileAsync(file, fullPath);

        var audioFileValidatorResult = await AudioFileValidator.isMusicFileVaild(fullPath);
        
        if (!audioFileValidatorResult.IsSucces)
        {
            LocalFileManager.DeleteFile(fullPath);
            return "Error for test";
        }


        using var metadata = await AudioFileMetadata.GetMetadataAsync(fullPath);

        string duration = metadata.RootElement.GetProperty("format").GetProperty("duration").GetString()!;

        Console.WriteLine($"Duration: {duration}");

        return fileName;
    }
}
