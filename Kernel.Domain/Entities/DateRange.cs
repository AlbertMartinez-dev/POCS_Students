using System;
using System.Collections.Generic;
using System.Text;
using ErrorOr;

namespace Kernel.Domain.Entities
{
    public record DateRange
    {
        public DateTime Start { get; }

        public DateTime End { get; }

        private DateRange (DateTime start, DateTime end)
        {
            Start = start;
            End = end;
          

        }
        // Quan algú demani Nights, retorna (End - Start).Days.”
        public  int Nights => (End - Start).Days;



        public static ErrorOr<DateRange> Create(DateTime start, DateTime end)
        {
            var normalizedStart = start.Date;

            var normalizedEnd = end.Date;


          


            if (normalizedEnd  < normalizedStart) 
                {
                    return Error.Conflict(
                        code: "DateRange.Conflict",
                        description: "End DateTime must be after Start");


                }

                return new DateRange(normalizedStart, normalizedEnd);

        }


        public bool Overlaps(DateRange other)
        {

            return Start < other.End && End < other.Start;
        }
        
        










    }
}
