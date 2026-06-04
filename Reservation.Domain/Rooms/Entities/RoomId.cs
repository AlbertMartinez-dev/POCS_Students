using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Kernel.Domain.Primitives;

namespace Reservation.Domain.Rooms.Entities
{
    public record RoomId (int Value) : IValue<int>
    {

        public static implicit operator int(RoomId self) => self.Value;
    }
}
