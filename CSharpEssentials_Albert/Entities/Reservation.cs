using System;

namespace CSharpEssentials_Albert.Entities
{
    public class Reservation : Entity<ReservationId>
    {
        public string? GuestName { get; private set; }
        public int RoomNumber { get; private set; }

        // Amb base(id), TId passa a ser ReservationId
        private Reservation(ReservationId id, string? guestName, int roomNumber) : base(id)
        {
            GuestName = guestName;
            RoomNumber = roomNumber;
        }

        public static Reservation Create(string? guestName, int roomNumber)
        {
            return new Reservation(
                new ReservationId(Guid.NewGuid()),
                guestName,
                roomNumber
            );
        }

        public static Reservation CreateWithId(ReservationId id, string? guestName, int roomNumber)
        {
            return new Reservation(id, guestName, roomNumber);
        }
    }
}