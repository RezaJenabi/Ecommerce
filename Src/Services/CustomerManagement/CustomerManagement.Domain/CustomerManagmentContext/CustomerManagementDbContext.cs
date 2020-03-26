using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Domain.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Infrastructure.Utilities.Extensions.ModelBuilder;
using MediatR;
using System.Data;
using Infrastructure.Domain.Extensions;
using Infrastructure.Domain.DataBaseContext;
using System.Reflection;

namespace CustomerManagement.Domain.CustomerManagmentContext
{
    public class CustomerManagementDbContext : DbContextBase
    {
        public override string DefaultSchema => "CustomerManagement";
        public CustomerManagementDbContext(DbContextOptions<CustomerManagementDbContext> options, IMediator mediator):base(options, mediator)
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