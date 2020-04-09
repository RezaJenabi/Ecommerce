using Infrastructure.Core.Repositories;
using Infrastructure.Core.Security;
using Infrastructure.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Api.Configure
{
    public static class BaseConfigure
    {
        public static void AddBaseRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
        public static void AddCurrentRequest(this IServiceCollection services)
        {
            services.AddScoped<ICurrentRequest, CurrentRequest>();
        }
    }
}
