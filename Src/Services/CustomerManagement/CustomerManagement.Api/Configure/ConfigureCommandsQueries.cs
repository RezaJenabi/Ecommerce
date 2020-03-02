using CommandsHandler.Customers;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerManagement.Api.Configure
{
    public static class ConfigureCommandsQueries
    {
        public static void AddCommandsQueries(this IServiceCollection services)
        {
            services.AddScoped<ICreateCustomerHandler, CreateCustomerHandler>();
        }
    }
}
