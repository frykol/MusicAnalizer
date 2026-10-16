using System;
using System.Text.Json.Serialization;

namespace backend.Infrastructure.Media;

public record FfprobeMetadata
(
    [property: JsonPropertyName("format")]
    FfprobeFormat? Format,

    [property: JsonPropertyName("streams")]
    List<FfprobeStream?> Streams
);

public record FfprobeFormat
(
    [property: JsonPropertyName("fileName")]
    string FileName,
    [property: JsonPropertyName("size")]
    string Size
);

public record FfprobeStream
(
    [property: JsonPropertyName("codec_name")]
    string CodecName,
    [property: JsonPropertyName("sample_rate")]
    string SampleRate,
    [property: JsonPropertyName("channels")]
    int Channels,
    [property: JsonPropertyName("bit_rate")]
    string? Bitrate,
    [property: JsonPropertyName("duration")]
    string Duration
);
