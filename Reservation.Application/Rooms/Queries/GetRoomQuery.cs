using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using ErrorOr;
using Reservation.Application.Rooms.DTOs;

namespace Reservation.Application.Rooms.Queries
{
    public record GetRoomQuery(int? FloorNumber, string? RoomType, bool ActiveOnly = true) : IRequest<ErrorOr<IReadOnlyList<RoomSearchResultDto>>>
    {
        

    }
}
