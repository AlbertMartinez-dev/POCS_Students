using ErrorOr;
using Kernel.Domain.Primitives;
using Kernel.Domain.Primitives.ActionTracker;

namespace VipManagement.Domain.Cards.Entities
{
    public class Card : Aggregate<CardId>
    {
        

        public string Number { get; set; }
        public string Name { get; set; }
        public DateTime ExpirationDate { get;  set; }

        public Guid? HistoryActionId { get; set ; }
        public const string HistoryTypeSelector = "VipManagement.Card";

        private Card()
        {
        }

        private Card(string number, string name, DateTime expirationDate)
        {
            Number = number;
            Name = name;
            ExpirationDate = expirationDate;
            HistoryActionId = Guid.NewGuid();


        }

        public static ErrorOr<Card> CreateCard(
            string number,
            string name,
            DateTime expirationDate
            )
        {
            var errors = new List<Error>();

            if (string.IsNullOrWhiteSpace(number))
            {
                errors.Add(Error.Validation(
                    code: "Card.NumberRequired",
                    description: "The card number is required."));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                errors.Add(Error.Validation(
                    code: "Card.NameRequired",
                    description: "The card name is required."));
            }

            if (expirationDate <= DateTime.UtcNow)
            {
                errors.Add(Error.Validation(
                    code: "Card.InvalidExpirationDate",
                    description: "The expiration date must be in the future."));
            }

            if (errors.Count > 0)
            {
                return errors;
            }

            var card = new Card(number,
                name,
                expirationDate
                );


            card.AddAction(new ParentActionTracker(
                    type: HistoryTypeSelector,
                    domain: CardActionTracker.CardCreated,
                    historyId: card.HistoryActionId
                ));


            return card;
        }
    }
}