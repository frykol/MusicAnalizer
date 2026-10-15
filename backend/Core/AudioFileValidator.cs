using System;

namespace backend.Core;

public static class AudioFileValidator
{
    public static AudioFileType isMp3(IFormFile file)
    {
        if(file.Length < 3)
            return AudioFileType.Unknown;
        Stream fileStream = file.OpenReadStream();
        byte[] buffer = new byte[3];

        fileStream.Read(buffer, 0, 3);
        fileStream.Close();
        if(buffer[0] == 0x49 && buffer[1] == 0x44 && buffer[2] == 0x33)
        {
            return AudioFileType.ID3v2;
        }
        if (buffer[0] == 0xFF && (buffer[1] == 0xFB || buffer[1] == 0xF3 || buffer[1] == 0xF2))
        {
            return AudioFileType.Raw;
        }

        return AudioFileType.Unknown;
    }
}
