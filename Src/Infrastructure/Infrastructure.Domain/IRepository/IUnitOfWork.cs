using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Domain.IRepository
{
    public interface IUnitOfWork: IDisposable
    {
        DbContext Context { get; }
        void Commit();
        void RoolBack();
        void BeginTransaction();
    }
}
