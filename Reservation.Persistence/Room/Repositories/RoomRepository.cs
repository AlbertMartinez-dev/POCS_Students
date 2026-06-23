using Reservation.Domain.Rooms.Entities;
using Reservation.Domain.Rooms.Interfaces;
using Reservation.Persistence.Room.EntityTypeConfiguration;
using RoomEntity = Reservation.Domain.Rooms.Entities.Room;
using Microsoft.EntityFrameworkCore;

namespace Reservation.Persistence.Room.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ReservationDbContext _dbContext;

        public RoomRepository(ReservationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public RoomEntity Add(RoomEntity room)
        {
            _dbContext.Rooms.Add(room);
            return room;
        }





        public async Task<RoomEntity?> GetByIdAsync(
            RoomId id,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.Rooms
                .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }



    }
}
