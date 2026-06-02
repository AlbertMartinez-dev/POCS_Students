using MediatR;
using VipManagement.Domain.Cards.DomainEvents;
using VipManagement.Domain.Cards.Entities;
using VipManagement.Persistence;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace VipManagement.Application.Cards.Commands
{
    public class CreateCardCommandHandler : IRequestHandler<CreateCardCommand, ErrorOr<int>>
    {
        private readonly VipManagementDbContext _context;
        private readonly IMediator _mediator;

        public CreateCardCommandHandler(
            VipManagementDbContext context,
            IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<ErrorOr<int>> Handle(CreateCardCommand request, CancellationToken cancellationToken)
        {

            var errors = new List<Error>();

            var cardNumberAlreadyExists = await _context.Cards
                .AnyAsync(card => card.Number == request.Number, cancellationToken);


            if (cardNumberAlreadyExists)
            {
                errors.Add(Error.Conflict(
                    code: "Card.NumberAlreadyExists",
                    description: "The card number already exists."));
            }


            var cardResult = Card.CreateCard(
                request.Number,
                request.Name,
                request.ExpirationDate
            );


            if (cardResult.IsError)
            {
                errors.AddRange(cardResult.Errors);
            }

            if (errors.Count > 0)
            {
                return errors;
            }


            var card = cardResult.Value;

            _context.Cards.Add(card);

            await _context.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(
                new CardCreatedDomainEvent(card.Id),
                cancellationToken
            );

            return card.Id.Value;
        }
    }
}