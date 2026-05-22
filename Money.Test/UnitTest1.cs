using CSharpEssentials_Albert.CsharpExercises.Exercise1;
namespace Money.Test
{
    public class MoneyTests
    {
        [Fact]
        public void Create_WithValidValues_ShouldReturnMoney()
        {
            // Arrange & Act
            var result = CSharpEssentials_Albert.CsharpExercises.Exercise1.Money.Create(100m, "EUR");

            // Assert -- what should you verify?


        }

        [Fact]
        public void Create_WithNegativeAmount_ShouldReturnError()
        {
            // Write this test
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("EU")]
        [InlineData("EURO")]
        public void Create_WithInvalidCurrencyCode_ShouldReturnError(string? code)
        {
            // Write this test
        }

        [Fact]
        public void TwoMoneyInstances_WithSameValues_ShouldBeEqual()
        {
            // Write this test -- records provide structural equality
        }
    }
}
