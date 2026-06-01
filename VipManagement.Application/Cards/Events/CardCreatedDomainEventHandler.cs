using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using VipManagement.Domain.Cards.DomainEvents;


namespace VipManagement.Application.Cards.Events
{
    public class CardCreatedDomainEventHandler : INotificationHandler<CardCreatedDomainEvent>
{
    private readonly ILogger<CardCreatedDomainEventHandler> _logger;

    public CardCreatedDomainEventHandler(ILogger<CardCreatedDomainEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(CardCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "CardCreatedDomainEvent executat. CardId: {CardId}",
            notification.CardId.Value
        );

        return Task.CompletedTask;
    }
}
}

