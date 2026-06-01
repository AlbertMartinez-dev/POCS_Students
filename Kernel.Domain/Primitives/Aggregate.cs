using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using MediatR;

namespace Kernel.Domain.Primitives
{
    public abstract class Aggregate<TId> where TId: notnull
    {
        // Llista de domainevents
        private readonly List<INotification> _domainEvents = new();
        public TId Id { get; private set; }

        // Entity Framework
        protected Aggregate() { }

        protected Aggregate(TId id)
        {
            Id = id;
        }


        protected void AddDomainEvent(INotification domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }



        // Comprovació de si aquest objecte es un aggregate o no
        public override bool Equals(object? obj)
        {
            if (obj is not Aggregate<TId> other) return false;
            return Id.Equals(other.Id);
        }
        // Un hash code és un número enter que .NET calcula a partir d’un objecte per poder trobar-lo i comparar-lo més ràpidament en col·leccions com HashSet o Dictionary.
        public override int GetHashCode() => Id.GetHashCode();

    }
}
