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
using Domain.Models.CustomerAggregate.Events.DomainEvents;
using Domain.Models.CustomerAggregate.Events.DomainEventHandlers;

namespace CustomerManagement.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAPIResponseWrapperMiddleware();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
