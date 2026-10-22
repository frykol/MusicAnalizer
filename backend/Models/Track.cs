using System;

namespace backend.Models;

public class Track
{
    public int Id {get; set; }
    public required string Title {get; set; }
    public required string Genre {get; set; }

    public int ArtistId { get; set; }

    public Artist Artist { get; set; } = null!;
    public AudioFile? AudioFile {get; set;}

    public int UserId {get; set;}
    public User User {get; set;} = null!;
}
