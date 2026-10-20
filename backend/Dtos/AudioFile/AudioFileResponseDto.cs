namespace backend.Dtos.AudioFile;

public record class AudioFileResponseDto
(
    string OriginalFileName,
    DateTime UploadedDate,
    double Duration,
    string Codec,
    int? Bitrate,
    int SampleRate,
    int Channels,
    long Size
);