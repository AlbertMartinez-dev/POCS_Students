using System;
using System.Collections.Generic;
using System.Text;

namespace Reservation.Application.Rooms.DTOs
{
    public record RoomSearchResultDto
    {
        Guid Id { get; set; }

        string RoomNumber { get; set; }
        
        int FloorNumber { get; set; }

        string RoomType { get; set; }

        bool IsActive { get; set; }

        
    }
}
