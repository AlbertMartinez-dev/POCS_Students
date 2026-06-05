using System;
using System.Collections.Generic;
using System.Text;
using ErrorOr;
using Kernel.Domain.Primitives;


namespace Reservation.Domain.Rooms.Entities
{
    public class Room : Aggregate<RoomId>
    {
        private readonly List<RoomAmenity> _amenities = new();

        public RoomType RoomType { get; private set; }

        public FloorNumber FloorNumber { get; private set; }

        public IReadOnlyCollection<RoomAmenity> Amenities => _amenities.AsReadOnly();

        protected Room()
        {
        }

        internal Room(
            RoomId id,
            RoomType roomType,
            FloorNumber floorNumber)
            : base(id)
        {
            RoomType = roomType;
            FloorNumber = floorNumber;
        }

        

        public ErrorOr<Success> AddAmenity(RoomAmenityId amenityId, string? name)
        {
            var amenityResult = RoomAmenity.Create(amenityId, name);

            if (amenityResult.IsError)
            {
                return amenityResult.Errors;
            }

            var amenity = amenityResult.Value;

            var amenityWithSameIdExists = _amenities.Any(existingAmenity =>
                existingAmenity.Id == amenity.Id);

            if (amenityWithSameIdExists)
            {
                return Error.Conflict(
                    code: "RoomAmenity.IdAlreadyExists",
                    description: "An amenity with this id already exists in the room.");
            }

            var amenityWithSameNameExists = _amenities.Any(existingAmenity =>
                existingAmenity.Name.Equals(amenity.Name, StringComparison.OrdinalIgnoreCase));

            if (amenityWithSameNameExists)
            {
                return Error.Conflict(
                    code: "RoomAmenity.NameAlreadyExists",
                    description: "An amenity with this name already exists in the room.");
            }

            _amenities.Add(amenity);

            return Result.Success;
        }
    }
}

