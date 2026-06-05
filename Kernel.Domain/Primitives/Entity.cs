using System;
using System.Collections.Generic;
using System.Text;

namespace Kernel.Domain.Primitives
{
    public abstract class Entity<TId> where TId : notnull
    {

        public TId Id {  get; private set; }


        protected Entity (TId id )
        {

            Id = id;
        }


        protected Entity()
        {
        }



        public override bool Equals (object? obj)
        {

            // si objecte que rebem no té TId no es de tipus Entity.

            if (obj is not Entity<TId> other) {
                
                return false; }


            // si es igual ho és.
            return Id.Equals(other.Id);
        }




    }
}
