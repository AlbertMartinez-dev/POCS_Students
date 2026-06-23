using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Kernel.Application.Abstractions.Data
{
    public interface IUnitOfWorkService
    {
        Task<int> SaveChangesAsync(CancellationToken c = default);

        /// <summary>
        /// Saves changes without transaction management.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        Task<int> SavechangesWithoutTransactionASync(CancellationToken c = default);

        IDbTransaction BeginTransactionWithConnection();

        void BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();

        void DiscardChanges<T>(T entity);

        void DiscardChanges<T>(List<T> entities);

        void TriggerOutboxJob();
    }
}
