using System;

namespace backend.Core;

public class AudioMetadata
{
    public string CodecName {get; init;} = String.Empty;

    public int SampleRate {get; init;}
    public int Channels {get; init;}
    public int? Bitrate {get; init;}
    public double Duration {get; init;}
}
