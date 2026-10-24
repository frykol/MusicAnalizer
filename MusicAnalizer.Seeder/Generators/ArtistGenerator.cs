using System;
using backend.Models;

namespace MusicAnalizer.Seeder.Generators;

public class ArtistGenerator
{
    public List<Artist> Generate(int[] userIds, int numberOfArtists)
    {
        List<Artist> artists = [];
        for(int i = 0; i < numberOfArtists; i++)
        {
            var userIdIndex = Random.Shared.Next(userIds.Length);
            var artist = new Artist{ Name=$"Artist_{i+1}", UserId=userIds[userIdIndex] };
            artists.Add(artist);
        }
        return artists;
    }
}
