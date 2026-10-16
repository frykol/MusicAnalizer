using System;
using System.Text.Json.Serialization;

namespace backend.Infrastructure.Media;

public record FfprobeMetadata
(
    [property: JsonPropertyName("format")]
    FfprobeFormat Format
);

public record FfprobeFormat
(
    [property: JsonPropertyName("duration")]
    string Duration
);
