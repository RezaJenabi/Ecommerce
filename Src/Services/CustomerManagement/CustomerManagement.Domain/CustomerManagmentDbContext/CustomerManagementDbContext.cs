using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Reflection;
using Infrastructure.Utilities.Extensions;
using Infrastructure.Core.BaseEntities;
using Infrastructure.Domain.DatabaseContext;

namespace CustomerManagement.Domain.CustomerManagmentDbContext
{
    public class CustomerManagementDbContext : DbContextBase
    {
        public override string DefaultSchema => "CustomerManagement";
        public CustomerManagementDbContext(DbContextOptions<CustomerManagementDbContext> options, IMediator mediator) : base(options, mediator)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            builder.RegisterAllEntities<IEntity>(currentAssembly);
            builder.RegisterEntityTypeConfiguration(currentAssembly);

            base.OnModelCreating(builder);

        }
    }
}