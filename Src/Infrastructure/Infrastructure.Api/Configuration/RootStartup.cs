using Infrastructure.Utilities.ActionFilter;
using Infrastructure.Utilities.ResponseWrapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace Infrastructure.Api.Configuration
{
    public abstract class RootStartup
    {
        protected abstract string Title { get; }
        protected virtual int[] Versions => new[] { 1 };
        protected IConfiguration Configuration { get; set; }
        private IServiceCollection ServiceCollection { get; set; }


        public RootStartup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            ServiceCollection = services;

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }

        public virtual void Configure(IApplicationBuilder app)
        {
            app.UseAPIResponseWrapperMiddleware();
        }
    }
}
