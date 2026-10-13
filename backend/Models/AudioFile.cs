using System;

namespace backend.Models;

public class AudioFile
{
    public int Id { get; set; }
    public string OriginalFileName { get; set; } = String.Empty;
    public string StorageKey { get; set; } = String.Empty;

    public DateTime UploadedDate { get; set; } = DateTime.UtcNow;

    public int TrackId { get; set; }
    public Track Track { get; set;} = null!;

}
