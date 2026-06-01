using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using VipManagement.Domain.Cards.Entities;

namespace VipManagement.Domain.Cards.DomainEvents
{

    // Fem servir INotification perque MediaTr pugui publicar l'event
    public record CardCreatedDomainEvent(CardId CardId) : INotification;
}
