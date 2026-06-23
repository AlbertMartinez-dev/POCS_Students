using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;


namespace Reservation.Application.Rooms.DTOs
{
    public class CreateRoomInputDTO
    {
        public string? RoomtypeName { get; set; }

        public string? RoomtypeDescription { get; set; }

        public int FloorNumber { get; set; }

        public int RoomNumber {  get; set; }





    }
}
