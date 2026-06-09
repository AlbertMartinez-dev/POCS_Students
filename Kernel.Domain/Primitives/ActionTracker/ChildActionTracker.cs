using System;
using System.Collections.Generic;
using System.Text;

namespace Kernel.Domain.Primitives.ActionTracker
{

  

        public class ChildActionTracker : IActionTracker
        {
            public ChildActionTracker(
                string type,
                string domain,
                Guid? parentHistoryId,
                IEntity entity)
            {
                if (parentHistoryId is null)
                {
                    throw new ArgumentNullException(nameof(parentHistoryId));
                }

                Type = type;
                Domain = domain;
                ParentHistoryId = parentHistoryId.Value;
                Entity = entity;
                CreatedOn = DateTime.UtcNow;
            }

            public string Type { get; }

            public string Domain { get; }

            public DateTime CreatedOn { get; }

            public Guid ParentHistoryId { get; }

            public IEntity Entity { get; }
        }


    

}