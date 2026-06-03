using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace Kernel.Domain.Primitives.ActionTracker
{
    public class ParentActionTracker : IActionTracker
    {
        public string Type { get; }

        public string Domain { get; }

        public DateTime CreatedOn { get; }

        public Guid? HistoryId { get;  }

        public object Entity { get; }


        public ParentActionTracker(
                    string type,
                    string domain,
                    Guid? historyId
                    )
        {
            Type = type;
            Domain = domain;
            HistoryId = historyId;
           
            CreatedOn = DateTime.UtcNow;
        }





    }


}
