using System;
using System.Globalization;
using System.Text.Json;
using backend.Core;

namespace backend.Infrastructure.Media;

public static class FfprobeMetadataReader
{
    private const int AudioStreamIndex = 0; 
    public static AudioMetadata? Read(JsonDocument json)
    {
        var response = JsonSerializer.Deserialize<FfprobeMetadata>(json) ?? throw new InvalidDataException("Invalid FFprobe response");

        var audioStream = response.Streams.FirstOrDefault();

        if (audioStream is null)
            return null;

        string codecName = audioStream.CodecName;
        int sampleRate = int.Parse(audioStream.SampleRate);
        int channels = audioStream.Channels;
        int? bitrate = null;
        if (audioStream.Bitrate is not null)
            bitrate = int.Parse(audioStream.Bitrate);
        
        double duration = double.Parse(audioStream.Duration, CultureInfo.InvariantCulture);

        Console.WriteLine($"JSON RESPONSE\n{response.Streams[0]}");

        return new AudioMetadata{CodecName = codecName, SampleRate = sampleRate, Channels = channels, Bitrate = bitrate, Duration = duration};
    }
}
