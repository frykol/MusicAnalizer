using System;

namespace backend.Core.File;

public static class FileNameGenerator
{
    public static string GenerateName(IFormFile file)
    {
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        var fileName = $"{Guid.NewGuid():N}{extension}";

        return fileName;
    }
}
