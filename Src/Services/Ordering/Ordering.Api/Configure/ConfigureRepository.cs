using Infrastructure.Core.Repositories;
using Infrastructure.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Domain.Models.OrderAggregate.Repository;
using Ordering.Repository;

namespace Ordering.Api.Configure
{
    public static class ConfigureRepository
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IOrderRepository, OrderRepository>();

        }
    }
}
