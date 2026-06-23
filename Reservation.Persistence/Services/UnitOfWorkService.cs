using System.Data;
using Kernel.Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Reservation.Persistence.Services
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        private readonly ReservationDbContext _dbContext;
        private IDbContextTransaction? _transaction;

        public UnitOfWorkService(ReservationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<int> SaveChangesAsync(CancellationToken c = default)
        {
            return _dbContext.SaveChangesAsync(c);
        }

        public Task<int> SavechangesWithoutTransactionASync(CancellationToken c = default)
        {
            return _dbContext.SaveChangesAsync(c);
        }

        public IDbTransaction BeginTransactionWithConnection()
        {
            _transaction = _dbContext.Database.BeginTransaction();

            return _transaction.GetDbTransaction();
        }

        public void BeginTransaction()
        {
            _transaction = _dbContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _transaction?.Commit();
            _transaction?.Dispose();
            _transaction = null;
        }

        public void RollbackTransaction()
        {
            _transaction?.Rollback();
            _transaction?.Dispose();
            _transaction = null;
        }

        public void DiscardChanges<T>(T entity)
        {
            if (entity is null)
            {
                return;
            }

            _dbContext.Entry(entity).State = EntityState.Detached;
        }

        public void DiscardChanges<T>(List<T> entities)
        {
            foreach (var entity in entities)
            {
                _dbContext.Entry(entity).State = EntityState.Detached;
            }
        }

        public void TriggerOutboxJob()
        {
            // De moment buit si encara no tens Outbox implementat
        }
    }
}