using Microsoft.EntityFrameworkCore;
using Reservation.Persistence.Room.EntityTypeConfiguration;
using System;
using System.Collections.Generic;
using System.Text;
using RoomEntity = Reservation.Domain.Rooms.Entities.Room;

namespace Reservation.Persistence
{
    public class ReservationDbContext : DbContext 
    {

        public ReservationDbContext(DbContextOptions<ReservationDbContext> options)
            : base(options)
        {
        }


        public DbSet<RoomEntity> Rooms { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ✅ AIXÒ ÉS EL QUE BUSQUES
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReservationDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
