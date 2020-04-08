using Microsoft.Extensions.DependencyInjection;
using Ordering.CommandsHandler.Orders;

namespace Ordering.Api.Configure
{
    public static class ConfigureCommandsQueries
    {
        public static void AddCommandsQueries(this IServiceCollection services)
        {
            services.AddScoped<ICreateOrderHandler, CreateOrderHandler>();
        }
    }
}
