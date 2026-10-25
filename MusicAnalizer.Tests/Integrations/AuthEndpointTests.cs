using System;
using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace MusicAnalizer.Tests.Integrations;

public class AuthEndpointTests
{
    [Fact]
    public async Task GetMe_WithoutToken_ReturnsUnauthorized()
    {
        //Arrange
        await using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        //Act
        var response = await client.GetAsync("/api/auth/me");

        //Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
