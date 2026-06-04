using Kernel.Domain.Primitives.ActionTracker;



namespace Kernel.Domain.Primitives
{


    public abstract class Aggregate<TId> : IEntity  where TId : notnull 
    {
        private readonly List<IActionTracker> _actions = new();

        public TId Id { get; private set; }

        protected Aggregate()
        {
        }

        protected Aggregate(TId id)
        {
            Id = id;
        }

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
