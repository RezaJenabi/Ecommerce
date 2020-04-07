using CustomerManagement.Repository;
using Domain.Models.CustomerAggregate.Repository;
using Infrastructure.Core.DatabaseContext;
using Infrastructure.Core.Repositories;
using Infrastructure.Domain.DatabaseContext;
using Infrastructure.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerManagement.Api.Configure
{
    public static class ConfigureRepository
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();

        }
    }
}
