using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;
using ErrorOr;

namespace Reservation.Domain.Rooms.Entities
{
    // Sealed ja que volem comportament Tancat : Només volem que la habitacio pugui ser Standard, Deluxe o Suite
    public sealed record RoomType
    {

        // Static ja que no cal fer new, pots utilitzar directament
        

        public string Value { get; }
        public string Description { get; }


        private RoomType(string value, string description)
        {
            Value = value;
            Description = description;
         
        }


        // ErrorOr per si no s'insereix la habitació adequada
        public static ErrorOr<RoomType> Create(string value, string description)
        {

          

            if (string.IsNullOrEmpty(value))
            {
                return Error.Validation(
                    code: "RoomType.Required",
                    description: "Room type is Required");
            }



            // obliga a ser majuscules i sense espais
            var NormalizedValue = value.ToUpper().Trim();

            if (NormalizedValue != "STANDARD" &&  
                NormalizedValue != "DELUXE" && 
                NormalizedValue != "SUITE")
            {
                return Error.Validation(
                    code: "RoomType.Invalid",
                    description: "Room type is not valid");

            }



            return new RoomType(NormalizedValue, description);

        }

    }
}
