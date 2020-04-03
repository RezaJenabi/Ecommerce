using System.Reflection;
using Infrastructure.Utilities.ActionFilter;
using Infrastructure.Utilities.ResponseWrapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentValidation.AspNetCore;
using Commands.Customers;
using CustomerManagement.Api.Configure;
using Domain.Models.CustomerAggregate.Events.DomainEventHandlers;
using Microsoft.OpenApi.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using CustomerManagement.Api.Extensions;

namespace CustomerManagement.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMediatR(typeof(CustomerCreateEventHandler).Assembly);
            services.AddHttpContextAccessor();
            services.AddSqlContext(Configuration);
            services.AddRepositories();
            services.AddCommandsQueries();

            services.AddControllers(options =>
            {
                options.Filters.Add(new ValidatorActionFilter());
            }).ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressConsumesConstraintForFormFileParameters = true;
                options.SuppressModelStateInvalidFilter = true;
            }).AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<CreateCustomer>();
                fv.ImplicitlyValidateChildProperties = true;
            });
            services.AddControllers();

            services.AddCustomerManagementAuthentication(Configuration);
            
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllParametersInCamelCase();
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Customer",
                    Version = "v1",
                    Description = "Customer"
                });

            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.Map("/swagger/v1/swagger.json", b =>
            {
                b.Run(async x => {
                    var json = File.ReadAllText("swagger.json");
                    await x.Response.WriteAsync(json);
                });
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v1/swagger.json", "Test");
            });


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
