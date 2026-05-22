namespace CSharpEssentials_Albert.CsharpExercises.Exercise1
{
    public record Money
    {
        public decimal Amount { get; }
        public string CurrencyCode { get; }

        private Money(decimal amount, string currencyCode)
        {
            Amount = amount;
            CurrencyCode = currencyCode;
        }

        public static Money Create(decimal amount, string? currencyCode)
        {
            if (amount < 0)
            {
                throw new InvalidOperationException("Amount can't be negative");
            }

            if (string.IsNullOrEmpty(currencyCode) || currencyCode.Length != 3)
            {
                throw new InvalidOperationException("CurrencyCode must be exactly 3 characters");
            }

            return new Money(amount, currencyCode);
        }
    }
}