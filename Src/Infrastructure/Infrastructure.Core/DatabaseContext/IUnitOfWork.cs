using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Core.DatabaseContext
{
    public interface IUnitOfWork
    {
        IDbContext Context { get; }
        void Commit(IDbContextTransaction dbContextTransaction);
        Task<IDbContextTransaction> BeginTransactionAsync();
        void Rollback();
    }
}
