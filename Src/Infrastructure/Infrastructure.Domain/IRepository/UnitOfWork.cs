using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Domain.IRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; }

        public void BeginTransaction()
        {
            Context.Database.BeginTransaction();
        }

        public void Commit()
        {
            Context.Database.CommitTransaction();
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public void RoolBack()
        {
            Context.Database.RollbackTransaction();
        }
    }
}
