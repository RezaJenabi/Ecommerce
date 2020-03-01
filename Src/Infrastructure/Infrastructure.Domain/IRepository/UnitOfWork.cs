using Infrastructure.Domain.DbContext;

namespace Infrastructure.Domain.IRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        public WebSiteDbContext Context { get; }

        public UnitOfWork(WebSiteDbContext context)
        {
            Context = context;
        }
        public void Commit()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();

        }

        public void CommitAsync()
        {
            Context.SaveChangesAsync();
        }

        public void Rollback()
        {
            throw new System.NotImplementedException();
        }
    }
}
