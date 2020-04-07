using Infrastructure.Core.DatabaseContext;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace Infrastructure.Domain.DatabaseContext
{
    public class UnitOfWork : IUnitOfWork
    {
        public IDbContext Context { get; }

        public UnitOfWork(IDbContext context)
        {
            Context = context;
        }
        public void Commit(IDbContextTransaction dbContextTransaction)
        {
            Context.CommitTransactionAsync(dbContextTransaction);
        }

        public void Rollback()
        {
            Context.RollbackTransaction();
        }


        public Task<IDbContextTransaction> BeginTransactionAsync()
        {
           return Context.BeginTransactionAsync();
        }

    }
}