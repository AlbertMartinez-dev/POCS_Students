using System;
using System.Collections.Generic;
using System.Text;

namespace Kernel.Domain.Primitives.ActionTracker
{
    public interface IActionTracker
    {
        public string Type { get; } // HistoryTypeSelector (identifica el tipus d'agregat)

        public string Domain { get; } // el nom de l'acció

        DateTime CreatedOn { get; } // Quan va ser creada
        

    }
}
