using Infrastructure.Domain.DbContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Domain.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        WebSiteDbContext Context { get; }
        void Commit();
        void CommitAsync();
        void Rollback();
    }
}
