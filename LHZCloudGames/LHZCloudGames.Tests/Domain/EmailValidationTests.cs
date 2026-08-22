using FluentAssertions;
using LHZCloudGames.Domain.Entities;
using Xunit;

namespace LHZCloudGames.Tests.Domain;

public class EmailValidationTests
{
    [Theory]
    [InlineData("", false)]
    [InlineData("invalidemail", false)]
    [InlineData("test@test", false)] // Because it doesn't have a dot suffix, Wait, the regex ^[^@\s]+@[^@\s]+\.[^@\s]+$ handles it.
    [InlineData("valid.email@example.com", true)]
    public void IsValidEmail_ShouldReturnExpectedResult(string email, bool expected)
    {
        // Act
        var result = User.IsValidEmail(email);

        // Assert
        result.Should().Be(expected);
    }
}
