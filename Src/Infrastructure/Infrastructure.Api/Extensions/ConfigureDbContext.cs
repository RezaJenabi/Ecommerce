using Infrastructure.Domain.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infrastructure.Api.Extensions
{
    public static class ConfigureDbContext
    {
        public static void AddSqlContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<WebSiteDbContext>
            (options => options.UseSqlServer(configuration.GetConnectionString("AppDbContextConnection")));

        }

    }
}