using Infrastructure.Core.Repositories;
using Infrastructure.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Api.Configure
{
    public static class ConfigureBaseRepository
    {
        public static void AddBaseRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        }
    }
}
