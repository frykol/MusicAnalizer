using System;

namespace backend.Core.File;

public static class LocalFileSaver
{
    public async static void SaveFile(IFormFile file, string filePath)
    {
        await using var stream = new FileStream(filePath, FileMode.CreateNew);
        await file.CopyToAsync(stream);
    }
}
