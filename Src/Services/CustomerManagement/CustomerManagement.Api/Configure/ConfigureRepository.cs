using CustomerManagement.Domain.Models.CustomerAggregate.Repository;
using CustomerManagement.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerManagement.Api.Configure
{
    public static class ConfigureRepository
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();

        }
    }
}
