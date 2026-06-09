using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Reservation.Domain.Rooms.Entities;
using Reservation.Domain.Rooms.DomainEvents;
using Xunit;
using Kernel.Domain.Primitives.ActionTracker;

namespace Kernel.Domain.Tests.Entities.RoomTest
{


    public class RoomTests
    {
        [Fact]

        public void Create_WithValidData_AddsParentActionTrackerToActionsCollection()
        {
            var roomResult = Room.Create(new RoomId(1), "STANDARD", 3);

            // per assegurar que el test no continua amb una room invalida
            roomResult.IsError.Should().BeFalse();

            var room = roomResult.Value;

            var actions = room.GetActions();

            actions.Should().ContainSingle();

            var parentAction = actions.OfType<ParentActionTracker>().Single();

            parentAction.Type.Should().Be(Room.HistoryTypeSelector);
            parentAction.Domain.Should().Be(RoomActionTracker.RoomCreated);
            parentAction.HistoryId.Should().NotBeNull();
            parentAction.Entity.Should().Be(room);

        }

        [Fact]

        public void AddAmenity_WithValidData_AddsChildActionTrackerWithCorrectParentHistoryId()

        {

            var roomResult = Room.Create(new RoomId(1), "STANDARD", 3);

            // per assegurar que el test no continua amb una room invalida
            roomResult.IsError.Should().BeFalse();

            var room = roomResult.Value;

            var amenityId = new RoomAmenityId(1);
            var amenityName = "Minibar";

            // act

            var result = room.AddAmenity(amenityId, amenityName);


            result.IsError.Should().BeFalse();

            var actions = room.GetActions();

            var childAction = actions.OfType<ChildActionTracker>().Single();

            childAction.Type.Should().Be(Room.HistoryTypeSelector);
            childAction.Domain.Should().Be(RoomActionTracker.RoomAmenityAdded);
            childAction.ParentHistoryId.Should().Be((Guid)room.HistoryActionId);

        }

        [Fact]
        public void GetActions_WithMultipleOperations_ReturnsCorrectCount()
        {
            var roomResult = Room.Create(new RoomId(1), "STANDARD", 3);

            // per assegurar que el test no continua amb una room invalida
            roomResult.IsError.Should().BeFalse();

            var room = roomResult.Value;

            var amenityId = new RoomAmenityId(1);
            var amenityName = "Minibar";

            // act

            var result = room.AddAmenity(amenityId, amenityName);


            result.IsError.Should().BeFalse();

            var actions = room.GetActions();

            actions.Count.Should().Be(2);


            actions.OfType<ParentActionTracker>().Should().ContainSingle();
            actions.OfType<ChildActionTracker>().Should().ContainSingle();
        }

        [Fact]
        public void HasCurrentHistoryVersion_WithValidData_ReturnsTrue()
        {

            // Arrange / Act
            var roomResult = Room.Create(new RoomId(1), "STANDARD", 3);

            // Assert
            roomResult.IsError.Should().BeFalse();

            var room = roomResult.Value;

            room.HasCurrentHistoryVersion().Should().BeTrue();




        }











        [Fact]
        
        public void RequestMaintenance_WithValidReason_ShouldRaiseEvent()
        {


            // arrange --> preparar dades
            var roomResult = Room.Create(new RoomId(1), "STANDARD", 3);

            // per assegurar que el test no continua amb una room invalida
            roomResult.IsError.Should().BeFalse();

            var room = roomResult.Value;

            var reason = "Cleaning";

            // act --> executar el que volem provar

            var result = room.RequestMaintenance(reason);


            // assert --> comprovar el resultat
            result.IsError.Should().BeFalse();

            room.MaintenanceRequested.Should().BeTrue();
            room.MaintenanceReason.Should().Be(reason);

            var domainEvents = room.GetDomainEvents();

            domainEvents.Should().ContainSingle();

            var domainEvent = domainEvents.OfType<RoomMaintenanceRequestedDomainEvent>().Single();

            domainEvent.Reason.Should().Be(reason);
            

        }
        [Fact]
        public void RequestMaintenance_WhenAlreadyRequested_ShouldReturnConflictError()
        {

            // arrange 
            var roomResult = Room.Create(new RoomId(1), "STANDARD", 3);

            // per assegurar que el test no continua amb una room invalida
            roomResult.IsError.Should().BeFalse();

            var room = roomResult.Value;

            var reason = "Cleaning";

            var reason2 = "Please Clean already!";


            var result = room.RequestMaintenance(reason);

            var result2 = room.RequestMaintenance(reason2);


            result2.IsError.Should().BeTrue();
            room.MaintenanceReason.Should().Be(reason);

        }
        [Fact]
        public void RequestMaintenance_WithEmptyReason_ShouldReturnValidationError()
        {
            // arrange 
            var roomResult = Room.Create(new RoomId(1), "STANDARD", 3);

            // per assegurar que el test no continua amb una room invalida
            roomResult.IsError.Should().BeFalse();

            var room = roomResult.Value;

            var reason = "";

            var result = room.RequestMaintenance(reason);



            result.IsError.Should().BeTrue();


        }





    }     
}
