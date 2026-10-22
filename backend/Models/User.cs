using System;

namespace backend.Models;

public class User
{
    public int Id {get; set;}
    public required string Username {get; set;} 
    public required string PasswordHash {get; set;}
    public string Description {get; set;} = String.Empty;

    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;
    public DateTime UpdatedAt {get; set;} = DateTime.UtcNow;
    
    public List<Artist> Artists {get; set;} = [];
    public List<Track> Tracks {get; set;} = [];

    public List<AudioFile> AudioFiles {get; set;} = [];
}
