using System;

namespace backend.Models;

public class AudioFile
{
    public int Id { get; set; }
    public string OriginalFileName { get; set; } = String.Empty;

    public string StorageKey { get; set; } = String.Empty;

    public DateTime UploadedDate { get; set; } = DateTime.UtcNow;

    public double Duration { get; set; }

    public string Codec { get; set; } = String.Empty;

    public int? Bitrate { get; set; }

    public int SampleRate { get; set; }

    public int Channels { get; set; }

    public string ChannelLayout { get; set; } = String.Empty;

    public long Size { get; set; }

    public required string Hash {get; set;}

    public int TrackId { get; set; }
    public Track Track { get; set;} = null!;

}
