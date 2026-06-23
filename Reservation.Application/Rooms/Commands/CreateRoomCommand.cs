using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Reservation.Application.Rooms.Commands.Base;
using ErrorOr;
namespace Reservation.Application.Rooms.Commands
{
    public record CreateRoomCommand(Guid IdempotencyKey) : RoomCommand(IdempotencyKey), IRequest<ErrorOr<int>>
    {
        public string? RoomtypeName { get; set; }

        public string? RoomtypeDescription { get; set; }

        public int FloorNumber { get; set; }

        public int RoomNumber { get; set; }
    }
}
