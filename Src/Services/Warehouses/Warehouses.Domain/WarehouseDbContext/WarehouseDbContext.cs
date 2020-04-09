using Infrastructure.Core.BaseEntities;
using Infrastructure.Domain.DatabaseContext;
using Infrastructure.Utilities.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Warehouses.Domain.WarehouseDbContext
{
    public class WarehouseDbContext : DbContextBase
    {
        public override string DefaultSchema => "Warehouse";
        public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options, IMediator mediator) : base(options, mediator)
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