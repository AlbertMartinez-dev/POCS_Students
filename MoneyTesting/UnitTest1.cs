using CSharpEssentials_Albert.Entities;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;


namespace CSharpEssentials_Albert.CsharpExercises.Exercise1
{

    public class MoneyTests
    {
        [Fact]
        public void Create_WithValidValues_ShouldReturnMoney()
        {
            // Arrange
            var amount = 100m;
            var currency = "EUR";

            // Act
            var result = Money.Create(amount, currency);

            // Assert
            result.Amount.Should().Be(amount);
            result.CurrencyCode.Should().Be(currency);
        }

        [Fact]
        public void Create_WithNegativeAmount_ShouldThrowException()
        {
            // Arrange
            var amount = -100m;
            var currency = "EUR";

            // Act
            Action act = () => Money.Create(amount, currency);

            // Assert
            act.Should().Throw<InvalidOperationException>()
               .WithMessage("Amount can't be negative");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("EU")]
        [InlineData("EURO")]
        public void Create_WithInvalidCurrencyCode_ShouldThrowException(string? code)
        {
            // Arrange
            var amount = 100m;

            // Act
            Action act = () => Money.Create(amount, code!);

            // Assert
            act.Should().Throw<InvalidOperationException>()
               .WithMessage("CurrencyCode must be exactly 3 characters");
        }

        [Fact]
        public void TwoMoneyInstances_WithSameValues_ShouldBeEqual()
        {
            // Arrange
            var amount = 100m;
            var currency = "EUR";

            // Act
            var money1 = Money.Create(amount, currency);
            var money2 = Money.Create(amount, currency);

            // Assert
            money1.Should().Be(money2);
        }
    }

}
