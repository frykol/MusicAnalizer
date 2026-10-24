using System;
using backend.Models;

namespace MusicAnalizer.Seeder.Generators;

public class TrackGenerator
{
    private static List<string> genres = ["Pop", "Rock", "Hip-Hop", "R&B", "Soul", "Jazz", "Classical", "Reggae", "Country", "Electronic", "EDM", "Metal", "Blues", "Folk", "Latin", "Punk", "Funk", "Disco", "Gospel", "Afrobeats", "K-pop", "Hyperpop", "Ambient", "Techno", "House"];

    public List<Track> Generate(int[] userIds, int[] artistsIds, int numberOfTracks)
    {
        List<Track> tracks = [];

        int genresLen = genres.Count;

        for(int i = 0; i < numberOfTracks; i++)
        {
            var artistIdsIndex = Random.Shared.Next(artistsIds.Length);
            var userIdIndex = Random.Shared.Next(userIds.Length);

            var chosenGenre = genres[Random.Shared.Next(0, genresLen)];

            var track = new Track{ Title=$"Track_{i+1}", Genre=chosenGenre, ArtistId=artistsIds[artistIdsIndex], UserId=userIds[userIdIndex]};
            tracks.Add(track);
        }

        return tracks;
    }
}
