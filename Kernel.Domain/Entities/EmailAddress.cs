using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using ErrorOr;

namespace Kernel.Domain.Entities
{
    public record EmailAddress
    {
        public String Value { get; }


        private EmailAddress(String value)
        {
            Value = value;
        }



        public ErrorOr<EmailAddress> Create(string  value)
        {

            if (string.IsNullOrEmpty(value))
            {
                Error.Validation(
                   code: "EmailAddess.Validation",
                   description: "Email Address can't be null"
                   );

            }
           


            var normalizedEmail = value.ToLower().Trim();

            bool foundAt = false;
            bool foundDotAfterAt = false;

            foreach (char character in normalizedEmail)
            {
                if (character == '@')
                {
                    foundAt = true;
                    continue;
                }
                if (foundAt && character == '.')
                {
                    foundDotAfterAt = true;
                }


                if (!foundAt)
                {
                    return Error.Validation(
                    code: "EmailAddress.Validation",
                    description: "Email Adddress must contain @"
                    );
                }

                if (!foundDotAfterAt)
                {
                    return Error.Validation(
                        code: "EmailAddress.Validation",
                        description: "Email Address must contain a dot after @");
                }

            }


            return new EmailAddress(normalizedEmail);
        }

        public override string ToString()
        {
            return Value;
        }


    }
}
