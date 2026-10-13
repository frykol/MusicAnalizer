using System;

namespace backend.Models;

public class Track
{
    public int Id {get; set; }
    public required string Title {get; set; }
    public required string Genre {get; set; }
}
