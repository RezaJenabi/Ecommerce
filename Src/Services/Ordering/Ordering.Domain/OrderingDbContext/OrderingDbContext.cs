using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Reflection;
using Infrastructure.Utilities.Extensions;
using Infrastructure.Core.BaseEntities;
using Infrastructure.Domain.DatabaseContext;

namespace Ordering.Domain.OrderingDbContext
{
    public class OrderingDbContext : DbContextBase
    {
        public override string DefaultSchema => "Ordering";
        public OrderingDbContext(DbContextOptions<OrderingDbContext> options, IMediator mediator) : base(options, mediator)
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