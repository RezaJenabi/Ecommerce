using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Core.DatabaseContext
{
    public interface IDbContext
    {
        string DefaultSchema { get; }
        void RollbackTransaction();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitTransactionAsync(IDbContextTransaction transaction);
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
    }
}