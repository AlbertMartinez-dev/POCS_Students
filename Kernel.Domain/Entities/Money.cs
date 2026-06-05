using System;
using System.Collections.Generic;
using System.Text;
using ErrorOr;

namespace Kernel.Domain.Entities
{
    public record Money
    {
        public decimal Amount { get; }


        public string CurrencyCode { get; }

        private Money (decimal amount, string currencyCode)
        {
            Amount = amount;
            CurrencyCode = currencyCode;
        }



        public static ErrorOr<Money> Create (decimal amount, string currencyCode)
        {
            if (amount <0 )
            {
                return Error.Validation(
                    code: "Money.Validation",
                    description: "Amount can't be negative");


            }

            var normalizedCode = currencyCode.ToUpper().Trim();

            if (normalizedCode.Length != 3)
            {
                return Error.Validation(
                    code: "Money.Validation",
                    description: "Currency Code must be 3 letters");
            }

            return new Money(amount, normalizedCode);




        }


        public  ErrorOr<Money> Add ( Money other)


        {
            if ( CurrencyCode != other.CurrencyCode)
            {
                return Error.Conflict(
                    code: "Money.Conflict",
                    description: "Currency code must be the same");
            }

            return new Money(Amount + other.Amount, CurrencyCode);


        }








    }
}
