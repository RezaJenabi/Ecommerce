using CustomerManagement.Domain.CustomerManagmentContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagement.Api.Configure
{
    public static class ConfigureDbContext
    {
        public static void AddSqlContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<CustomerManagementDbContext>
            (options => options.UseSqlServer(configuration.GetConnectionString("AppDbContextConnection")));

        }
    }
}
