using ErrorOr;
using Kernel.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;


namespace Reservation.Domain.Rooms.Entities
{
    public class RoomAmenity : Entity<RoomAmenityId>
    {
        protected RoomAmenity()
        {
            Name = string.Empty;
        }

        private RoomAmenity( string name)
            
        {
            Name = name;
        }

        public string Name { get; private set; }

        public static ErrorOr<RoomAmenity> Create(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Error.Validation(
                    code: "RoomAmenity.NameRequired",
                    description: "Amenity name is required.");
            }

            var normalizedName = name.Trim();

            return new RoomAmenity(normalizedName);
        }
    }
}
