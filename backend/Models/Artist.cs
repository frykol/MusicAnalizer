using System;

namespace backend.Models;

public class Artist
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public List<Track> Tracks { get; set; } = [];

    public int UserId {get; set;}
    public User User {get; set;} = null!;
}
