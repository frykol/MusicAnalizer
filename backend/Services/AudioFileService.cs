using System;
using backend.Core;
using backend.Infrastructure.File;
using backend.Infrastructure.Media;
using backend.Models;
using backend.Repositories;
using backend.Services.Results;

namespace backend.Services;

public class AudioFileService : IAudioFileService
{
    private readonly IAudioFileRepository _repository;
    private readonly ITrackRepository _trackRepository;
    private readonly string _storagePath;

    public AudioFileService(IConfiguration configuration, IWebHostEnvironment environment,IAudioFileRepository repository, ITrackRepository trackRepository)
    {
        _repository = repository;
        _trackRepository = trackRepository;
        var relativePath = configuration["Storage:AudioPath"] ?? "Storage/audio";

        _storagePath = Path.GetFullPath(relativePath, environment.ContentRootPath);

        Directory.CreateDirectory(_storagePath);
    }
    public async Task<AudioFileServiceResult> SaveAsync(int trackId, IFormFile file){

        var track = await _trackRepository.GetByIdAsync(trackId);

        if (track is null)
            return AudioFileServiceResult.TrackNotFound;
        if (file is null || file.Length == 0)
            return AudioFileServiceResult.IsNotMusicFile;

        var fileName = FileNameGenerator.GenerateName(file.FileName);

        var fullPath = Path.Combine(_storagePath, fileName);

        await LocalFileManager.SaveFileAsync(file, fullPath);

        var audioFileValidatorResult = await AudioFileValidator.isMusicFileVaild(fullPath);
        
        if (!audioFileValidatorResult.IsSucces)
        {
            LocalFileManager.DeleteFile(fullPath);
            return AudioFileServiceResult.IsNotMusicFile;
        }


        using var metadata = await AudioFileMetadata.GetMetadataAsync(fullPath);

        if (!metadata.IsSucces)
        {
            LocalFileManager.DeleteFile(fullPath);
            return AudioFileServiceResult.IsNotMusicFile;
        }

        AudioMetadata? audioMetadata;

        try
        {
            audioMetadata = FfprobeMetadataReader.Read(metadata.Value!);
        }
        catch
        {
            LocalFileManager.DeleteFile(fullPath);
            return AudioFileServiceResult.InvalidMetadata;
        }

        if(audioMetadata is null)
        {
            LocalFileManager.DeleteFile(fullPath);
            return AudioFileServiceResult.InvalidMetadata;
        }

        var audioFile = new AudioFile
        {
            OriginalFileName = file.FileName,
            StorageKey = fullPath,
            Size = file.Length,
            Duration = audioMetadata.Duration,
            Codec = audioMetadata.CodecName,
            Bitrate = audioMetadata.Bitrate,
            SampleRate = audioMetadata.SampleRate,
            Channels = audioMetadata.Channels,

            Track = track,
            TrackId = trackId
        };

        await _repository.CreateAsync(audioFile);

        //ONLY FOR TESTING
        //LocalFileManager.DeleteFile(fullPath);


        return AudioFileServiceResult.IsSucces;
    }
}
