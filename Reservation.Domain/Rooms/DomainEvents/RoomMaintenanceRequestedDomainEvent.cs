using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reservation.Domain.Rooms.DomainEvents
{
    public record RoomMaintenanceRequestedDomainEvent(int? CardId, string Reason, DateTime RequestedAt) : INotification;
   
    
}
