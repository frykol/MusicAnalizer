using System;
using System.IO;

public static class LocalFileManager
{
    public async static Task SaveFileAsync(IFormFile file, string filePath)
    {
        await using var stream = new FileStream(filePath, FileMode.CreateNew);
        await file.CopyToAsync(stream);
    }

    public static void DeleteFile(string filePath)
    {
        try
        {
            File.Delete(filePath);
        }
        catch (Exception ex)
        {
            throw new Exception($"Couldn't delete file: {filePath}, error: {ex}");
        }
    }
}
