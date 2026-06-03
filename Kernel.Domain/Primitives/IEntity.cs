using Kernel.Domain.Primitives.ActionTracker;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kernel.Domain.Primitives
{
    public interface IEntity
    {

      
        ICollection<IActionTracker> GetActions();

        IActionTracker GetCurrentHistoryVersion();

        bool HasCurrentHistoryVersion();

        bool HasOnlyHistoryDetails();

        List<IActionTracker> GetChildActions();

        void ClearActions();

        void ClearHistoryVersion();

        object GetId();
    }

}

