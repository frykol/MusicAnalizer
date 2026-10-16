using System;
using System.Text.Json;

namespace backend.Infrastructure.Media;

public static class FfprobeMetadataReader
{
    public static void Read(JsonDocument json)
    {
        var response = JsonSerializer.Deserialize<FfprobeMetadata>(json) ?? throw new InvalidDataException("Invalid FFprobe response");

        Console.WriteLine($"JSON RESPONSE\n{response}");
    }
}
