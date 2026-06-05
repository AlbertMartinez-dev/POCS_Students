using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Kernel.Domain.Entities;
using FluentAssertions;

namespace Kernel.Domain.Tests.ValueObjects
{


    public class MoneyTests
    {
        [Fact]
        public void Create_WithValidData_ReturnsMoney()
        {
            // Arrange
            var amount = 100m;
            var currencyCode = "EUR";

            // Act
            var result = Money.Create(amount, currencyCode);

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Amount.Should().Be(amount);
            result.Value.CurrencyCode.Should().Be(currencyCode);
        }

        [Fact]
        public void Create_WithLowercaseCurrency_NormalizesCurrencyCode()
        {
            // Arrange
            var amount = 50m;
            var currencyCode = "eur";

            // Act
            var result = Money.Create(amount, currencyCode);

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.CurrencyCode.Should().Be("EUR");
        }

        [Fact]
        public void Create_WithCurrencyWithSpaces_NormalizesCurrencyCode()
        {
            // Arrange
            var amount = 50m;
            var currencyCode = " eur ";

            // Act
            var result = Money.Create(amount, currencyCode);

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.CurrencyCode.Should().Be("EUR");
        }

        [Fact]
        public void Create_WithNegativeAmount_ReturnsValidationError()
        {
            // Arrange
            var amount = -10m;
            var currencyCode = "EUR";

            // Act
            var result = Money.Create(amount, currencyCode);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.Should().Contain(error => error.Code == "Money.Validation");
        }

        [Fact]
        public void Create_WithCurrencyCodeShorterThanThreeLetters_ReturnsValidationError()
        {
            // Arrange
            var amount = 10m;
            var currencyCode = "EU";

            // Act
            var result = Money.Create(amount, currencyCode);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.Should().Contain(error => error.Code == "Money.Validation");
        }

        [Fact]
        public void Create_WithCurrencyCodeLongerThanThreeLetters_ReturnsValidationError()
        {
            // Arrange
            var amount = 10m;
            var currencyCode = "EURO";

            // Act
            var result = Money.Create(amount, currencyCode);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.Should().Contain(error => error.Code == "Money.Validation");
        }

        [Fact]
        public void Add_WithSameCurrency_ReturnsSum()
        {
            // Arrange
            var firstMoney = Money.Create(100m, "EUR").Value;
            var secondMoney = Money.Create(50m, "EUR").Value;

            // Act
            var result = firstMoney.Add(secondMoney);

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Amount.Should().Be(150m);
            result.Value.CurrencyCode.Should().Be("EUR");
        }

        [Fact]
        public void Add_WithDifferentCurrency_ReturnsConflictError()
        {
            // Arrange
            var firstMoney = Money.Create(100m, "EUR").Value;
            var secondMoney = Money.Create(50m, "USD").Value;

            // Act
            var result = firstMoney.Add(secondMoney);

            // Assert
            result.IsError.Should().BeTrue();
            result.Errors.Should().Contain(error => error.Code == "Money.Conflict");
        }

        [Fact]
        public void Money_WithSameValues_AreEqual()
        {
            // Arrange
            var firstMoney = Money.Create(100m, "EUR").Value;
            var secondMoney = Money.Create(100m, "EUR").Value;

            // Act & Assert
            firstMoney.Should().Be(secondMoney);
        }

        [Fact]
        public void Money_WithSameValuesButDifferentCurrencyCasing_AreEqual()
        {
            // Arrange
            var firstMoney = Money.Create(100m, "eur").Value;
            var secondMoney = Money.Create(100m, "EUR").Value;

            // Act & Assert
            firstMoney.Should().Be(secondMoney);
        }
    }


}
