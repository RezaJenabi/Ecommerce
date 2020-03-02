using Infrastructure.Domain.IRepository;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerManagement.Api.Configure
{
    public static class ConfigureRepository
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
