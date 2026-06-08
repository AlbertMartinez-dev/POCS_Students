using FluentAssertions;
using Kernel.Domain.Entities;

namespace Kernel.Domain.Tests.ValueObjects
{
    public class EmailAddressTests
    {
        [Fact]
        public void Create_WithValidEmail_ReturnsEmailAddress()
        {
            // Arrange
            var email = "test@example.com";

            // Act
            var result = EmailAddress.Create(email);

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Value.Should().Be("test@example.com");
        }

        [Fact]
        public void Create_WithUppercaseEmail_NormalizesToLowercase()
        {
            // Arrange
            var email = "TEST@EXAMPLE.COM";

            // Act
            var result = EmailAddress.Create(email);

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Value.Should().Be("test@example.com");
        }

        [Fact]
        public void Create_WithSpacesAroundEmail_TrimsValue()
        {
            // Arrange
            var email = "  test@example.com  ";

            // Act
            var result = EmailAddress.Create(email);

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Value.Should().Be("test@example.com");
        }

        [Fact]
        public void Create_WithEmptyEmail_ReturnsValidationError()
        {
            // Arrange
            var email = "";

            // Act
            var result = EmailAddress.Create(email);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.Should().Contain(error => error.Code == "EmailAddress.Validation");
        }

        [Fact]
        public void Create_WithEmailWithoutAt_ReturnsValidationError()
        {
            // Arrange
            var email = "testexample.com";

            // Act
            var result = EmailAddress.Create(email);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.Should().Contain(error => error.Code == "EmailAddress.Validation");
        }

        [Fact]
        public void Create_WithEmailWithoutDotAfterAt_ReturnsValidationError()
        {
            // Arrange
            var email = "test@example";

            // Act
            var result = EmailAddress.Create(email);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.Should().Contain(error => error.Code == "EmailAddress.Validation");
        }

        [Fact]
        public void ToString_ShouldReturnEmailValue()
        {
            // Arrange
            var email = EmailAddress.Create("TEST@EXAMPLE.COM").Value;

            // Act
            var text = email.ToString();

            // Assert
            text.Should().Be("test@example.com");
        }

        [Fact]
        public void EmailAddress_WithSameNormalizedValue_AreEqual()
        {
            // Arrange
            var firstEmail = EmailAddress.Create("TEST@EXAMPLE.COM").Value;
            var secondEmail = EmailAddress.Create("test@example.com").Value;

            // Act & Assert
            firstEmail.Should().Be(secondEmail);
        }
    }
}