using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Reservation.Domain.Rooms;
using Reservation.Application.Rooms.Commands.Base;

namespace Reservation.Application.Rooms.Commands
{
    public class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
    {

        public CreateRoomCommandValidator()
        {
            RuleFor(x => x.RoomNumber)
            .NotEmpty()
                .WithMessage("Room number is required.")
            .LessThanOrEqualTo(9999)
                .WithMessage("Room number must not exceed number 9999");

            RuleFor(x => x.FloorNumber)
                .InclusiveBetween(1, 99)
                    .WithMessage("Floor number must be between 1 and 99.");

            RuleFor(x => x.RoomtypeName)
                .IsInEnum()
                    .WithMessage("Room type must be a valid value (Standard, Deluxe, or Suite).");


        }



    }
}
