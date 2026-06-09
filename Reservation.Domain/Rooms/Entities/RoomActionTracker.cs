using System;
using System.Collections.Generic;
using System.Text;

namespace Reservation.Domain.Rooms.Entities
{
    public static class RoomActionTracker
    {
        public const string RoomCreated = "RoomCreated";
        public const string RoomUpdated = "RoomUpdated";
        public const string RoomAmenityAdded = "RoomAmenityAdded";
        public const string RoomAmenityRemoved = "RoomAmenityRemoved";
        public const string RoomMaintenanceRequested = "RoomMaintenanceRequested";
    }
}
