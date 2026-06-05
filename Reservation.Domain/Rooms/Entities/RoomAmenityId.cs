using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Kernel.Domain.Primitives;

namespace Reservation.Domain.Rooms.Entities
{
    public record 
        RoomAmenityId (int Value) : IValue<int>
    {
        public static implicit operator int(RoomAmenityId self) => self.Value;


    }
}
