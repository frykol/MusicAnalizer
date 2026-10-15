using System;
using System.Diagnostics;
using System.Text.Json;

namespace backend.Core;

public static class AudioFileMetadata
{
    public static async Task<JsonDocument> GetMetadataAsync(string path)
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

        using var process = Process.Start(startInfo) ?? throw new Exception("Couldn't start FFprobe");

        var outputTask = process.StandardOutput.ReadToEndAsync();
        var errorTask = process.StandardError.ReadToEndAsync();

        await process.WaitForExitAsync();

        string output = await outputTask;
        string error = await errorTask;

        if (process.ExitCode != 0)
            throw new Exception($"FFprobe error: {error}");

        return JsonDocument.Parse(output);
    }
}
