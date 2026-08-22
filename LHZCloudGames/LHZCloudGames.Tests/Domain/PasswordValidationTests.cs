using FluentAssertions;
using LHZCloudGames.Domain.Entities;
using Xunit;

namespace LHZCloudGames.Tests.Domain;

public class PasswordValidationTests
{
    [Theory]
    [InlineData("", false)]
    [InlineData("short1!", false)]
    [InlineData("nouppercaseorlowercase1!", true)]
    [InlineData("NoNumbersHereAtAll!", false)]
    [InlineData("1234567890!", false)]
    [InlineData("NoSpecialChar123456", false)]
    [InlineData("ValidPassw0rd!", true)]
    [InlineData("@n0therV@l1d", true)]
    public void IsValidPassword_ShouldReturnExpectedResult(string password, bool expected)
    {
        // Act
        var result = User.IsValidPassword(password);

        // Assert
        result.Should().Be(expected);
    }
}
