using System;
using System.Collections.Generic;
using System.Text;
using ErrorOr;
using FluentAssertions;
using Reservation.Domain.Rooms.Entities;

namespace Reservation.Test.Rooms
{
    public class RoomTest
    {



        // Test creation with valid parameters
        [Fact]
        public void Create_WithValidData_ReturnsRoom()
        {

            //arrange
            var roomNumber = 80;

            var type = "STANDARD";

            var floor = 4;

            var description = "Beautiful";
            // act

            var result = Room.Create(type, description, floor, roomNumber);


            // assert

            result.IsError.Should().BeFalse();
            result.Value.RoomNumber.Should().Be(roomNumber);

        }


        // Test adding an amenity - Create a room, add amenity and verify it appears in amenities collection
        [Fact]
        public void AddAmenity_AfterCreatingRoom_AddsToCollection()
        {

            var room1 = Room.Create("STANDARD", "Beautiful", 4, 80).Value;


            var result = room1.AddAmenity("Nightstand");


            result.IsError.Should().BeFalse();
            room1.Amenities.Should().HaveCount(1);

        }


        // Test a business rule violation - attempt to create a rooom wtih an invalid floor number (0 or negative) and verify it returns an ErrorOr Error

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void CreateRoom_WithInvalidFloorNumber_ReturnsError(int floorNumber)
        {
            var result = Room.Create(
                "STANDARD",
                "Beautiful",
                floorNumber,
                80);

            result.IsError.Should().BeTrue();
            result.Errors.First().Code.Should().Be("FloorNumber.Invalid");
        }







    }
}
