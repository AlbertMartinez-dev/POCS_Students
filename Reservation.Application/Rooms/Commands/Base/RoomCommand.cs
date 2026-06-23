using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using ErrorOr;
namespace Reservation.Application.Rooms.Commands.Base
{
    public abstract record RoomCommand (Guid Idempotencykey) : IRequest<ErrorOr<int>>
    {




    }
}
