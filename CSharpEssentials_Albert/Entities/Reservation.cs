using System;

namespace CSharpEssentials_Albert.Entities
{
    public class Reservation : Entity<ReservationId>
    {
        public string? GuestName { get; private set; }
        public int RoomNumber { get; private set; }


        // Crees objecte reserva
        private Reservation(ReservationId id, string? guestName, int roomNumber) : base(id)
        {
            GuestName = guestName;
            RoomNumber = roomNumber;
        }

        // tu construeixes tot manualment
        public static Reservation Create(string? guestName, int roomNumber)
        {
            return new Reservation(
                ReservationId.New(),
                guestName,
                roomNumber
            );
        }

        // reconstruir reserva existent automatica
        public static Reservation CreateWithId(ReservationId id, string? guestName, int roomNumber)
        {
            return new Reservation(id, guestName, roomNumber);
        }
    }
}