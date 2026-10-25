using backend.Core.Auth;

namespace MusicAnalizer.Tests;

public class UnitTest1
{
    [Fact]
    public void CorrectPassword_ShouldReturnTrue()
    {
        //Arrange
        const string password = "Test123!";
        string hash = PasswordHasher.HashPassword(password);

        //Act
        bool result = PasswordHasher.VerifyPassword(password, hash);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public void WrongPassword_ShouldReturnFalse()
    {
        //Arrange
        const string password = "Test123!";
        string hash = PasswordHasher.HashPassword(password);

        //Act
        bool result = PasswordHasher.VerifyPassword("WrongPassword", hash);

        //Assert
        Assert.False(result);
    }

    [Fact]
    public void SamePassword_ShouldProduceDifferentHashes()
    {
        //Arrange
        const string password = "Test123!";
        string hashOne = PasswordHasher.HashPassword(password);
        string hashTwo = PasswordHasher.HashPassword(password);


        //Assert
        Assert.NotEqual(hashOne, hashTwo);
    }
}
