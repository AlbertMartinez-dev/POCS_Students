using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Reservation.Domain.Rooms.Entities;

namespace Reservation.Persistence.Room.EntityTypeConfiguration
{
    public class RoomEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Rooms.Entities.Room>
    {
        public void Configure(EntityTypeBuilder<Domain.Rooms.Entities.Room> builder)
        {

            builder.ToTable("Rooms", "Reservation");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).
                HasConversion(v => v.Value, v => new RoomId(v)).ValueGeneratedOnAdd();

            builder.Property(r => r.RoomNumber)
               .HasMaxLength(10)
               .IsRequired();

            builder.OwnsOne(r => r.RoomType, RoomType =>
            {
                RoomType.Property(rt => rt.Value)
                    .HasColumnName("RoomType_Category")
                    .HasMaxLength(50)
                    .IsRequired();

            });

            builder.OwnsOne(r => r.RoomType, RoomType =>
            {
                RoomType.Property(rt => rt.Description)
                    .HasColumnName("RoomType_Description")
                    .HasMaxLength(200)
                    .IsRequired();

            });



            builder.OwnsOne(r => r.FloorNumber, FloorNumber =>
            {
                FloorNumber.Property(rt => rt.Number)
                    .HasColumnName("Floor_Number")
                    .IsRequired();
            });

            builder.HasMany(r => r.Amenities)
                .WithOne()
                .HasForeignKey("RoomId")
                .OnDelete(DeleteBehavior.Cascade);




            // EN LA SEVA SOLUCIO HO TENEN A Persistence.Core.Extensions; ChangeTrackerExtensions
            builder.Property<bool>("IsActive")
                .HasDefaultValue(true)
                .IsRequired();

            builder.Property<bool>("IsDeleted")
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property<int>("CreatedById")
                .IsRequired();

            builder.Property<int>("ModifiedById")
                .IsRequired();

            builder.Property<DateTime>("CreatedOn")
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property<DateTime>("ModifiedOn")
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property<byte[]>("Timestamp")
                .IsRowVersion()
                .IsRequired();



            builder.Property(r => r.HistoryActionId)
                .IsRequired(false);





            builder.HasIndex(r => r.RoomNumber).IsUnique();
                

        }
    }
}
