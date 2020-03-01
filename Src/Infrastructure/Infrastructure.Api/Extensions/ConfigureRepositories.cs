using Infrastructure.Domain.IRepository;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Api.Extensions
{
    public static class ConfigureRepository
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
