using Infrastructure.Domain.BaseEntities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Infrastructure.Utilities.Extensions.ModelBuilder;
using MediatR;
using Infrastructure.Domain.DataBaseContext;

namespace CustomerManagement.Domain.DbContext
{
    public class CustomerManagementDbContext : DbContextBase
    {
        public override string DefaultSchema { get => "customer"; }

        public CustomerManagementDbContext(DbContextOptions<CustomerManagementDbContext> options, IMediator mediator) : base(options, mediator)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            modelBuilder.RegisterAllEntities<IEntity>(currentAssembly);
            modelBuilder.RegisterEntityTypeConfiguration(currentAssembly);
          
            base.OnModelCreating(modelBuilder);
        }

      
    }
}
