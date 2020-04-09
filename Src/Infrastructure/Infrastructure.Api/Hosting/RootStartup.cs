using FluentValidation.AspNetCore;
using Infrastructure.Api.Authentication;
using Infrastructure.Api.Configure;
using Infrastructure.Utilities.ActionFilter;
using Infrastructure.Utilities.ResponseWrapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Infrastructure.Api.Hosting
{
    public class RootStartup
    {
        private readonly IConfiguration _configuration;
        public virtual IEnumerable<Assembly> RegisterFluentValidation { get;}
        public virtual Assembly[] MediatRAssemblies { get; }

        public RootStartup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(MediatRAssemblies);
            services.AddHttpContextAccessor();
            services.AddBaseRepositories();
            services.AddCurrentRequest();

            services.AddControllers(options =>
            {
                options.Filters.Add(new ValidatorActionFilter());
            }).ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressConsumesConstraintForFormFileParameters = true;
                options.SuppressModelStateInvalidFilter = true;
            }).AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblies(RegisterFluentValidation);
                fv.ImplicitlyValidateChildProperties = true;
            });
            services.AddControllers();

            services.AddCustomerManagementAuthentication(_configuration);

        }

        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseAPIResponseWrapperMiddleware();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "api/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}