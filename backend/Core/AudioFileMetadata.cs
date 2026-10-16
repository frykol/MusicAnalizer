using System;
using System.Diagnostics;
using System.Text.Json;

namespace backend.Core;

public record class AudioFileMetadataResponse : IDisposable
{
    public bool IsSucces {get; init;}
    public string ErrorMessage {get; init;} = String.Empty;  

    public JsonDocument? Value {get; init;}


    public void Dispose()
    {
        Value?.Dispose();
    }
}
public static class AudioFileMetadata
{
    public static async Task<AudioFileMetadataResponse> GetMetadataAsync(string path)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = "ffprobe",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        startInfo.ArgumentList.Add("-v");
        startInfo.ArgumentList.Add("error");
        startInfo.ArgumentList.Add("-show_entries");
        startInfo.ArgumentList.Add("format=duration:stream=codec_name,sample_rate,channels");
        startInfo.ArgumentList.Add("-of");
        startInfo.ArgumentList.Add("json");
        startInfo.ArgumentList.Add(path);

        using var process = Process.Start(startInfo);

        if (process is null)
            return new AudioFileMetadataResponse{IsSucces = false, ErrorMessage="Couldn't start FFprobe"};

        var outputTask = process.StandardOutput.ReadToEndAsync();
        var errorTask = process.StandardError.ReadToEndAsync();

        await process.WaitForExitAsync();

        string output = await outputTask;
        string error = await errorTask;

        if (process.ExitCode != 0)
            return new AudioFileMetadataResponse{IsSucces = false, ErrorMessage=$"FFprobe error: {error}"};

        var outputJson = JsonDocument.Parse(output);

        return new AudioFileMetadataResponse{IsSucces = true, Value = outputJson};
    }
}
