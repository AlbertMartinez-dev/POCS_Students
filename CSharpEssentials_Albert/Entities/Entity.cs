using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpEssentials_Albert.Entities
{
    public abstract class Entity<TId> where TId : notnull
    {
        public TId Id { get; protected set; }

        protected Entity(TId id) => Id = id;
    }

}
