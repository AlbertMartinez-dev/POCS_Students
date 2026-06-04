using System;
using System.Collections.Generic;
using System.Text;
using ErrorOr;

namespace Reservation.Domain.Rooms.Entities
{
    public sealed record FloorNumber
    {
        public int Number { get; }

        private FloorNumber(int number)
        {
            Number = number;
        }

        public static ErrorOr<FloorNumber> Create(int number)
        {


            if (number > 50 || number < 1)
            {
                return Error.Validation(
                    code: "FloorNumber.Invalid",
                    description: "Floor Number must be between 1 and 50");

            }


            


        }



    }
}
