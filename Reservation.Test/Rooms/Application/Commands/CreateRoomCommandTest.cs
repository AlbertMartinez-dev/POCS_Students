using FluentAssertions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Reservation.Domain.Rooms.Entities;
using Reservation.Application.Rooms.Commands;
using Reservation.Application.Rooms;
using Kernel.Application.Abstractions.Data;
using Moq;
using Reservation.Domain.Rooms.Interfaces;
namespace Reservation.Test.Rooms.Application.Commands
{
    public class CreateRoomCommandTest
    {


        private readonly Mock<IUnitOfWorkService> _mockUnitofWork;
        private readonly Mock<IRoomRepository> _mockRooMRepository;
        private readonly CreateRoomCommandHandler _handler;




        //      1111  Happy path: Send a valid CreateRoomCommand and verify:
        //The result is not an error
        //AddAsync was called once on the repository
        //SaveChangesAsync was called once on the unit of work















        // 2222       Error path: Send a command that triggers a
        // domain error(e.g., invalid room type) and verify:
        //The result is an error
        //AddAsync was never called
        //SaveChangesAsync was never called



    }
}
