using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using MediatR;
using System.Data;
using Infrastructure.Domain.Extensions;
using Infrastructure.Utilities.Extensions;
using Infrastructure.Core.BaseEntities;
using Infrastructure.Core.DatabaseContext;

namespace Infrastructure.Domain.DatabaseContext
{
    public abstract class DbContextBase : DbContext, IDbContext
    {
        public virtual string DefaultSchema { get; }

        private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction;

        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        public bool HasActiveTransaction => _currentTransaction != null;

        public DbContextBase(DbContextOptions options, IMediator mediator)
            : base(options)
        {
            Database.SetCommandTimeout(300);
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.AddRestrictDeleteBehaviorConvention();
            builder.AddPluralizingTableNameConvention();
            //modelBuilder.AddSequentialGuidForIdConvention();
            //modelBuilder.ConvertNameForSql();
            builder.HasDefaultSchema(DefaultSchema);
        }



        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEventsAsync(this);
            var result = await base.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveEntitiesAsync();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();


            foreach (var entry in entries.Where(x => x.State != EntityState.Deleted))
            {
                EntityTrack(entry);
            }

            foreach (var entry in entries.Where(x => x.State == EntityState.Deleted))
            {
                SoftDeleteTrack(entry);
            }
        }

        private void EntityTrack(EntityEntry entry)
        {
            var now = DateTime.UtcNow;
            var user = GetCurrentUser();

            if (!(entry.Entity is ITrackable trackable)) return;
            trackable.LastUpdatedAt = now;
            trackable.LastUpdatedBy = user;

            if (entry.State != EntityState.Added) return;
            trackable.CreatedAt = now;
            trackable.CreatedBy = user;
        }

        private void SoftDeleteTrack(EntityEntry entry)
        {
            var now = DateTime.UtcNow;
            var user = GetCurrentUser();
            var softDelete = entry.Entity as ISoftDelete;
            if (softDelete == null) return;
            entry.State = EntityState.Modified;
            softDelete.Deleted = true;
            softDelete.DeletedAt = now;
            softDelete.DeletedBy = user;
        }

        private string GetCurrentUser()
        {
            return "UserName";
        }
    }
}