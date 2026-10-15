using System;
using System.Diagnostics;
using Microsoft.AspNetCore.StaticAssets;

namespace backend.Core;
public record class AudioFileValidatorResult
{
    public bool IsSucces;
    public string ErrorMessage = String.Empty;  
};
public static class AudioFileValidator
{

    //Only for testing/educational purposes
    public static AudioFileType isMp3(IFormFile file)
    {
        if(file.Length < 3)
            return AudioFileType.Unknown;
        Stream fileStream = file.OpenReadStream();
        byte[] buffer = new byte[3];

        fileStream.Read(buffer, 0, 3);
        fileStream.Close();
        if(buffer[0] == 0x49 && buffer[1] == 0x44 && buffer[2] == 0x33)
        {
            return AudioFileType.ID3v2;
        }
        if (buffer[0] == 0xFF && (buffer[1] == 0xFB || buffer[1] == 0xF3 || buffer[1] == 0xF2))
        {
            return AudioFileType.Raw;
        }

        return AudioFileType.Unknown;
    }

    public async static Task<AudioFileValidatorResult> isMusicFileVaild(string filePath)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = "ffmpeg",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        startInfo.ArgumentList.Add("-xerror");
        startInfo.ArgumentList.Add("-v");
        startInfo.ArgumentList.Add("error");
        startInfo.ArgumentList.Add("-i");
        startInfo.ArgumentList.Add(filePath);
        startInfo.ArgumentList.Add("-f");
        startInfo.ArgumentList.Add("null");
        startInfo.ArgumentList.Add("-");

        using var process = Process.Start(startInfo);
        if (process == null)
            return new AudioFileValidatorResult{IsSucces = false, ErrorMessage = "Could not start ffmpeg"};

        var outputTask = process.StandardOutput.ReadToEndAsync();
        var errorTask = process.StandardError.ReadToEndAsync();

        string output = await outputTask;
        string error = await errorTask;

        await process.WaitForExitAsync();

        

        if (process.ExitCode != 0)
            return new AudioFileValidatorResult{IsSucces = false, ErrorMessage = $"ffpmeg error: {error}"};

        return new AudioFileValidatorResult{IsSucces = true};
    }
}
