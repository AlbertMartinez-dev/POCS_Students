using Reservation.Domain.Rooms.Entities;
using Kernel.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Text;

public class GetActiveRoomsByFloorSpec : Specification2<Room, RoomId>
{
    public GetActiveRoomsByFloorSpec(int floorNumber, bool includeAmenities)
    {
        // Implement the specification
    }
}
