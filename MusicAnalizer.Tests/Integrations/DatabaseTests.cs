using System;
using backend.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MusicAnalizer.Tests.Integrations;

public class DatabaseTests : IClassFixture<MusicAnalizerFactory>
{
    private readonly MusicAnalizerFactory _factory;

    public DatabaseTests(MusicAnalizerFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task TestDatabase_ShouldBeAccessible()
    {
        //Arrange
        using var client = _factory.CreateClient();

        using var scope = _factory.Services.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        //Act
        var databaseName = db.Database.GetDbConnection().Database;

        //Assert
        Assert.Equal("musicdb_test", databaseName);

        bool connected = await db.Database.CanConnectAsync();

        Assert.True(connected);
    }
}
