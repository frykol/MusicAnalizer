using System;
using backend.Core;
using backend.Infrastructure.File;
using backend.Infrastructure.Media;
using backend.Models;
using backend.Repositories;
using backend.Services.Results;

using backend.Dtos.AudioFile;
using backend.Mappers;

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
    public async Task<AudioFileServiceStatus> SaveAsync(int trackId, IFormFile file, User user){

        var track = await _trackRepository.GetByIdAsync(trackId);

        if (track is null)
            return AudioFileServiceStatus.TrackNotFound;
        if (file is null || file.Length == 0)
            return AudioFileServiceStatus.IsNotMusicFile;

        var fileName = FileNameGenerator.GenerateName(file.FileName);

        var fullPath = Path.Combine(_storagePath, fileName);

        await LocalFileManager.SaveFileAsync(file, fullPath);


        using var stream = LocalFileManager.GetFileStream(fullPath);

        string hash = await Sha256Hasher.ComputeAsync(stream); 

        if (await _repository.ExistsByHashAsync(hash))
        {
            LocalFileManager.DeleteFile(fullPath);
            return AudioFileServiceStatus.FileAlreadyExists;
        }


        var audioFileValidatorResult = await AudioFileValidator.isMusicFileVaild(fullPath);
        
        if (!audioFileValidatorResult.IsSucces)
        {
            LocalFileManager.DeleteFile(fullPath);
            return AudioFileServiceStatus.IsNotMusicFile;
        }

    
        using var metadata = await AudioFileMetadata.GetMetadataAsync(fullPath);
    
        if (!metadata.IsSucces)
        {
            LocalFileManager.DeleteFile(fullPath);
            return AudioFileServiceStatus.IsNotMusicFile;
        }

        AudioMetadata? audioMetadata;

        try
        {
            audioMetadata = FfprobeMetadataReader.Read(metadata.Value!);
        }
        catch
        {
            LocalFileManager.DeleteFile(fullPath);
            return AudioFileServiceStatus.InvalidMetadata;
        }

        if(audioMetadata is null)
        {
            LocalFileManager.DeleteFile(fullPath);
            return AudioFileServiceStatus.InvalidMetadata;
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
            Hash = hash,

            Track = track,
            TrackId = trackId,

            User = user,
            UserId = user.Id
        };

        await _repository.CreateAsync(audioFile);

        //ONLY FOR TESTING
        //LocalFileManager.DeleteFile(fullPath);


        return AudioFileServiceStatus.IsSucces;
    }

    public async Task<List<AudioFileResponseDto>> GetAllAsync()
    {
        var audioFiles = await _repository.GetAllAsync();
        return audioFiles.Select(AudioFileMapper.toResponseDto).ToList();
    }
    public async Task<AudioFileResponseDto?> GetByIdAsync(int id)
    {
        var audioFile = await _repository.GetByIdAsync(id);
        if (audioFile is null)
            return null;

        return AudioFileMapper.toResponseDto(audioFile);
    }
    public async Task<AudioFileServiceStatus> DeleteByIdAsync(int id, int userId)
    {
        var audioFile = await _repository.GetByIdAsync(id);
        if (audioFile is null)
            return AudioFileServiceStatus.NotFound;
        
        if (audioFile.UserId != userId)
            return AudioFileServiceStatus.InvalidCredentials;
        
        string filePath = audioFile.StorageKey;
        await _repository.DeleteAsync(audioFile);
        LocalFileManager.DeleteFile(filePath);
        return AudioFileServiceStatus.IsSucces;
    }
}
