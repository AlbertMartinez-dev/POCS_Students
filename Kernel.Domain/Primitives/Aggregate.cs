using Kernel.Domain.Primitives.ActionTracker;
using MediatR;

namespace Kernel.Domain.Primitives
{
    public abstract class Aggregate<TId> : IEntity where TId : notnull
    {
        private readonly List<IActionTracker> _actions = new();
        private readonly List<INotification> _domainEvents = new();

        public TId Id { get; private set; }

        protected Aggregate()
        {
        }

        protected Aggregate(TId id)
        {
            Id = id;
        }

        // -------------------------
        // History Actions
        // -------------------------

        protected void AddAction(IActionTracker actionTracker)
        {
            _actions.Add(actionTracker);
        }

        public ICollection<IActionTracker> GetActions()
        {
            return _actions.ToList();
        }

        public IActionTracker? GetCurrentHistoryVersion()
        {
            return _actions
                .OfType<ParentActionTracker>()
                .LastOrDefault();
        }

        public bool HasCurrentHistoryVersion()
        {
            return _actions
                .OfType<ParentActionTracker>()
                .Any();
        }

        public void ClearActions()
        {
            _actions.Clear();
        }

        // -------------------------
        // Domain Events
        // -------------------------

        protected void PushEvent(INotification domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public IReadOnlyCollection<INotification> GetDomainEvents()
        {
            return _domainEvents.AsReadOnly();
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        // -------------------------
        // Generic Id
        // -------------------------

        public object GetId()
        {
            return Id;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Aggregate<TId> other)
            {
                return false;
            }

            return Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}