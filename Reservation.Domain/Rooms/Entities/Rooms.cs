using ErrorOr;
using Kernel.Domain.Primitives;
using Kernel.Domain.Primitives.ActionTracker;
using Reservation.Domain.Rooms.DomainEvents;

namespace Reservation.Domain.Rooms.Entities
{
    public class Rooms : Aggregate<RoomId>
    {
        public const string HistoryTypeSelector = "Reservation.Room";

        private readonly List<RoomAmenity> _amenities = new();

        public RoomType RoomType { get; private set; }

            

        public FloorNumber FloorNumber { get; private set; }

        public int RoomNumber { get; private set; }
        
      

        public bool MaintenanceRequested { get; private set; }

        public string? MaintenanceReason { get; private set; }


        public Guid? HistoryActionId { get; private set; }

        public IReadOnlyCollection<RoomAmenity> Amenities => _amenities.AsReadOnly();

        protected Rooms()
        {
        }

        private Rooms(
            RoomId id,
            int roomNumber,
            RoomType roomType,
            FloorNumber floorNumber,
            Guid? historyActionId = null)
            : base(id)
        {
            RoomType = roomType;
            RoomNumber = roomNumber;
            FloorNumber = floorNumber;
            HistoryActionId = historyActionId ?? Guid.NewGuid();
            
        }


        public static ErrorOr<Rooms> Create(
            RoomId id,
            string? roomType,
            int? floorNumber,
            int roomNumber,
            Guid? historyActionId = null
        )
        {
            var errors = new List<Error>();

            var roomTypeResult = RoomType.Create(roomType);
            if (roomTypeResult.IsError)
            {
                errors.AddRange(roomTypeResult.Errors);
            }

            var floorNumberResult = FloorNumber.Create(floorNumber);
            if (floorNumberResult.IsError)
            {
                errors.AddRange(floorNumberResult.Errors);
            }

            if (errors.Count > 0)
            {
                return errors;
            }

            var room = new Rooms(
                id,
                roomNumber,
                roomTypeResult.Value,
                floorNumberResult.Value,
                historyActionId
            );

            room.AddAction(new ParentActionTracker(
                HistoryTypeSelector,
                RoomActionTracker.RoomCreated,
                historyId: room.HistoryActionId,
                room));

            return room;
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




            AddAction(new ChildActionTracker(
                HistoryTypeSelector,
                RoomActionTracker.RoomAmenityAdded,
                parentHistoryId: HistoryActionId,
                entity: this));





            return Result.Success;
        }

        public ErrorOr<Success> RequestMaintenance(string? reason)
        {
            if (string.IsNullOrWhiteSpace(reason))
            {
                return Error.Validation(
                    code: "RequestMaintenance.Validation",
                    description: "Maintenance reason can't be blank");
            }

            if (MaintenanceRequested)
            {
                return Error.Conflict(
                    code: "Room.MaintenanceAlreadyRequested",
                    description: "Maintenance has already been requested for this room.");
            }

            var normalizedReason = reason.Trim();

            MaintenanceRequested = true;
            MaintenanceReason = normalizedReason;

            PushEvent(new RoomMaintenanceRequestedDomainEvent(
                Id,
                normalizedReason,
                DateTime.UtcNow
            ));


            AddAction(new ChildActionTracker(
                HistoryTypeSelector,
                RoomActionTracker.RoomMaintenanceRequested,
                parentHistoryId: HistoryActionId,
                entity: this));



            return Result.Success;
        }
    }
}