using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Reservation.Persistence.Room.EntityTypeConfiguration;
using Reservation.Application.Rooms.DTOs;
using RoomEntity = Reservation.Domain.Rooms.Entities.Room;
namespace Reservation.Persistence.Mapping
{
    public class RoomProfile :Profile
    {

        public RoomProfile()
        {

            CreateMap<RoomEntity, RoomDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
                .ForMember(dest => dest.RoomNumber, opt => opt.MapFrom(src => src.RoomNumber))
                .ForMember(dest => dest.FloorNumber, opt => opt.MapFrom(src => src.FloorNumber.Number))
                .ForMember(dest => dest.RoomType, opt => opt.MapFrom(src => src.RoomType.Value));







        }






    }
}
