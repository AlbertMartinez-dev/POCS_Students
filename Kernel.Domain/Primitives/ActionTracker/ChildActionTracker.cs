using System;
using System.Collections.Generic;
using System.Text;

namespace Kernel.Domain.Primitives.ActionTracker
{

    public class ChildActionTracker : IActionTracker
    {
        public string Type { get; }

        public string Domain { get; }

        public DateTime CreatedOn { get; }

        public Guid? HistoryId { get; }

    }

}