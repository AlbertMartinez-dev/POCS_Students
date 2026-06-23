
using ErrorOr;
using MediatR;
using Reservation.Application.Rooms.DTOs;

namespace Reservation.Application.Rooms.Queries
{
    public record GetRoomByIdQuery(int RoomId) : IRequest<ErrorOr<RoomDto>>;
}
