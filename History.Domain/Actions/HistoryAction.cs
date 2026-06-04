using System;
using System.Collections.Generic;
using System.Text;

namespace History.Domain.Actions
{

    public class HistoryAction
    {
        private HistoryAction()
        {
        }

        public HistoryAction(
            string type,
            string domain,
            Guid? historyId,
            DateTime createdOn)
        {
            Id = Guid.NewGuid();
            Type = type;
            Domain = domain;
            HistoryId = historyId;
            CreatedOn = createdOn;
        }

        public Guid Id { get; private set; }

        public string Type { get; private set; }

        public string Domain { get; private set; }

        public Guid? HistoryId { get; private set; }

        public DateTime CreatedOn { get; private set; }
    }
}
