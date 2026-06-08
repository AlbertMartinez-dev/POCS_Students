using ErrorOr;

namespace Kernel.Domain.Entities
{
    public sealed record EmailAddress
    {
        public string Value { get; }

        private EmailAddress(string value)
        {
            Value = value;
        }

        public static ErrorOr<EmailAddress> Create(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return Error.Validation(
                    code: "EmailAddress.Validation",
                    description: "Email Address can't be null or empty"
                );
            }

            var normalizedEmail = value.Trim().ToLowerInvariant();

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
            }

            if (!foundAt)
            {
                return Error.Validation(
                    code: "EmailAddress.Validation",
                    description: "Email Address must contain @"
                );
            }

            if (!foundDotAfterAt)
            {
                return Error.Validation(
                    code: "EmailAddress.Validation",
                    description: "Email Address must contain a dot after @"
                );
            }

            return new EmailAddress(normalizedEmail);
        }

        public override string ToString()
        {
            return Value;
        }
    }
}