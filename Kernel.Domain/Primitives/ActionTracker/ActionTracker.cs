using System;
using System.Collections.Generic;
using System.Text;

namespace Kernel.Domain.Primitives.ActionTracker
{
    public class ActionTracker : IActionTracker
    {
        public string? Type { get; } // HistoryTypeSelector (identifica el tipus d'agregat)


        public string? Domain { get; } // el nom de l'acció


        public DateTime CreatedOn { get; }


        public ActionTracker(string type, string domainName)
        {
            Type = type;
           
            Domain = domainName;
        
         
            CreatedOn = DateTime.UtcNow;
        }
    }
}
