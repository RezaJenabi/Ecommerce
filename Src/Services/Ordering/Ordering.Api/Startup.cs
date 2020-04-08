using Infrastructure.Api.Configure;
using Infrastructure.Api.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Api.Configure;
using Ordering.Commands.Orders;
using Ordering.Domain.Models.OrderAggregate.Events.DomainEventHandlers;
using Ordering.Domain.OrderingDbContext;
using System.Collections.Generic;
using System.Reflection;

namespace Ordering.Api
{
    public class Startup : RootStartup
    {
        private readonly Assembly[] _mediatRAssemblies = { typeof(OrderCreateEventHandler).Assembly };
        private readonly IList<Assembly> _registerFluentValidation = new List<Assembly>() { typeof(CreateOrder).Assembly };

        public override IEnumerable<Assembly> RegisterFluentValidation => _registerFluentValidation;
        public override Assembly[] MediatRAssemblies => _mediatRAssemblies;

        public Startup(IConfiguration configuration) : base(configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddSqlContext<OrderingDbContext>(Configuration.GetConnectionString("AppDbContextConnection"));

            services.AddRepositories();
            services.AddCommandsQueries();
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            base.Configure(app, env);
        }
    }
}
