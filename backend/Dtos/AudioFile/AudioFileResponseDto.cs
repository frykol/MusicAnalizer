namespace backend.Dtos.AudioFile;

public record class AudioFileResponseDto
(
    string OriginalFileName,
    DateTime UploadedDate,
    double Duration
);
