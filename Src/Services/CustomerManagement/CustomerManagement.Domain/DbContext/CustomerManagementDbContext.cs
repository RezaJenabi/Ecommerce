using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Utilities.Extensions.ModelBuilder;
using Infrastructure.Domain.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection;

namespace CustomerManagement.Domain.DbContext
{
    public class CustomerManagementDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        private IDbContextTransaction _transaction;

        public CustomerManagementDbContext(DbContextOptions<CustomerManagementDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            modelBuilder.RegisterAllEntities<IEntity>(currentAssembly);
            modelBuilder.RegisterEntityTypeConfiguration(currentAssembly);
            modelBuilder.AddRestrictDeleteBehaviorConvention();
            modelBuilder.AddPluralizingTableNameConvention();
            //modelBuilder.AddSequentialGuidForIdConvention();
            //modelBuilder.ConvertNameForSql();
            base.OnModelCreating(modelBuilder);
        }

        public void BeginTransaction()
        {
            _transaction = Database.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                SaveChanges();
                _transaction.Commit();
            }
            finally
            {
                _transaction.Dispose();
            }
        }

        public void Rollback()
        {
            _transaction.Rollback();
            _transaction.Dispose();
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
            entry.State = EntityState.Modified;
            if (softDelete == null) return;
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
