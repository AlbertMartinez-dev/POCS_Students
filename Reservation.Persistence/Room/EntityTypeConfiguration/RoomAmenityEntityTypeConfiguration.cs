
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reservation.Domain.Rooms.Entities;

namespace Reservation.Persistence.Room.EntityTypeConfiguration
{
    public class RoomAmenityEntityTypeConfiguration : IEntityTypeConfiguration<RoomAmenity>
    {
        public void Configure(EntityTypeBuilder<RoomAmenity> builder)
        {
            builder.ToTable("RoomAmenities", "Reservation");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(v => v.Value, v => new RoomAmenityId(v))
                .ValueGeneratedOnAdd();

            builder.Property<RoomId>("RoomId")
                .HasConversion(v => v.Value, v => new RoomId(v))
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex("RoomId");

            builder.HasIndex("RoomId", "Name")
                .IsUnique();
        }
    }
}

