using System;
using System.Collections.Generic;
using System.Text;

namespace Reservation.Application.Rooms.DTOs
{

    public class RoomDto
    {
        public int Id { get; set; }

        public int RoomNumber { get; set; }

        public int FloorNumber { get; set; }

        public string? RoomType { get; set; } 
    }

}
