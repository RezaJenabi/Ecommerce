using Infrastructure.Core.DatabaseContext;
using Infrastructure.Domain.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Api.Configure
{
    public static class ConfigureDbContext
    {
        public static void AddSqlContext<TDbContext>(this IServiceCollection services, string connectionString)
            where TDbContext : DbContextBase
        {
            services.AddDbContext<TDbContext>
            (options => options.UseSqlServer(connectionString, providerOptions => providerOptions.CommandTimeout(60))

            );

            services.AddScoped<IDbContext>(sp => sp.GetRequiredService<TDbContext>());
            services.AddScoped<DbContext>(sp => sp.GetRequiredService<TDbContext>());
        }
    }
}
