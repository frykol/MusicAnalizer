using System;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using backend.Dtos.User;

namespace MusicAnalizer.Tests.Integrations;

public class UserLoginTests : IClassFixture<MusicAnalizerFactory>
{
    private readonly MusicAnalizerFactory _factory;

    public UserLoginTests(MusicAnalizerFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task User_CanRegisterAndLogin()
    {
        //Arrange
        using var client = _factory.CreateClient();

        string username = $"UserTest_{Guid.NewGuid():N}";
        string password = "Test";

        //Act
        var register = await client.PostAsJsonAsync("/api/auth/register", new UserCreateDto(username, password, ""));

        Assert.Equal(HttpStatusCode.OK, register.StatusCode);

        var login = await client.PostAsJsonAsync("/api/auth/login", new UserLoginDto(username, password));

        Assert.Equal(HttpStatusCode.OK, login.StatusCode);

        var auth = await login.Content.ReadFromJsonAsync<UserLoginResponseDto>();

        Assert.NotNull(auth);

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth.JwtToken);

        var getMe = await client.GetAsync("/api/auth/me");

        Assert.Equal(HttpStatusCode.OK, getMe.StatusCode);
        
        var me = await getMe.Content.ReadFromJsonAsync<UserDetailedResponseDto>();
        //Assert
        Assert.NotNull(me);
        Assert.Equal(username, me.Username);
    }
}
