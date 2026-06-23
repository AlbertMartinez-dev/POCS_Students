using ErrorOr;
using Kernel.Application.Abstractions.Data;
using MediatR;
using Reservation.Domain.Rooms.Entities;
using Reservation.Domain.Rooms.Interfaces;

namespace Reservation.Application.Rooms.Commands
{
    public class CreateRoomCommandHandler
        : IRequestHandler<CreateRoomCommand, ErrorOr<int>>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IUnitOfWorkService _unitOfWorkService;

        public CreateRoomCommandHandler(
            IRoomRepository roomRepository,
            IUnitOfWorkService unitOfWorkService)
        {
            _roomRepository = roomRepository;
            _unitOfWorkService = unitOfWorkService;
        }

        public async Task<ErrorOr<int>> Handle(
            CreateRoomCommand request,
            CancellationToken cancellation)
        {
            var roomResult = Room.Create(
                request.RoomtypeName,
                request.RoomtypeDescription,
                request.FloorNumber,
                request.RoomNumber
            );

            if (roomResult.IsError)
            {
                return roomResult.Errors;
            }

            var room = roomResult.Value;

            _roomRepository.Add(room);

            await _unitOfWorkService.SaveChangesAsync(cancellation);

            return room.Id.Value;
        }
    }
}