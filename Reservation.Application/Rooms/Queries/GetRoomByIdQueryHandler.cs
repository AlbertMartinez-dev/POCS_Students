using Reservation.Domain.Rooms.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using AutoMapper;
using Reservation.Application.Rooms.DTOs;
using ErrorOr;
using System.Net.Http.Headers;
using Reservation.Domain.Rooms.Entities;

namespace Reservation.Application.Rooms.Queries
{
    public class GetRoomByIdQueryHandler : IRequestHandler<GetRoomByIdQuery,ErrorOr<RoomDto>>
    {

        IRoomRepository _roomRepository;
        IMapper _mapper; 

        public GetRoomByIdQueryHandler( IRoomRepository roomRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;

        }

        public async Task<ErrorOr<RoomDto>> Handle ( GetRoomByIdQuery query, CancellationToken cancellation)
        {
            var roomId = new RoomId(query.RoomId);
            var room = await _roomRepository.GetByIdAsync(roomId, cancellation);

            if (room is null)
            {
                return Error.NotFound(
                    code: "Room.NotFound",
                    description: "Room wasn't found");


            }

            var roomDto = _mapper.Map<RoomDto>(room);

            return roomDto;



        }



    }
}
