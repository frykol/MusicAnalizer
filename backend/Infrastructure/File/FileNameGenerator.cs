using System;

namespace backend.Infrastructure.File;

public static class FileNameGenerator
{
    public static string GenerateName(string name)
    {
        var extension = Path.GetExtension(name).ToLowerInvariant();
        var fileName = $"{Guid.NewGuid():N}{extension}";

        return fileName;
    }
}
