using Infrastructure.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerManagement.Api.Configure
{
    public static class ConfigureRepository
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
