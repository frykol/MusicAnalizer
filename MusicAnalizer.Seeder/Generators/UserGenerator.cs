using System;
using System.Security.Cryptography;
using backend.Core.Auth;
using backend.Models;
using Microsoft.Extensions.Configuration;

namespace MusicAnalizer.Seeder.Generators;

public class UserGenerator
{   
    private static List<string> adjectives = [
    "Brave", "Cosmic", "Swift", "Silent", "Neon", "Shadow", "Lunar", "Solar", 
    "Frost", "Blaze", "Vortex", "Quantum", "Hidden", "Echo", "Mystic", "Crimson", 
    "Aero", "Hyper", "Cubic", "Phantom"
    ];

    private static List<string> nouns = [
    "Tiger", "Falcon", "Wolf", "Phoenix", "Shark", "Panda", "Raptor", "Cobra", 
    "Titan", "Knight", "Ranger", "Glitch", "Wizard", "Viper", "Raven", "Cyborg", 
    "Gamer", "Nomad", "Hunter", "Scout"
    ];

    private static int passwordLen = 10;
    private static string passwordChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    private static List<string> spacers = ["", "_", "-"];

    private readonly IConfiguration _config;

    public UserGenerator(IConfiguration config)
    {
        _config = config;
    }
    public async Task<List<User>> Generate(int numberOfUsers)
    {
        string userPath = _config["Seeder:CredentialsFile"] ?? throw new InvalidOperationException("User credentials save path not provided");
        await using var writer = new StreamWriter(userPath);

        await writer.WriteLineAsync("username,password");

        List<User> users = [];
        var adjectivesLen = adjectives.Count;
        var nounsLen = nouns.Count;
        var spacersLen = spacers.Count;

        for (int i = 0; i < numberOfUsers; i++)
        {
            var chosenAdjective = adjectives[Random.Shared.Next(0, adjectivesLen)];
            var chosenNoun = nouns[Random.Shared.Next(0, nounsLen)];
            var chosenSpacer = spacers[Random.Shared.Next(0, spacersLen)];
            var randomNumber = Random.Shared.Next(0, 100000);

            var username = $"{chosenAdjective}{chosenSpacer}{chosenNoun}{randomNumber}";

            var randomPassword = RandomNumberGenerator.GetString(passwordChars, passwordLen);

            var passwordHash = PasswordHasher.HashPassword(randomPassword);

            var createdUser = new User{Username = username, PasswordHash = passwordHash};

            users.Add(createdUser);

            await writer.WriteLineAsync($"{username},{randomPassword}");
        }         

        return users;
    }
}
