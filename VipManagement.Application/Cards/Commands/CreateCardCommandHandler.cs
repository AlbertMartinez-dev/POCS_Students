using MediatR;
using VipManagement.Domain.Cards.DomainEvents;
using VipManagement.Domain.Cards.Entities;
using VipManagement.Persistence;

namespace VipManagement.Application.Cards.Commands
{
    public class CreateCardCommandHandler : IRequestHandler<CreateCardCommand, int>
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

        public async Task<int> Handle(CreateCardCommand request, CancellationToken cancellationToken)
        {
            var card = new Card(
                request.Number,
                request.Name,
                request.ExpirationDate
            );

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
