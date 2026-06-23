using System;
using System.Collections.Generic;
using System.Text;
using Kernel.Domain.Primitives;
namespace Kernel.Domain
{
    public interface IRepository2<TEntity, TEntityId> where TEntity : Aggregate<TEntityId>
    {


        TEntity Add (TEntity entity);

        Task<TEntity> GetByIdAsync(TEntityId id, CancellationToken cancellation = default);
    }
}
