using ErrorOr;
using History.Domain.Actions;
using History.Persistance;
using Kernel.Domain.Primitives.ActionTracker;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VipManagement.Domain.Cards.DomainEvents;
using VipManagement.Domain.Cards.Entities;
using VipManagement.Persistence;

namespace VipManagement.Application.Cards.Commands
{
    public class CreateCardCommandHandler : IRequestHandler<CreateCardCommand, ErrorOr<int>>
    {
        private readonly VipManagementDbContext _context;
        private readonly HistoryDbContext _historyContext;
        private readonly IMediator _mediator;

        public CreateCardCommandHandler(
            VipManagementDbContext context,
            HistoryDbContext historyContext,
            IMediator mediator)
        {
            _context = context;
            _historyContext = historyContext;
            _mediator = mediator;
        }

        public async Task<ErrorOr<int>> Handle(CreateCardCommand request, CancellationToken cancellationToken)
        {
            var errors = new List<Error>();

            if (!string.IsNullOrWhiteSpace(request.Number))
            {
                var cardNumberAlreadyExists = await _context.Cards
                    .AnyAsync(card => card.Number == request.Number, cancellationToken);

                if (cardNumberAlreadyExists)
                {
                    errors.Add(Error.Conflict(
                        code: "Card.NumberAlreadyExists",
                        description: "The card number already exists."));
                }
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

            var actions = card.GetActions();

            foreach (var action in actions)
            {
                var historyAction = new HistoryAction(
                    type: action.Type,
                    domain: action.Domain,
                    historyId: action is ParentActionTracker parent
                        ? parent.HistoryId
                        : null,
                    createdOn: action.CreatedOn
                );

                _historyContext.HistoryActions.Add(historyAction);
            }

            _context.Cards.Add(card);

            await _context.SaveChangesAsync(cancellationToken);
            await _historyContext.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(
                new CardCreatedDomainEvent(card.Id),
                cancellationToken
            );

            card.ClearActions();

            return card.Id.Value;
        }
    }
}
