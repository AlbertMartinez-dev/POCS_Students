using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpEssentials_Albert.Entities
{



    public record class ReservationId
    {
        // Només lectura (immutable) per a altres del programa. el guid de reservationID
        public Guid Value { get; }


        // Per a poder utilitzar ur un reservaiton ID amb un Guid pre-exisent
        public ReservationId(Guid value)
        {
            Value = value;
        }
        // Crea un ReservationId nou amb un Guid nou
        public static ReservationId New()
        {
            return new ReservationId(Guid.NewGuid());
        }
    }

}
